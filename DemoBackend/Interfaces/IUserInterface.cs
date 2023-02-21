using DemoBackend.Models;

namespace DemoBackend.Interfaces
{
    public interface IUserInterface
    {
        public int LastUser();
        public bool AddUser(User user);
        public bool UserExist(string userEmail);
        public User GetUser(string userEmail);
        public bool Save();
    }
}
