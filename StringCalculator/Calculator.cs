namespace StringCalculator
{
    public class Calculator
    {
        /// <summary>
        /// Складывает числа, разделенные запятыми или переносами строк.
        /// Поддерживает пользовательские разделители.
        /// </summary>
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            var delimiters = new List<string> { ",", "\n" };

            // Проверка на пользовательский разделитель
            if (numbers.StartsWith("//"))
            {
                var parts = numbers.Split('\n', 2);
                var customDelimiter = parts[0].Substring(2);
                delimiters.Add(customDelimiter);
                numbers = parts[1];
            }

            // Разбиваем строку по всем разделителям
            var splitNumbers = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);

            int sum = 0;
            var negatives = new List<int>();

            foreach (var numStr in splitNumbers)
            {
                if (string.IsNullOrEmpty(numStr))
                    continue;

                if (int.TryParse(numStr, out int number))
                {
                    if (number < 0)
                        negatives.Add(number);

                    if (number <= 1000) // Игнорируем числа > 1000
                        sum += number;
                }
            }

            if (negatives.Any())
                throw new ArgumentException($"Отрицательные числа запрещены: {string.Join(", ", negatives)}");

            return sum;
        }
    }
}