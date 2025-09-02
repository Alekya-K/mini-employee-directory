namespace employeedirectorysystemApi.Models
{
    public class EmployeeRequestDto
    {
        #region Properties

        public int EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }

        #endregion
    }
}
