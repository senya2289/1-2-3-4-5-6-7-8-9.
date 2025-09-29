using System;

namespace NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int secretNumber = random.Next(1, 11); // От 1 до 10 включительно

            try
            {
                Console.Write("Угадайте число от 1 до 10: ");
                int guess = Convert.ToInt32(Console.ReadLine());

                if (guess == secretNumber)
                {
                    Console.WriteLine("Поздравляем! Вы угадали число.");
                }
                else
                {
                    Console.WriteLine($"Не угадали. Загаданное число было {secretNumber}.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено не число. Программа завершена.");
            }
        }
    }
}