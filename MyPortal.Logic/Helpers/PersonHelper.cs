using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Helpers;

public class PersonHelper
{
    public static Person CreatePersonFromModel(PersonRequestModel model)
    {
        var createDate = DateTime.Now;

        return new Person
        {
            FirstName = model.FirstName,
            PreferredFirstName = model.PreferredFirstName,
            LastName = model.PreferredLastName,
            PreferredLastName = model.PreferredLastName,
            MiddleName = model.MiddleName,
            Title = model.Title,
            NhsNumber = model.NhsNumber,
            CreatedDate = createDate,
            Deleted = false,
            Gender = model.Gender,
            Dob = model.Dob,
            EthnicityId = model.EthnicityId,
            Directory = new Directory
            {
                Name = "person-root"
            }
        };
    }

    public static void UpdatePersonFromModel(Person person, PersonRequestModel requestModel)
    {
        person.FirstName = requestModel.FirstName;
        person.PreferredFirstName = requestModel.PreferredFirstName;
        person.LastName = requestModel.LastName;
        person.PreferredLastName = requestModel.PreferredLastName;
        person.MiddleName = requestModel.MiddleName;
        person.Title = requestModel.Title;
        person.NhsNumber = requestModel.NhsNumber;
        person.Gender = requestModel.Gender;
        person.Dob = requestModel.Dob;
        person.EthnicityId = requestModel.EthnicityId;
        person.Deceased = requestModel.Deceased;
        person.PhotoId = requestModel.PhotoId;
    }
}