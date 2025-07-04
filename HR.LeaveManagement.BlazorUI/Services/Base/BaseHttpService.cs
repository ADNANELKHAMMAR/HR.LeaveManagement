﻿namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;

        public BaseHttpService(IClient client)
        {
            _client = client;
        }
        protected Response<Guid> ConvertApiException<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>
                {
                    Message = "Invalid data was submitted",
                    Success = false,
                    ValidationErrors = ex.Response
                };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>
                {
                    Message = "The record was not found",
                    Success = false,
                    ValidationErrors = ex.Response
                };
            }

            else
            {
                return new Response<Guid>
                {
                    Message = "Something went wrong ,please try again later",
                    Success = false,
                    ValidationErrors = ex.Response
                };
            }
        }
    }
}
