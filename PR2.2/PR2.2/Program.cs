using System;

namespace PrimeNumbers
{
    class Program
    {
        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите количество простых чисел K: ");
                int K = Convert.ToInt32(Console.ReadLine());
                if (K <= 0) throw new Exception("K должно быть положительным.");

                int count = 0; // Счетчик найденных простых чисел
                int number = 2; // Начальное число для проверки
                int numbersPerLine = 0; // Счетчик чисел в строке

                while (count < K)
                {
                    if (IsPrime(number))
                    {
                        Console.Write($"{number,4}");
                        count++;
                        numbersPerLine++;
                        if (numbersPerLine == 10 && count < K)
                        {
                            Console.WriteLine();
                            numbersPerLine = 0;
                        }
                    }
                    number++;
                }
                Console.WriteLine(); // Для завершения вывода
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено некорректное значение.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
