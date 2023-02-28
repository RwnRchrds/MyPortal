using System;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Logic.Models.Data.Addresses;

public class AddressLinkDataModel
{
    public AddressLinkDataModel(AddressPerson addressPerson)
    {
        AddressLinkId = addressPerson.Id;
        AddressId = addressPerson.AddressId;
        AddressTypeId = addressPerson.AddressTypeId;
        Main = addressPerson.Main;
        BuildingNumber = addressPerson.Address.BuildingNumber;
        BuildingName = addressPerson.Address.BuildingName;
        Apartment = addressPerson.Address.Apartment;
        Street = addressPerson.Address.Street;
        District = addressPerson.Address.District;
        Town = addressPerson.Address.Town;
        County = addressPerson.Address.County;
        Country = addressPerson.Address.Country;
        Postcode = addressPerson.Address.Postcode;
        Validated = addressPerson.Address.Validated;
    }

    public AddressLinkDataModel(AddressAgency addressAgency)
    {
        AddressLinkId = addressAgency.Id;
        AddressId = addressAgency.AddressId;
        AddressTypeId = addressAgency.AddressTypeId;
        Main = addressAgency.Main;
        BuildingNumber = addressAgency.Address.BuildingNumber;
        BuildingName = addressAgency.Address.BuildingName;
        Apartment = addressAgency.Address.Apartment;
        Street = addressAgency.Address.Street;
        District = addressAgency.Address.District;
        Town = addressAgency.Address.Town;
        County = addressAgency.Address.County;
        Country = addressAgency.Address.Country;
        Postcode = addressAgency.Address.Postcode;
        Validated = addressAgency.Address.Validated;
    }
    
    public Guid AddressLinkId { get; set; }
    
    public Guid AddressId { get; set; }
    
    public Guid AddressTypeId { get; set; }

    public bool Main { get; set; }
    
    public string BuildingNumber { get; set; }
    
    public string BuildingName { get; set; }
    
    public string Apartment { get; set; }
    
    public string Street { get; set; }
    
    public string District { get; set; }
    
    public string Town { get; set; }
    
    public string County { get; set; }
    
    public string Postcode { get; set; }
    
    public string Country { get; set; }
    
    public bool Validated { get; set; }
}