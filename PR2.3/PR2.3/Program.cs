using System;

namespace MinMaxRange
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Ввод параметров
                Console.Write("Введите размер массива K: ");
                int K = Convert.ToInt32(Console.ReadLine());
                if (K <= 0) throw new Exception("K должно быть положительным.");

                Console.Write("Введите начало диапазона A: ");
                int A = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите конец диапазона B: ");
                int B = Convert.ToInt32(Console.ReadLine());
                if (A >= B) throw new Exception("A должно быть меньше B.");

                // Создание массива
                Random random = new Random();
                int[] array = new int[K];
                for (int i = 0; i < K; i++)
                {
                    array[i] = random.Next(A, B); // [A, B)
                }

                Console.WriteLine("Исходный массив:");
                Console.WriteLine(string.Join(" ", array));

                // Поиск индексов min и max
                int minIndex = 0, maxIndex = 0;
                for (int i = 1; i < K; i++)
                {
                    if (array[i] < array[minIndex]) minIndex = i;
                    if (array[i] > array[maxIndex]) maxIndex = i;
                }

                // Определение границ для вывода
                int start = Math.Min(minIndex, maxIndex);
                int end = Math.Max(minIndex, maxIndex);

                // Вывод элементов между min и max (включая)
                Console.WriteLine("Элементы между минимальным и максимальным (включая):");
                for (int i = start; i <= end; i++)
                {
                    Console.Write(array[i] + " ");
                }
                Console.WriteLine();
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
