using System;
using System.Linq;

namespace SortedIndices
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание массива
            Random random = new Random();
            double[] array = new double[10];
            for (int i = 0; i < 10; i++)
            {
                array[i] = random.NextDouble() * 20 - 10; // [-10, 10)
            }

            // Создание массива индексов, отсортированных по значениям
            int[] indices = Enumerable.Range(0, 10)
                .OrderBy(i => array[i])
                .ToArray();

            // Вывод массивов
            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"array[{i}] = {array[i]:F4}");
            }
            Console.WriteLine("Массив индексов (по возрастанию значений):");
            Console.WriteLine(string.Join(" ", indices));
        }
    }
}