using CinemaManagement.Models;
using CinemaManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace CinemaManagement.ViewModel
{
    class TicketTemp : Ticket
    {
        public string MovieName { get; set; }

    }
    class UpdateAccountViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public DateTime? birthday { get; set; }
        public string gender { get; set; }
        public int genderIndex { get; set; }
        public CommunityToolkit.Mvvm.Input.RelayCommand ClickUpdateCommand { get; set; }

        public ObservableCollection<Ticket> Tickets { get; set; }

        public void UpdateButton_Clicked()
        {
            Debug.WriteLine(password);
            bool result = UserService.updateUser(new User
            {
                Username = username,
                Password = password,
                Gender = gender,
                BirthDate = birthday
            });
            

            if (result == true)
            {
                MessageBox.Show("Update successfully");
            }
            else
            {
                MessageBox.Show("Update failed");
            }
        }
        public UpdateAccountViewModel(int id)
        {
            ClickUpdateCommand = new CommunityToolkit.Mvvm.Input.RelayCommand(UpdateButton_Clicked);

            

            User user = UserService.getUserById(id);
            if (user == null) return;
            username = user.Username;
            birthday = user.BirthDate;
            gender = user.Gender.ToString();
            if(gender.ToLower().IndexOf("female") > 0)
            {
                genderIndex = 1;
            }
            else if (gender.ToLower().IndexOf("male") > 0)
            {
                genderIndex = 0;
            }
            else
            {
                genderIndex = 2;
            }

            Tickets = new ObservableCollection<Ticket>();

/*            Tickets.Add(new Ticket
            {
                Id = 1,
                InvoiceId = 1,
                ScreeningId = 1,
                Seat = 12,
                Price = 122
            });

            Tickets.Add(new Ticket
            {
                Id = 2,
                InvoiceId = 2,
                ScreeningId = 2,
                Seat = 141,
                Price = 122
            });

            Tickets.Add(new Ticket
            {
                Id = 3,
                InvoiceId = 5,
                ScreeningId = 4,
                Seat = 112,
                Price = 122
            });
*/

            Tickets = new ObservableCollection<Ticket>(TicketService.GetTicketsByUserId(id));

        }

        
    }
}
