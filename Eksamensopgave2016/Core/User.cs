//20154304_Alexander_Nørskov_Larsen


using System;
using System.Linq;
using System.Text;

namespace Eksamensopgave2016.Core
{
    delegate void UserBalanceNotifications(User user, decimal Balance);

    public class User : IComparable<User>
    {
        private static int NextID = 1;
        public int UserID { get; }
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            private set
            {
                InputValidation.ValidateName(value);
                _firstName = FormatName(value);
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            private set
            {
                InputValidation.ValidateName(value);
                _lastName = FormatName(value);
            }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            private set
            {
                InputValidation.ValidateUsername(value);
                _username = value;
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            private set
            {
                InputValidation.ValidateEmail(value);
                _email = value.ToLower();
            }
        }

        public decimal BalanceDecimal { get; private set; }

        public int CompareTo(User other)
        {
            return this.UserID.CompareTo(other.UserID);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Email})";
        }

        public override bool Equals(object obj)
        {
            User other = obj as User;
            return other != null && this.UserID == other.UserID;
        }

        public override int GetHashCode()
        {
            //mock implementation
            return UserID.GetHashCode() ^ Username.GetHashCode();
        }

        public User(string firstName, string lastName, string email, string username)
        {
            UserID = NextID++;
            Email = email.ToLower().Trim();
            FirstName = firstName;
            LastName = lastName;
        }

        private string FormatName(string oldName)
        {
            char[] name = oldName.ToCharArray();
            if (name.Length >= 1)
                name[0] = char.ToUpper(name[0]);
            

            for (int index = 1; index < oldName.Length; index++)
            {
                if (name[index - 1] == ' ')
                    name[index] = char.ToUpper(name[index]);
                else
                    name[index] = char.ToLower(name[index]);
            }
            return new string(name);
        }
    }
}