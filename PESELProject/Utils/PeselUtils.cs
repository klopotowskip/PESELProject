using PESELProject.Model;

namespace PESELProject.Utils
{
    public static class PeselUtils
    {
        public const int ValidPeselLength = 11;

        public static readonly int[] ControlSumWeights = { 1, 3, 7, 9 };
        public static bool IsValidPesel(string pesel, out DateTime? dateOfBirth)
        {
            dateOfBirth = null;

            if (pesel == null) return false;
            Console.WriteLine("[" + pesel + "] is not null");
            if (pesel.Length != ValidPeselLength) return false;
            Console.WriteLine("[" + pesel + "] valid length");
            if (!Int64.TryParse(pesel, out _)) return false;

            Console.WriteLine("[" + pesel + "] is numeric");

            dateOfBirth = GetDateOfBirth(pesel);
            Console.WriteLine("[" + pesel + "] date is " + dateOfBirth);
            if (dateOfBirth == null) return false;
            return CheckControlSum(pesel);
        }

        private static bool CheckControlSum(string pesel)
        {
            int sum = 0;

            for(int i = 0; i < 10; i++)
            {
                sum += int.Parse(pesel[i] + "") * ControlSumWeights[i % ControlSumWeights.Length];
            }

            sum += int.Parse(pesel[10] + "");

            Console.WriteLine("[" + pesel + "] sum is " + sum);
            return sum % 10 == 0;

        }

        /**
         * Returns date of birth for a given PESEL number.
         * If date of birth is is invalid or later than today returns null.
         */
        public static DateTime? GetDateOfBirth(string pesel)
        {
            int dayOfBirth = int.Parse(pesel.Substring(4, 2));

            int monthBase = int.Parse(pesel.Substring(2, 2));
            int yearBase = int.Parse(pesel.Substring(0, 2));

            

            int century = 1900;
            if (monthBase > 80) century = 1800; // just in case
            else century += (monthBase / 20) * 100;

            int yearOfBirth = century + yearBase;
            int monthOfBirth = monthBase % 20;

            Console.WriteLine("[" + pesel + "] has date " + yearOfBirth + ", " + monthOfBirth + ", " + dayOfBirth);
            DateTime date;
            try
            {
                date = new DateTime(yearOfBirth, monthOfBirth, dayOfBirth);
                DateTime today = DateTime.Today;

                if (date > today) return null;

                return date;
            } catch
            {
                return null;
            }
        }

        public static Gender GetGender(string pesel)
        {
            return int.Parse(pesel[10] + "") % 2 == 0 ? Gender.FEMALE : Gender.MALE;
        }

    }
}
