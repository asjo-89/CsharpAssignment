namespace Business.Models;

public class ContactForm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string StreetAddress { get; set; } = null!;
    public int? PostalCode { get; set; }
    public string City { get; set; } = null!;
}
