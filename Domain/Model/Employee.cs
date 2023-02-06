namespace Domain
{
    public class Employee : Entity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? AddressEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public bool? Active { get; set; }
    }
}
