namespace WannaBePrincipal.Models
{
    public class UserFactory
    {
        private static readonly Random RandomGenerator = new();

        public static User CreateRandomUser(bool isInvalid = false)
        {
            // Generate random data for the User
            #region data for generation
            string[] names = [
                "Leanne Graham",
                "Ervin Howell",
                "Clementine Bauch",
                "Patricia Lebsack",
                "Chelsey Dietrich",
                "Mrs. Dennis Schulist",
                "Kurtis Weissnat",
                "Nicholas Runolfsdottir V",
                "Glenna Reichert",
                "Clementina DuBuque"
            ];

            string[] usernames = [
                "Bret",
                "Antonette",
                "Samantha",
                "Karianne",
                "Kamren",
                "Leopoldo_Corkery",
                "Elwyn.Skiles",
                "Maxime_Nienow",
                "Delphine",
                "Moriah.Stanton"
            ];

            string[] emails = [
                "Sincere@april.biz",
                "Shanna@melissa.tv",
                "Nathan@yesenia.net",
                "Julianne.OConner@kory.org",
                "Lucio_Hettinger@annie.ca",
                "Karley_Dach@jasper.info",
                "Telly.Hoeger@billy.biz",
                "Sherwood@rosamond.me",
                "Chaim_McDermott@dana.io",
                "Rey.Padberg@karina.biz"
            ];

            string[] streets = [
                "Kulas Light",
                "Victor Plains",
                "Douglas Extension",
                "Hoeger Mall",
                "Skiles Walks",
                "Norberto Crossing",
                "Rex Trail",
                "Ellsworth Summit",
                "Dayna Park",
                "Kattie Turnpike"
            ];
            
            string[] suites = [
                "Apt. 556",
                "Suite 879",
                "Suite 847",
                "Apt. 692",
                "Suite 351",
                "Apt. 950",
                "Suite 280",
                "Suite 729",
                "Suite 449",
                "Suite 198"
            ];

            string[] cities = [
                "Gwenborough",
                "Wisokyburgh",
                "McKenziehaven",
                "South Elvis",
                "Roscoeview",
                "South Christy",
                "Howemouth",
                "Aliyaview",
                "Bartholomebury",
                "Lebsackbury"
            ];

            string[] zipcodes = [
                "92998-3874",
                "90566-7771",
                "59590-4157",
                "53919-4257",
                "33263",
                "23505-1337",
                "58804-1099",
                "45169",
                "76495-3109",
                "31428-2261"
            ];

            string[] phoneNumbers = [
                "1-770-736-8031 x56442",
                "010-692-6593 x09125",
                "1-463-123-4447",
                "493-170-9623 x156",
                "(254)954-1289",
                "1-477-935-8478 x6430",
                "210.067.6132",
                "586.493.6943 x140",
                "(775)976-6794 x41206",
                "024-648-3804"
            ];

            string[] websites = [
                "hildegard.org",
                "anastasia.net",
                "ramiro.info",
                "kale.biz",
                "demarco.info",
                "ola.org",
                "elvis.io",
                "jacynthe.com",
                "conrad.com",
                "ambrose.net"
            ];

            string[] companyNames = [
                "Romaguera-Crona",
                "Deckow-Crist",
                "Romaguera-Jacobson",
                "Robel-Corkery",
                "Keebler LLC",
                "Considine-Lockman",
                "Johns Group",
                "Abernathy Group",
                "Yost and Sons",
                "Hoeger LLC"
            ];

            string[] catchPhrases = [
                "Multi-layered client-server neural-net",
                "Proactive didactic contingency",
                "Face to face bifurcated interface",
                "Multi-tiered zero tolerance productivity",
                "User-centric fault-tolerant solution",
                "Synchronised bottom-line interface",
                "Configurable multimedia task-force",
                "Implemented secondary concept",
                "Switchable contextually-based project",
                "Centralized empowering task-force"
            ];

            string[] companyBs = [
                "harness real-time e-markets",
                "synergize scalable supply-chains",
                "e-enable strategic applications",
                "transition cutting-edge web services",
                "revolutionize end-to-end systems",
                "e-enable innovative applications",
                "generate enterprise e-tailers",
                "e-enable extensible e-tailers",
                "aggregate real-time technologies",
                "target end-to-end models"
            ];
            #endregion

            string randomName = names[RandomGenerator.Next(names.Length)];
            string randomEmail = emails[RandomGenerator.Next(emails.Length)];
            string randomPhone = phoneNumbers[RandomGenerator.Next(phoneNumbers.Length)];
            string randomUserName = usernames[RandomGenerator.Next(usernames.Length)];
            string randomWebsite = websites[RandomGenerator.Next(websites.Length)];
            string randomCitie = cities[RandomGenerator.Next(cities.Length)];
            string randomStreet = streets[RandomGenerator.Next(streets.Length)];
            string randomSuite = suites[RandomGenerator.Next(suites.Length)];
            string randomZipCose = zipcodes[RandomGenerator.Next(zipcodes.Length)];
            string randomBs = companyBs[RandomGenerator.Next(companyBs.Length)];
            string randomPhase = catchPhrases[RandomGenerator.Next(catchPhrases.Length)];
            string randomCName = companyNames[RandomGenerator.Next(companyNames.Length)];
            double randomLat = RandomGenerator.NextDouble() * 360 - 180;
            double randomLon = RandomGenerator.NextDouble() * 360 - 180;


            return new User
            {
                Name = randomName,
                Email = isInvalid ? "very invalid email" : randomEmail,
                Phone = randomPhone,
                UserName = randomUserName,
                Website = randomWebsite,
                Address = new AddressData
                {
                    City = randomCitie,
                    Street = randomStreet,
                    Suite = randomSuite,
                    ZipCode = randomZipCose,
                    Geo = new Location
                    {
                        Lat = randomLat,
                        Lng = randomLon
                    }
                },
                Company = new CompanyData
                {
                    BS = randomBs,
                    CatchPhrase = randomPhase,
                    Name = randomCName
                },
            };
        }
    }
}
