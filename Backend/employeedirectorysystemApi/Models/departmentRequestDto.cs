namespace employeedirectorysystemApi.Models
{
    public class departmentRequestDto
    {
        #region Properties

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        #endregion

    }
}
