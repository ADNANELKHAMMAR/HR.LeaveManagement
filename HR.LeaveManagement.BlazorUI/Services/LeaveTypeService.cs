using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;
using System.Runtime.InteropServices;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService,ILeaveTypeService
    {
        protected IClient _client;
        private readonly IMapper _mapper;
        public LeaveTypeService(IClient client, IMapper mapper) : base(client)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
        {
            try
            {
                var CreatLeaveType = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypesPOSTAsync(CreatLeaveType);
                return  new Response<Guid>()
                {
                    Message = "leave type succefully created",
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiException<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> DeleteLeaveType(int Id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(Id);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiException<Guid>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int Id)
        {
            var leaveType = await _client.LeaveTypesGETAsync(Id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leavetypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leavetypes);
        }

        public async Task<Response<Guid>> UpdateLeaveType(int Id, LeaveTypeVM leaveType)
        {
            try
            {
                var UpdateLeaveType = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
                await _client.LeaveTypesPUTAsync(UpdateLeaveType);
                return new Response<Guid>()
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiException<Guid>(ex);
                throw;
            }
        }
    }
}
