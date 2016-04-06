//20154304_Alexander_Nørøskov_Larsen


using System;
using System.Linq;

namespace Eksamensopgave2016.InputValidation
{
    static class InputValidation
    {
        public static void ValidateName(string name)
        {
            CheckNullOrWhiteSpace(name);
            if (ContainsDigits(name))
                throw new ArgumentException("Name must not contain digits");
            
        }

        public static void ValidateUsername(string username)
        {
            
            if (username.Where(ch => !char.IsLetterOrDigit(ch)).Any(ch => ch != '_'))
                throw new ArgumentException("Username must only contain letters, numbers and underscores (\"_\")!");
        }

        public static void ValidateEmail(string email)
        {
            CheckNullOrWhiteSpace(email);
            ValidateSpecialCharacters(email);
            
        }

        private static void ValidateSpecialCharacters(string email)
        {
            if (email.Count(ch => ch == '@') != 1)
                throw new ArgumentException("Email address must contain exactly one \"@\" symbol");
            string localPart = GetLocalPart(email);
            string domain = GetDomain(email, localPart.Length);
            ValidateLocalPart(localPart);
            ValidateDomain(domain);
        }

        private static string GetDomain(string email, int localLength)
        {
            return email.Substring(localLength + 1);
        }

        private static string GetLocalPart(string email)
        {
            return email.TakeWhile(ch => ch != '@').Aggregate("", (current, ch) => current + ch);
        }


        private static void ValidateLocalPart(string local)
        {
            foreach (char ch in local)
            {
                if (char.IsLetterOrDigit(ch)) continue;
                if (IsLegalLocalPartCharacter(ch)) continue;
                throw new ArgumentException($"{ch} is an illegal symbol!");
            }
            
        }

        private static bool IsLegalLocalPartCharacter(char ch)
        {
            if (IsLegalDomainCharacter(ch))
                return true;
            return ch == '_' || ch == ',';
        }

        private static void ValidateDomain(string domain)
        {
            TestFirstAndLastSymbol(domain);
            if (domain.Count(ch => ch == '.') != 1)
                throw new ArgumentException("Domain must contain exactly one full stop symbol (\".\")");
            foreach (char ch in domain)
            {
                if (char.IsLetterOrDigit(ch)) continue;
                if (IsLegalDomainCharacter(ch)) continue;
                throw new ArgumentException($"{ch} is an illegal character");
            }
            
        }

        private static bool IsLegalDomainCharacter(char ch)
        {
            bool result;
            switch (ch)
            {
                case '.':
                    result = true;
                    break;
                case '-':
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        private static void TestFirstAndLastSymbol(string domain)
        {
            if (domain.StartsWith(".") || domain.StartsWith("-") || domain.EndsWith(".") || domain.EndsWith("-"))
                throw new ArgumentException("Domain must not start or end with a full stop or a dash (\".\" or \"-\")");
        }

        private static bool ContainsDigits(string str)
        {
            return str.Any(char.IsDigit);
        }

        private static void CheckNullOrWhiteSpace(string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException("String {0} must not be null or empty", nameof(str));
        }
    }
}
