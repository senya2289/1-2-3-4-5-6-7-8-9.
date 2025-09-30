using System;

namespace MaxElementReplacement
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Инициализация массива
                int[] array = { 5, 12, -3, 8, 19, 4, 0, 15, 7, 10 };
                Console.WriteLine("Исходный массив:");
                Console.WriteLine(string.Join(" ", array));

                // Ввод числа
                Console.Write("Введите целое число: ");
                int newValue = Convert.ToInt32(Console.ReadLine());

                // Поиск индекса максимального элемента
                int maxIndex = 0;
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i] > array[maxIndex])
                        maxIndex = i;
                }

                // Замена максимального элемента
                array[maxIndex] = newValue;

                // Вывод результата
                Console.WriteLine("Массив после замены максимального элемента:");
                Console.WriteLine(string.Join(" ", array));
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено некорректное значение.");
            }
        }
    }
}
