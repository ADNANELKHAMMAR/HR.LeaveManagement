﻿namespace HR.LeaveManagement.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        //public string? CreatedBy { get; set; } 
        public DateTime? DateModified { get; set; }
        //public string? LastModifiedBy { get; set; }
    }
}
