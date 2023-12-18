using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WannaBePrincipal.Models;

namespace TestUserManager.RepositoryTests
{
    [Collection("ControllerCollection")]
    public class AddUserTest : BaseRepositoryTest
    {
        [Fact]
        public async Task AddUserRepository_ReturnsNewId()
        {
            // Arrange
            User user = UserFactory.CreateRandomUser();

            // Act
            string newId = await _userRepository.AddUser(user);
            User? userResult = await _userRepository.GetUser(newId);
            User userFromDB;

            // Assert
            Assert.NotEmpty(newId);
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
