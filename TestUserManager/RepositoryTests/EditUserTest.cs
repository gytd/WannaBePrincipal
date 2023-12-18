using WannaBePrincipal.Models;

namespace TestUserManager.RepositoryTests
{
    [Collection("ControllerCollection")]
    public class EditUserTest : BaseRepositoryTest
    {
        [Fact]
        public async Task EditUserRepository_WithExistingUser_ReturnsTrue()
        {
            // Arrange
            User user = UserFactory.CreateRandomUser();
            string existingId = await _userRepository.AddUser(user);
            User editUser = UserFactory.CreateRandomUser();

            // Act
            bool result = await _userRepository.EditUser(existingId, editUser);
            User? userResult = await _userRepository.GetUser(existingId);
            User userFromDB;

            // Assert
            Assert.True(result);
            if (userResult != null)
            {
                userFromDB = userResult;
                Assert.Equal(editUser, userFromDB, new UserEqualityComparer());
            }
            else
            {
                Assert.Fail("Returned with null value.");
            }
        }

        [Fact]
        public async Task EditUserRepository_WIthNonExistingUser_ReturnsFalse()
        {
            // Arrange
            User editUser = UserFactory.CreateRandomUser();

            // Act
            bool result = await _userRepository.EditUser("invalid user id", editUser);

            // Assert
            Assert.True(!result);
        }
    }
}

