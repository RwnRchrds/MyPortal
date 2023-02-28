using MyPortal.Database.Models.Search;

namespace MyPortal.Logic.Models.Requests.Addresses;

public class AddressSearchRequestModel
{
    public string Apartment { get; set; }
    
    public string BuildingName { get; set; }
    
    public string BuildingNumber { get; set; }

    public string Street { get; set; }
    
    public string District { get; set; }
    
    public string Town { get; set; }
    
    public string County { get; set; }
    
    public string Postcode { get; set; }
    
    public string Country { get; set; }

    internal AddressSearchOptions GetSearchOptions()
    {
        return new AddressSearchOptions
        {
            Apartment = Apartment,
            BuildingName = BuildingName,
            BuildingNumber = BuildingNumber,
            Street = Street,
            District = District,
            Town = Town,
            County = County,
            Postcode = Postcode,
            Country = Country
        };
    }
}