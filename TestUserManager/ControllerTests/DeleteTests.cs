using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager.ControllerTest
{
    public class DeleteTests
    {
        private readonly Mock<IUserModel> _userModelMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public DeleteTests()
        {
            _userModelMock = new Mock<IUserModel>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userModelMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Delete_WithNullId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Delete(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_WithValidId_DeletesUser()
        {
            // Arrange
            _userModelMock.Setup(x => x.DeleteUser(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete("valid-id");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithNonExistingUser_ReturnsNotFound()
        {
            // Arrange
            _userModelMock.Setup(x => x.DeleteUser(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete("non-existing-id");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Problem occurred while editing the non-existing-id user.", notFoundResult.Value);
        }
    }
}
