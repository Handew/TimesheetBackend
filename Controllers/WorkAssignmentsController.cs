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
                var wa = (from w in db.WorkAssignments where w.Active == true select w).ToList();
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
    }
}
