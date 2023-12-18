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
    public class GetUsersFromDBTests : BaseRepositoryTest
    {
        [Fact]
        public async Task GetAllUserRepository_ReturnsUserList()
        {
            // Arrange
            await _userRepository.DeleteCollection("users");
            User user1 = UserFactory.CreateRandomUser();
            _ = await _userRepository.AddUser(user1);
            User user2 = UserFactory.CreateRandomUser();
            _ = await _userRepository.AddUser(user2);

            // Act
            var users = await _userRepository.GetAllUsers();

            // Assert
            Assert.Equal(2, users.Count);
        }
    }
}
