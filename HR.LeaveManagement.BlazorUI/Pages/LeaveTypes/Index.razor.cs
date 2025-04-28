using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILeaveTypeService leaveTypeService { get; set; }
        [Inject]
        public ILeaveAllocationService leaveAllocationService { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; private set; }
        public string Message { get; set; } = string.Empty;
        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/Leavetypes/create/");
        }
        protected void AllocateLeaveType(int id)
        {
           // leaveAllocationService.CreateLeaveAllocationAsync(id);
        }
        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/Leavetypes/edit/{id}");
        }
        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/Leavetypes/Details/{id}");
        }
        protected async Task DeleteLeaveType(int id)
        {
            var response = await leaveTypeService.DeleteLeaveType(id);
            if (response.Success)
                StateHasChanged();
            else
                Message = response.Message;
        }
        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await leaveTypeService.GetLeaveTypes();
        }
    }
}