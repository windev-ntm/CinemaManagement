using CinemaManagement.Data.Models;

namespace CinemaManagement.Data.Services
{
    public class AdminService
    {
        public bool SignIn(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                using var db = new CinemaManagementContext();

                var user = db.Admins.FirstOrDefault(u => u.Username == username);

                if (user is null) return false;

                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                    return false;

                return true;
            }
            catch (System.Exception) { }
            return false;
        }
    }
}
