﻿namespace Business.Models;

public class Contact
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int PhoneNumber { get; set; }
    public string StreetAddress { get; set; } = null!;
    public int PostalCode { get; set; }
    public string City { get; set; } = null!;
}