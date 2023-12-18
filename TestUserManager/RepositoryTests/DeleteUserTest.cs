using WannaBePrincipal.Models;

namespace TestUserManager.RepositoryTests
{
    [Collection("ControllerCollection")]
    public class DeleteUserTest : BaseRepositoryTest
    {
        [Fact]
        public async Task DeleteUserRepository_WithExistingUser_ReturnsTrue()
        {
            // Arrange
            User user = UserFactory.CreateRandomUser();
            string existingId = await _userRepository.AddUser(user);

            // Act
            bool result = await _userRepository.DeleteUser(existingId);
            User? userResult = await _userRepository.GetUser(existingId);

            // Assert
            Assert.True(result);
            Assert.Null(userResult);
        }

        [Fact]
        public async Task DeleteUserRepository_WithNonExistingUser_ReturnsFalse()
        {
            // Arrange

            // Act
            bool result = await _userRepository.DeleteUser("invalid user id");

            // Assert
            Assert.True(!result);
        }
    }
}
