namespace PESELProject.Utils
{
    public static class TimeUtils
    {
        public static int GetPersonAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;

            int yearsDifference = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Month > today.Month) yearsDifference -= 1;
            else if(dateOfBirth.Month == today.Month && dateOfBirth.Day > today.Day) yearsDifference -= 1;

            return yearsDifference;
        }

        public static string GetDiscountRate(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;

            if(dateOfBirth.Month == today.Month)
            {
                if (dateOfBirth.Day == today.Day) return "10%";
                else return "5%";

            }

            return today.Month >= 2 && today.Month <= 9 ? "2,5%" : "0%";
        }
    }
}
