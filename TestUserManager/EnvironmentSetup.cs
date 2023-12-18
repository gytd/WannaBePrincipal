using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WannaBePrincipal.Controllers;
using WannaBePrincipal.Models;

namespace TestUserManager
{
    [CollectionDefinition("ControllerCollection")]
    public class MyTestCollection : ICollectionFixture<EnvironmentSetup>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    internal class EnvironmentSetup : IAsyncLifetime
    {

        private readonly Mock<ILogger<UserRepository>> _loggerMock = new();
        private readonly UserRepositorySettings _us = new() { ProjectString = "wannabe-unit-test", CollectionName = "users" };
        private readonly IOptions<UserRepositorySettings> _userRepoSettings;
        public EnvironmentSetup()
        {
            _userRepoSettings = Options.Create(_us);
        }

        public async Task InitializeAsync()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Environment.CurrentDirectory + "/../../../../WannaBePrincipal/keys/google_credentials_unit_test.json");

            UserRepository userRepository = new(_userRepoSettings, _loggerMock.Object);
            await userRepository.DeleteCollection("users");
        }

        public async Task DisposeAsync()
        {
            UserRepository userRepository = new(_userRepoSettings, _loggerMock.Object);
            await userRepository.DeleteCollection("users");
        }
    }
}
