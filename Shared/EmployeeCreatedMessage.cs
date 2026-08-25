namespace Shared
{
    public class EmployeeCreatedMessage
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string EmployeeEmail { get; set; } = string.Empty;
    }
}
