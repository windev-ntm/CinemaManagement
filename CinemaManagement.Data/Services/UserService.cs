using CinemaManagement.Data.Models;

namespace CinemaManagement.Data.Services
{
    public class UserService
    {
        public int GetNumberOfUsers()
        {
            using var context = new CinemaManagementContext();
            return context.Users.Count();
        }
    }
}
