﻿using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailQuery :IRequest<LeaveAllocationDetailDto>
    {
        public int id { get; set; }
    }
}
