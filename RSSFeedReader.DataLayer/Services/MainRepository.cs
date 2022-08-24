using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RSSFeedReader.DataLayer.Models;

namespace RSSFeedReader.DataLayer.Services
{
    public class MainRepository
    {
        private string FilePath { get; set; }
        public MainRepository()
        {
            FilePath = Path.GetFullPath("./Context/Users.json");
        }

        private async Task<List<User>> GetAllUsers()
        {
            var file = await File.ReadAllTextAsync(FilePath);
            var model = JsonConvert.DeserializeObject<List<User>>(file);
            return await Task.FromResult(model);
        }
        public int GetCountOfUsers()
        {
            return GetAllUsers().Result.Count;
        }
        public List<User> GetActiveUsers()
        {
            var model = GetAllUsers().Result;
            model = model.FindAll(a => a.Status == UserStatus.Active);

            return model;
        }
        public bool AddUser(User user)
        {
            try
            {
                if (IsExists(user.Id))
                    return false;

                var list = GetAllUsers().Result;
                list.Add(user);
                string json = JsonConvert.SerializeObject(list.ToArray());
                File.WriteAllText(FilePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ActiveUser(long userId)
        {
            try
            {
                if (!IsExists(userId))
                    return false;

                var list = GetAllUsers().Result;
                var user = list.Find(a => a.Id == userId);
                user.Status = UserStatus.Active;
                string json = JsonConvert.SerializeObject(list.ToArray());
                File.WriteAllText(FilePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool InActiveUser(long userId)
        {
            try
            {
                if (!IsExists(userId))
                    return false;

                var list = GetAllUsers().Result;
                var user = list.Find(a => a.Id == userId);
                user.Status =UserStatus.Inactive;
                string json = JsonConvert.SerializeObject(list.ToArray());
                File.WriteAllText(FilePath, json);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IsExists(long userId)
        {
            return GetAllUsers().Result.Exists(a => a.Id == userId);
        }
    }
}
