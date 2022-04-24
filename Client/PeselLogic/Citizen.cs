namespace Client.PeselLogic
{
    public class Citizen
    {
        public int CitizenId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PESEL { get; set; }
        public string Sex { get; set; }

        public void GetPESEL(int lastIndex)
        {
            var currentPesel = $"{GeneratePESELDateBorn()}{GenaratePESELNumber(lastIndex)}";
            PESEL = $"{currentPesel}{GenerateControlNumber(currentPesel)}";
        }

        private string GeneratePESELDateBorn()
        {
            string yearPart = DateOfBirth.Year.ToString().Substring(2, 2);
            string monthPart = null;
            string dayPart = DateOfBirth.Day.ToString("00");
            var dateTimeYear = DateOfBirth.Year;
            if (dateTimeYear >= 1900 && dateTimeYear <= 1999)
                monthPart = DateOfBirth.Month.ToString("00");
            else if (dateTimeYear >= 2000 && dateTimeYear <= 2099)
            {
                int month = DateOfBirth.Month + 20;
                monthPart = month.ToString("00");
            }
            if (monthPart == null) return null;
            return $"{yearPart}{monthPart}{dayPart}";
        }

        private string GenaratePESELNumber(int lastIndex)
        {
            string number = (lastIndex + 1).ToString("000");
            string sexDigit = Sex == "female" ? GenerateEven() : GenerateOdd();
            return $"{number}{sexDigit}";
        }

        private string GenerateControlNumber(string currentPESEL)
        {
            List<int> wages = new List<int>() { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            List<int> values = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                values.Add(int.Parse(currentPESEL.Substring(i, 1)) * wages[i]);
            }

            List<int> digits = new List<int>();
            foreach (var item in values)
            {
                if (item.ToString().Length == 1)
                    digits.Add(item);
                else
                    digits.Add(int.Parse(item.ToString().Substring(1, 1)));
            }

            var sum = digits.Sum();
            if (sum.ToString().Length != 1)
                sum = int.Parse(sum.ToString().Substring(1, 1));

            return (10 - sum).ToString();
        }

        private string GenerateEven()
        {
            Random random = new Random();
            int number = random.Next(1, 10);
            while (number % 2 != 0)
                number = random.Next(1, 10);

            return number.ToString();
        }

        private string GenerateOdd()
        {
            Random random = new Random();
            int number = random.Next(1, 10);
            while (number % 2 == 0)
                number = random.Next(1, 10);

            return number.ToString();
        }
    }
}
