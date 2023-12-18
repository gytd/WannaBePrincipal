using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WannaBePrincipal.Models;

namespace TestUserManager.RepositoryTests
{
    [Collection("ControllerCollection")]
    public  class GetUserTests : BaseRepositoryTest
    {
        [Fact]
        public async Task GetUserRepository_GetUser()
        {
            // Arrange
            User user = UserFactory.CreateRandomUser();
            string newId = await _userRepository.AddUser(user);

            // Act
            User? userResult = await _userRepository.GetUser(newId);
            User userFromDB;

            // Assert
            if (userResult != null)
            {
                userFromDB = userResult;
                Assert.Equal(user, userFromDB, new UserEqualityComparer());
            }
            else
            {
                Assert.Fail("Returned with null value.");
            }
        }
    }
}
