namespace AbstractFactory.Business.Models.Commerce;

public class Address
{
    public string To { get; set; } = string.Empty;
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string City { get; set; } = string.Empty;
    public Country Country { get; set; } = Country.none;
}
