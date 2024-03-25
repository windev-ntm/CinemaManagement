using CinemaManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
namespace CinemaManagement.Services
{
    public class UserService
    {

        static CinemaManagementContext cinemaManagementContext = new CinemaManagementContext();

        public async Task<User> SignIn(string username, string password)
        {
            try
            {
                if (username == null || password == null)
                {
                    MessageBox.Show("Username or password is empty");
                    return null ;
                }

                using (var db = new CinemaManagementContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);

                    if (user == null)
                    {
                        MessageBox.Show("Username not found");
                        return null;
                    }
                    if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        MessageBox.Show("Password is incorrect");
                        return null;
                    }
                    return user;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return null;
        }
        public bool CreateUser(User user)
        {
            try
            {
                if (user.Username == null || user.Password == null)
                {
                    MessageBox.Show("Username or password is empty");
                    return false;
                }
                Regex regexPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()-_+=])[A-Za-z\d!@#$%^&*()-_+=]{8,}$");

                if (!regexPassword.IsMatch(user.Password))
                {
                    MessageBox.Show("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");
                    return false;
                }

                if (user.BirthDate == null)
                {
                    MessageBox.Show("Birthdate is empty");
                    return false;
                }
                if (user.Gender == null)
                {
                    MessageBox.Show("Gender is empty");
                    return false;
                }

                MessageBox.Show(user.Gender);

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.BirthDate = user.BirthDate.Value.ToUniversalTime();


                using (var db = new CinemaManagementContext())
                {
                    var isUsernameTaken = db.Users.Any(u => u.Username == user.Username);

                    if (isUsernameTaken)
                    {
                        MessageBox.Show("Username is already taken");
                        return false;
                    }



                    user.Id = db.Users.Count() + 1;
                    db.Users.Add(user);
                    db.SaveChanges();
                    MessageBox.Show("Sign up successfully");
                }
            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Debug.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Debug.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        public bool updateUser()
        {
            return true;
        }
        public bool deleteUser()
        {
            return true;
        }
        public User getUserById(string id)
        {
            return null;
        }
        public User getUserByUsername(string username)
        {
            return null;
        }
        public bool isUser(User user)
        {
            return true;
        }
        public bool FindUser(string username, string password)
        {
            return true;
        }
    }
}
