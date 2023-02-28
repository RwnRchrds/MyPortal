using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Search;

namespace MyPortal.Logic.Models.Requests.Addresses;

public class AddressRequestModel
{
    public string BuildingNumber { get; set; }
    
    [StringLength(128)]
    public string BuildingName { get; set; }
    
    [StringLength(128)]
    public string Apartment { get; set; }
    
    [Required]
    [StringLength(256)]
    public string Street { get; set; }
    
    [StringLength(256)]
    public string District { get; set; }
    
    [Required]
    [StringLength(256)]
    public string Town { get; set; }
    
    [Required]
    [StringLength(256)]
    public string County { get; set; }
    
    [Required]
    [StringLength(128)]
    public string Postcode { get; set; }
    
    [Required]
    [StringLength(128)]
    public string Country { get; set; }
}