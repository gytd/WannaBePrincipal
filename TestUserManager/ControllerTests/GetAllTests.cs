using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager.ControllerTest
{
    public class GetAllTests
    {
        private readonly Mock<IUserModel> _userModelMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public GetAllTests()
        {
            _userModelMock = new Mock<IUserModel>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userModelMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            // Arrange
            _userModelMock.Setup(x => x.GetAllUsers()).ReturnsAsync([]);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Empty(users);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfUsers()
        {
            // Arrange
            var mockUsers = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                mockUsers.Add(UserFactory.CreateRandomUser());
            }

            _userModelMock.Setup(x => x.GetAllUsers()).ReturnsAsync(mockUsers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Equal(mockUsers.Count, users.Count);
        }

        [Fact]
        public async Task GetAll_ThrowsException()
        {
            // Arrange
            _userModelMock.Setup(x => x.GetAllUsers()).ThrowsAsync(new Exception("Some error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.GetAll());
        }

    }
}
