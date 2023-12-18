using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager.ControllerTest
{
    public class CreateTests
    {
        private readonly Mock<IUserModel> _userModelMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public CreateTests()
        {
            _userModelMock = new Mock<IUserModel>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userModelMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_WithValidUserData_ReturnsCreatedResult()
        {
            // Arrange
            var validUserData = UserFactory.CreateRandomUser();
            _userModelMock.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync("some-id");
            _controller.ModelState.Clear();

            // Act
            var result = await _controller.Create(validUserData);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal("some-id", createdResult.Location);
            Assert.Equal(validUserData, createdResult.Value);
        }

        [Fact]
        public async Task Create_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var invalidUserData = UserFactory.CreateRandomUser(true);
            _controller.ModelState.AddModelError("error", "bad format");

            // Act
            var result = await _controller.Create(invalidUserData);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
