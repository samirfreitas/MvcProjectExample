using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Business.Models.Validations.Docs
{
    public class CpfValidation
    {
        public const int LenghtCpf = 11;

        public static bool Validation(string cpf)
        {
            var cpfNumbers = Utils.OnlyNumbers(cpf);

            if (!ValidLenght(cpfNumbers)) return false;
            return !HasRepeatedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
        }

        private static bool ValidLenght(string value)
        {
            return value.Length == LenghtCpf;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, LenghtCpf - 2);
            var verifyingDigit = new VerifyingDigit(number)
                .WithMultipliersOf(2, 11)
                .Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculatesDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculatesDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(LenghtCpf - 2, 2);
        }
    }

    public class CnpjValidation
    {
        public const int LenghtCnpj = 14;

        public static bool Validation(string cpnj)
        {
            var cnpjNumbers = Utils.OnlyNumbers(cpnj);

            if (!HasValidLenght(cnpjNumbers)) return false;
            return !HasRepeatedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
        }

        private static bool HasValidLenght(string value)
        {
            return value.Length == LenghtCnpj;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string valor)
        {
            var number = valor.Substring(0, LenghtCnpj - 2);

            var verifyingDigit = new VerifyingDigit(number)
                .WithMultipliersOf(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = verifyingDigit.CalculatesDigit();
            verifyingDigit.AddDigit(firstDigit);
            var secondDigit = verifyingDigit.CalculatesDigit();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(LenghtCnpj - 2, 2);
        }
    }

    public class VerifyingDigit
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _replacements = new Dictionary<int, string>();
        private bool _complementaryModule = true;

        public VerifyingDigit(string number)
        {
            _number = number;
        }

        public VerifyingDigit WithMultipliersOf(int firstMultiplier, int lastMultiplier)
        {
            _multipliers.Clear();
            for (var i = firstMultiplier; i <= lastMultiplier; i++)
                _multipliers.Add(i);

            return this;
        }

        public VerifyingDigit Replacing(string substitute, params int[] digits)
        {
            foreach (var i in digits)
            {
                _replacements[i] = substitute;
            }
            return this;
        }

        public void AddDigit(string digits)
        {
            _number = string.Concat(_number, digits);
        }

        public string CalculatesDigit()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var product = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                sum += product;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (sum % Module);
            var resultado = _complementaryModule ? Module - mod : mod;

            return _replacements.ContainsKey(resultado) ? _replacements[resultado] : resultado.ToString();
        }
    }

    public class Utils
    {
        public static string OnlyNumbers(string valor)
        {
            var onlyNumber = "";
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }


}
