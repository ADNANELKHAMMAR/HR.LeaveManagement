namespace HR.LeaveManagement.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string Name , object key) : base($"{Name}{key} was not found")
        {

        }
    }
}
