using System;

namespace PalindromeChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string input = Console.ReadLine();

            // Очистка: убираем пробелы и приводим к нижнему регистру
            string cleaned = input.Replace(" ", "").ToLower();

            // Переворачиваем строку
            char[] charArray = cleaned.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            if (cleaned == reversed)
            {
                Console.WriteLine($"Строка \"{input}\" является палиндромом.");
            }
            else
            {
                Console.WriteLine($"Строка \"{input}\" не является палиндромом.");
            }
        }
    }
}
