using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WannaBePrincipal.Models;

namespace TestUserManager.RepositoryTests
{
    public abstract class BaseRepositoryTest
    {
        protected readonly UserRepository _userRepository;
        protected readonly Mock<ILogger<UserRepository>> _loggerMock;
        protected readonly UserRepositorySettings _us;
        protected readonly IOptions<UserRepositorySettings> _userRepositorySettings;

        protected BaseRepositoryTest()
        {
            _loggerMock = new Mock<ILogger<UserRepository>>();
            _us = new() { ProjectString = "wannabe-unit-test", CollectionName = "users" };
            _userRepositorySettings = Options.Create(_us);
            _userRepository = new UserRepository(_userRepositorySettings, _loggerMock.Object);
        }
    }
}
