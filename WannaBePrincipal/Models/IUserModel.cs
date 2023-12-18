namespace WannaBePrincipal.Models
{
    public interface IUserModel
    {
        Task<string> AddUser(User user);
        Task<User?> GetUser(string userID);
        Task<bool> EditUser(string userID, User user);
        Task<bool> DeleteUser(string userID);
        Task<List<User>> GetAllUsers();
        Task DeleteCollection(string collectionName, int batchSize = 100);
    }
}