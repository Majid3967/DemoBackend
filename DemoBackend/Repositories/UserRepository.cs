using DemoBackend.Data;
using DemoBackend.Interfaces;
using DemoBackend.Models;

namespace DemoBackend.Repositories
{
    public class UserRepository : IUserInterface
    {
        private DataContext _context;
        public UserRepository(DataContext dataContext) { 
            _context = dataContext;
        }
        public int LastUser()
        {
            User? user = _context.Users.OrderBy(u => u.UserId).LastOrDefault();
            if (user == null)
                return 0;
            return user.UserId;
        }
        public bool AddUser(User user)
        {
            _context.Users.Add(user);  
            return Save();
        }

        public User GetUser(string userEmail)
        {
            return _context.Users.Where(u => u.Email == userEmail).FirstOrDefault();
        }

        public bool UserExist(string userEmail)
        {
            return _context.Users.Any(u =>  u.Email==userEmail);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
