
using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseEntity
    {
        public int NumberOfDays { get; set; }
        //[ForeignKey("EmployeeId")]
        //public Employee Employee { get; set; }
        //public string EmployeeId { get; set; }
        //[ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
