using System.ComponentModel;

namespace CinemaManagement.Entity
{
    internal class UserEntity : INotifyPropertyChanged
    {
        private int _id;
        private string _username;
        private string _password;
        private DateTime _birthDate;
        private string _gender;

        public string getPassword()
        {
            return _password;
        }
        public DateTime getBirthDate()
        {
            return _birthDate;
        }
        public string getGender()
        {
            return _gender;
        }
        public int getId()
        {
            return _id;
        }
        public string getUsername()
        {
            return _username;

        }


        public void setUsername(string username)
        {
            _username = username;
            OnPropertyChanged(nameof(_username));

        }
        public void setPassword(string password)
        {
            _password = password;
            OnPropertyChanged(nameof(_password));
        }
        public void setBirthDate(DateTime birthDate)
        {
            _birthDate = birthDate;
            OnPropertyChanged(nameof(_birthDate));

        }
        public void setGender(string gender)
        {
            _gender = gender;
            OnPropertyChanged(nameof(_gender));

        }
        public void setId(int id)
        {
            _id = id;
            OnPropertyChanged(nameof(_id));

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
