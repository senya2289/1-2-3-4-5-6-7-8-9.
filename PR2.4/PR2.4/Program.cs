using System;
using System.Collections.Generic;

namespace ConsonantArray
{
    class Program
    {
        static bool IsConsonant(char c)
        {
            // Согласные буквы русского алфавита
            char[] consonants = { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };
            return Array.IndexOf(consonants, char.ToLower(c)) != -1;
        }

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите размер массива K: ");
                int K = Convert.ToInt32(Console.ReadLine());
                if (K <= 0) throw new Exception("K должно быть положительным.");

                // Русский алфавит (33 буквы)
                char[] alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
                Random random = new Random();
                char[] array = new char[K];

                // Заполнение массива случайными буквами
                for (int i = 0; i < K; i++)
                {
                    array[i] = alphabet[random.Next(alphabet.Length)];
                }

                // Создание массива согласных
                List<char> consonants = new List<char>();
                for (int i = 0; i < K; i++)
                {
                    if (IsConsonant(array[i]))
                        consonants.Add(array[i]);
                }

                // Вывод массивов
                Console.WriteLine("Исходный массив:");
                Console.WriteLine(string.Join(" ", array));
                Console.WriteLine("Массив согласных:");
                Console.WriteLine(consonants.Count > 0 ? string.Join(" ", consonants) : "Согласных нет.");
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
