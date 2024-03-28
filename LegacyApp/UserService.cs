using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameNotCorrect(firstName) || IsLastNameNotCorrect(lastName))
            {
                return false;
            }

            if (IsEmailNotCorrect(email))
            {
                return false;
            }

            var age = CalculatingAgeTillNowUsingBirthdate(dateOfBirth);

            if (age < 21)
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            SetUserCreditLimitByUserImportance(client, user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static void SetUserCreditLimitByUserImportance(Client client, User user)
        {
            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                {
                    using (var userCreditService = new UserCreditService())
                    {
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        user.CreditLimit = creditLimit * 2;
                    }

                    break;
                }
                default:
                {
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditService())
                    {
                        user.CreditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    }

                    break;
                }
            }
        }

        private static int CalculatingAgeTillNowUsingBirthdate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }

        private static bool IsEmailNotCorrect(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return true;
            }

            return false;
        }

        private static bool IsLastNameNotCorrect(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstNameNotCorrect(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}
