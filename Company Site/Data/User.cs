namespace Company_Site.Data
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime DOB { get; set; }
        public DateTime LastLogin { get; set; }
        public string? OfficeLocation { get; set; }
        public string? Status { get; set; }
        public string? Access { get; set; }
    }
}
