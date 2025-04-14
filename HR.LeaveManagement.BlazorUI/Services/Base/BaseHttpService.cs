namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;

        public BasHttpService(IClient client)
        {
            _client = client;
        }
    }
}
