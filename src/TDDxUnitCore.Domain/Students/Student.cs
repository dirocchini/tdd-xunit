using System.Text.RegularExpressions;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Domain.Students
{
    public class Student : Entity
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; set; }
        public Audience Audience { get; private set; }

        public Student(string name, string document, string email, Audience audience)
        {
            Name = name;
            Document = document;
            Email = email;
            Audience = audience;

            Validate();
        }

        private void Validate()
        {
            RulerValidator.New()
                .When(string.IsNullOrEmpty(Name), Resources.InvalidName)
                .When(!IsCpf(Document), Resources.InvalidDocument)
                .When(!IsValidEmail(Email), Resources.InvalidEmail)
                .ThrowException();
        }




        public void ChangeName(string name)
        {
            Name = name;
        }






        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }


        private static bool IsCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
