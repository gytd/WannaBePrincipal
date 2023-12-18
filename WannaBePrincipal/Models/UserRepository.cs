using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WannaBePrincipal.Models
{
    public class UserRepository(IOptions<UserRepositorySettings> settings, ILogger<UserRepository> logger) : IUserModel
    {
        private readonly UserRepositorySettings _settings = settings.Value;
        private readonly ILogger<UserRepository> _logger = logger;

        /// <summary>
        /// Add new user to a db.
        /// </summary>
        /// <param name="user">Document with data of the entry.</param>
        /// <returns>Returns with the new user ID.</returns>
        public async Task<string> AddUser(User user)
        {
            FirestoreDb db = FirestoreDb.Create(_settings.ProjectString);
            CollectionReference collRef = db.Collection(_settings.CollectionName);
            DocumentReference newUser = await collRef.AddAsync(user.ToDictionary());
            _logger.LogInformation("New user created with id: {id}", newUser.Id);

            return newUser.Id;
        }

        /// <summary>
        /// Returns with 1 user's data.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>Returns with true if the user is exists.</returns>
        public async Task<User?> GetUser(string userID)
        {
            FirestoreDb db = FirestoreDb.Create(_settings.ProjectString);
            DocumentReference docRef = db.Collection(_settings.CollectionName).Document(userID.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                _logger.LogWarning("GetUser called with invalid user id: {id}", userID);
                return null;
            }
            else
            {
                Dictionary<string, object> docData = snapshot.ToDictionary();
                string json = JsonConvert.SerializeObject(docData);
                User user = JsonConvert.DeserializeObject<User>(json) ?? throw new DataMisalignedException("Something wrong in db.");

                _logger.LogInformation("User queried with id: {id}", userID);
                return user;
            }
        }

        /// <summary>
        /// Edit user's data in db.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <param name="user">Document with data of the entry.</param>
        /// <returns>Returns with true if the user is exists.</returns>
        public async Task<bool> EditUser(string userID, User user)
        {
            FirestoreDb db = FirestoreDb.Create(_settings.ProjectString);
            DocumentReference docRef = db.Collection(_settings.CollectionName).Document(userID.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                _logger.LogWarning("EditUser called with invalid user id: {id}", userID);
                return false;
            }
            else
            {
                _ = await docRef.SetAsync(user.ToDictionary());
                _logger.LogInformation("User data modified with id: {id}", userID);

                return true;
            }
        }

        /// <summary>
        /// Delete user's data in db.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>Returns with true if the user was existed.</returns>
        public async Task<bool> DeleteUser(string userID)
        {
            FirestoreDb db = FirestoreDb.Create(_settings.ProjectString);
            DocumentReference docRef = db.Collection(_settings.CollectionName).Document(userID.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return false;
            }
            else
            {
                _ = await docRef.DeleteAsync();
                _logger.LogInformation("User deleted with id: {id}", userID);

                return true;
            }
        }

        /// <summary>
        /// List all users.
        /// </summary>
        /// <returns>A <see cref="CollectionReference"/> with all the users.</returns>
        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = [];

            FirestoreDb db = FirestoreDb.Create(_settings.ProjectString);
            Query allCitiesQuery = db.Collection(_settings.CollectionName);
            QuerySnapshot allCitiesQuerySnapshot = await allCitiesQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Dictionary<string, object> docData = documentSnapshot.ToDictionary();
                string json = JsonConvert.SerializeObject(docData);
                User user = JsonConvert.DeserializeObject<User>(json) ?? throw new DataMisalignedException("Something wrong in db.");

                users.Add(user);
            }

            _logger.LogInformation("All the users queried. There are {count} user in the db.", users.Count);
            return users;
        }

        /// <summary>
        /// Delete all users.
        /// </summary>
        public async Task DeleteCollection(string collectionName, int batchSize = 150)
        {
            FirestoreDb db = FirestoreDb.Create(_settings.ProjectString);
            CollectionReference collectionReference = db.Collection(collectionName);
            QuerySnapshot snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
            IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;

            while (documents.Count > 0)
            {
                foreach (DocumentSnapshot document in documents)
                {
                    await document.Reference.DeleteAsync();
                }
                snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
                documents = snapshot.Documents;
            }

            _logger.LogInformation("Finished deleting all documents from the {collectionName} collection.", collectionName);
        }
    }
}
