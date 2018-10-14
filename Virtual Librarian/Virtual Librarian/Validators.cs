using System.Text.RegularExpressions;

namespace Virtual_Librarian
{
    class Validators
    {
        public static bool IsValidISBN(string isbn)
        {
            isbn = isbn.Replace("-", "");
            if (Regex.IsMatch(isbn, @"^(97(8|9))?\d{9}(\d|X)$") == false)
            {
                return false;
            }

            switch (isbn.Length)
            {
                case 10:
                    return IsValidISBN10(isbn);
                //break;
                case 13:
                    return IsValidISBN13(isbn);
                default:
                    return false;

            }

        }

        public static bool IsValidISBN10(string isbn)
        {
            long sum = 0;
            if (isbn?.Length != 10 ||
                !long.TryParse(isbn?.Substring(0, 9), out long unused))
            {
                return false;
            }

            for (int i = 0; i < 9; i++)
            {
                sum += long.Parse(isbn[i].ToString()) * (10 - i);
            }

            if (isbn[9] == 'X')
            {
                sum += 10 * 10;
            }
            else if (long.TryParse(isbn[9].ToString(), out unused))
            {
                sum += long.Parse(isbn[9].ToString());
            }
            else
            {
                return false;
            }

            return sum % 11 == 0;
        }

        public static bool IsValidISBN13(string isbn)
        {
            long sum = 0;
            int lastnum;

            if (isbn?.Length != 13 ||
            !long.TryParse(isbn?.Substring(0, 12), out long unused))
            {
                return false;
            }

            for (int i = 0; i < 12; i++)
            {
                int mult = i % 2 == 0 ? 1 : 3;
                sum += long.Parse(isbn[i].ToString()) * mult;
            }
            if (sum % 10 == 0)
            {
                lastnum = 0;
            }
            else
            {
                lastnum = 10 - (int)(sum % 10);
            }

            return lastnum == int.Parse(isbn[12].ToString());
        }
    }
}

