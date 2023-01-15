namespace WebBlazor.ModelWebBlazor
{
    public class AddEmployeeModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? AddressEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Password { get; set; }

        public AddEmployeeModel()
        {

        }
        public AddEmployeeModel(string? name, string? surname, string? addressEmail, string? phoneNumber, string? licenseNumber, string? password)
        {
            Name = name;
            Surname = surname;
            AddressEmail = addressEmail;
            PhoneNumber = phoneNumber;
            LicenseNumber = licenseNumber;
            Password = password;
        }
    }
}
