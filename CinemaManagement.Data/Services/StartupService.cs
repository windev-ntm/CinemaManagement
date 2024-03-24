using CinemaManagement.Data.Models;

namespace CinemaManagement.Data.Services
{
    public class StartupService
    {
        public static void CompileModel()
        {
            using var context = new CinemaManagementContext();
            //_ = context.Model;
            context.Database.EnsureCreated();
        }
    }
}
