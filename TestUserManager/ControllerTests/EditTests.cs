using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager.ControllerTest
{
    public class EditTests
    {
        private readonly Mock<IUserModel> _userModelMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public EditTests()
        {
            _userModelMock = new Mock<IUserModel>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userModelMock.Object, _loggerMock.Object);
        }

        // Tests for GET Edit

        [Fact]
        public async Task GetEdit_WithNullId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetEdit_WithNonExistingUser_ReturnsNotFound()
        {
            // Arrange
            _ = _userModelMock.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Edit("some-id");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetEdit_WithValidId_ReturnsOk()
        {
            // Arrange
            _userModelMock.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(UserFactory.CreateRandomUser());

            // Act
            var result = await _controller.Edit("valid-id");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Tests for POST Edit

        [Fact]
        public async Task PostEdit_WithNullId_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.Edit(null as string, UserFactory.CreateRandomUser());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostEdit_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "bad format");

            // Act
            var result = await _controller.Edit("valid-id", UserFactory.CreateRandomUser(true));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostEdit_WithEditFailure_ReturnsNotFound()
        {
            // Arrange
            _userModelMock.Setup(x => x.EditUser(It.IsAny<string>(), It.IsAny<User>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Edit("valid-id", UserFactory.CreateRandomUser());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PostEdit_WithSuccessfulEdit_ReturnsOk()
        {
            // Arrange
            _userModelMock.Setup(x => x.EditUser(It.IsAny<string>(), It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Edit("valid-id", UserFactory.CreateRandomUser());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
