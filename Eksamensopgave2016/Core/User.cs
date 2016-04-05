//20154304_Alexander_Nørøskov_Larsen


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
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
                if (value == null)
                    throw new InvalidNameException();
                _firstName = value;
            }
        }

        private string _lastName;

        public string LastName {
            get { return _lastName; }
            private set
            {
                if (value == null)
                    throw new InvalidNameException();
                _lastName = value;
            }
        }
        public string Username { get; private set; }
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
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public User(string firstName, string lastName, string email, string username)
        {
            if (validateEmail(email) == false)
                throw new InvalidEmailException();
            if (validateUsername(username) == false)
                throw new InvalidUsernameException();
            
            UserID = NextID++;
            Email = email;
            
        }

        private bool validateEmail(string email)
        {
            StringBuilder sb = new StringBuilder();
            string localPart = "";
            if (email.Contains('@'))
                foreach (char ch in email)
                {
                    if (ch == '@')
                        break;
                    sb.Append(ch);
                }
            localPart = sb.ToString();
            //string localPart = email.TakeWhile(ch => ch.Equals('@') == false).ToString();
            return default(bool);
        }

        private bool validateUsername(string username)
        {
            return default(bool);
        }
    }
    internal class InvalidEmailException : Exception
    {

    }

    internal class InvalidUsernameException : Exception
    {
    }

    internal class InvalidNameException : Exception
    {

    }
}