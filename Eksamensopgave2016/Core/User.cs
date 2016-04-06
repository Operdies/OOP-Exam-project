//20154304_Alexander_Nørøskov_Larsen


using System;
using System.Linq;
using System.Text;

namespace Eksamensopgave2016.Core
{
    class User : IComparable<User>
    {
        private static int NextID = 1;
        public int UserID { get; private set; }
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            private set {
                InputValidation.ValidateName(value);
                _firstName = value;
            }
        }

        private string _lastName;

        public string LastName {
            get { return _lastName; }
            private set
            {
                InputValidation.ValidateName(value);
                _lastName = value;
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

        public string Email { get; private set; }
        public decimal BalanceDecimal { get; private set; }

        public int CompareTo(User other)
        {
            return this.UserID.CompareTo(other.UserID);
        }

        public override string ToString()
        {
            string result = $"{FirstName} {LastName} ({Email})";
            return result;
        }

        public override bool Equals(object obj)
        {
            User other = obj as User;
            return other != null && this.UserID == other.UserID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public User(string firstName, string lastName, string email, string username)
        {
            UserID = NextID++;
            Email = email.ToLower();
            FirstName = firstName;
            LastName = lastName;
        }

        private string FormatName(string name)
        {
            string newName = char.ToUpper(name[0]) + name.Substring(1);
            return newName;
        }
    }
}