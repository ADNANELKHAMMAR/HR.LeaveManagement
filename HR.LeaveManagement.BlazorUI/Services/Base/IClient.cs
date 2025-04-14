namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public partial interface IClient
    {
        HttpClient httpClient { get; }
    }
}
