using System;
using System.Collections.Generic;

#nullable disable

namespace TimesheetBackend.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Timesheets = new HashSet<Timesheet>();
        }

        public int IdCustomer { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public bool? Active { get; set; }

        public virtual WorkAssignment WorkAssignment { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
