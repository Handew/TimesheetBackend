using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimesheetBackend.Models;

namespace TimesheetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkAssignmentsController : ControllerBase
    {
        private tuntidbContext db = new tuntidbContext();

        [HttpGet]
        [Route("")]
        public ActionResult GetAllActiveWorkAssignments()
        {
            try
            {
                var wa = (from w in db.WorkAssignments where w.Active == true && w.Completed == false select w).ToList();
                return Ok(wa);
            }
            catch (Exception e)
            {
                return BadRequest("Virhe: " + e);
            }
            finally
            {
                db.Dispose();
            }
        }

        [HttpPost]
        [Route("")]
        public bool StartStop(Operation op)
        {
            if (op == null)
            {
                return false;
            }

            WorkAssignment assignment = (from w in db.WorkAssignments
                                         where (w.Active == true) &&
                                         (w.IdWorkAssingment == op.WorkAssidnmentID)
                                         select w).FirstOrDefault();

            if (assignment == null)
            {
                return false;
            }


            // -----------------------------Start------------------------------------------------------------------
            else if (op.OperationType == "start")
            {
                if (assignment.InProgress == true || assignment.Completed == true)
                {
                    return (false);
                }

                assignment.InProgress = true;
                assignment.WorkStartedAt = DateTime.Now.AddHours(3);
                assignment.LastModifiedAt = DateTime.Now.AddHours(3);
                db.SaveChanges();

                Timesheet newEntry = new Timesheet()
                {
                    IdWorkAssignment = op.WorkAssidnmentID,
                    StartTime = DateTime.Now.AddHours(3),
                    Active = true,
                    IdEmployee = op.EmployeeID,
                    IdCustomer = op.CustomerID,
                    CreatedAt = DateTime.Now.AddHours(3),
                    Comments = op.Comment,
                    Latitude = op.Latitude,
                    Longitude = op.Longitude
                };

                db.Timesheets.Add(newEntry);
                db.SaveChanges();
                return (true);

            }
            // -------------------- IF STOP ------------------------------------------------------------------------
            else
            {
                if (assignment.InProgress == false || assignment.Completed == true)
                {
                    return (false);
                }

                assignment.InProgress = false;
                assignment.CompletedAt = DateTime.Now.AddHours(3);
                assignment.Completed = true;
                assignment.LastModifiedAt = DateTime.Now.AddHours(3);

                Timesheet ts = (from t in db.Timesheets
                                where (t.Active == true) &&
                                (t.IdWorkAssignment == op.WorkAssidnmentID)
                                select t).FirstOrDefault();

                ts.StopTime = DateTime.Now.AddHours(3);
                ts.LastModifiedAt = DateTime.Now.AddHours(3);
                ts.Comments = op.Comment;
                ts.Longitude = op.Longitude;
                ts.Latitude = op.Latitude;
                db.SaveChanges();

                return (true);
            }
        }
    }
}
