using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> Errors { get; set; }
        public BadRequestException(string Message) :base(Message)
        {
            
        }
        public BadRequestException(string Message , ValidationResult validationResult) : base(Message)
        {
            Errors = new();
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}
