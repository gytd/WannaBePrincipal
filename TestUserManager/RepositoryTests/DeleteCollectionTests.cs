using WannaBePrincipal.Models;

namespace TestUserManager.RepositoryTests
{
    [Collection("ControllerCollection")]
    public class DeleteCollectionTests : BaseRepositoryTest
    {
        [Fact]
        public async Task DeleteCollectionRepository_ClearCollection()
        {
            // Arrange
            User user = UserFactory.CreateRandomUser();
            string newId = await _userRepository.AddUser(user);
            User? userResult = await _userRepository.GetUser(newId);
            Assert.NotNull(userResult);

            // Act
            await _userRepository.DeleteCollection("users");
            var userList = await _userRepository.GetAllUsers();

            // Assert
            Assert.Empty(userList);
        }
    }
}
