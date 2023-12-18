using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace WannaBePrincipal.Models
{
    public class Location
    {
        [Required]
        public double Lat { get; set; }

        [Required]
        public double Lng { get; set; }
    }

    public class AddressData
    {
        [Required]
        public required string City { get; set; }

        [Required]
        public required string Street { get; set; }

        [Required]
        public required string ZipCode { get; set; }

        [Required]
        public required string Suite { get; set; }

        [Required]
        public required Location Geo { get; set; }
    }

    public class CompanyData
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string BS { get; set; }

        [Required]
        public required string CatchPhrase { get; set; }
    }

    public class User
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9+_.-]+@(.+)$", ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Website { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required CompanyData Company { get; set; }

        [Required]
        public required AddressData Address { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> geoDict = new()
            {
                { "lat", Address.Geo.Lat },
                { "lng", Address.Geo.Lng }
            };

            Dictionary<string, object> addressDict = new()
            {
                { "street", Address.Street },
                { "city", Address.City },
                { "zipCode", Address.ZipCode },
                { "suite", Address.Suite },
                { "geo", geoDict }
            };

            Dictionary<string, object> companyDict = new()
            {
                { "bs", Company.BS },
                { "catchPharse", Company.CatchPhrase },
                { "name", Company.Name }
            };

            Dictionary<string, object> userData = new()
            {
                { "name", Name },
                { "email", Email },
                { "username", UserName },
                { "website", Website },
                { "address",  addressDict},
                { "company", companyDict },
                { "phone",  Phone}
            };
            return userData;
        }

        /*public static User CreateNew(DocumentSnapshot snapshot)
        {
            Dictionary<string, object> companyValues = snapshot.GetValue<Dictionary<string, object>>("company");
            Dictionary<string, object> addressValue = snapshot.GetValue<Dictionary<string, object>>("address");
            Dictionary<string, object> geoValues = (Dictionary<string, object>)addressValue["geo"];

            return new User()
            {
                Name = snapshot.GetValue<string>("name"),
                Email = snapshot.GetValue<string>("email"),
                Phone = snapshot.GetValue<string>("phone"),
                UserName = snapshot.GetValue<string>("username"),
                Website = snapshot.GetValue<string>("website"),
                Company = new CompanyData
                {
                    BS = (string)companyValues["bs"],
                    CatchPhrase = (string)companyValues["catchPharse"],
                    Name = (string)companyValues["name"]
                },
                Address = new AddressData
                {
                    City = (string)addressValue["city"],
                    Street = (string)addressValue["street"],
                    Suite = (string)addressValue["suite"],
                    ZipCode = (string)addressValue["zipCode"],
                    Geo = new Location
                    {
                        Lat = (double)geoValues["lat"],
                        Lng = (double)geoValues["lng"]
                    }
                }
            };
        }*/
    }
}
