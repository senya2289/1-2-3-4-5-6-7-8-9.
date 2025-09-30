using System;

namespace CitySearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Массив из 5 городов (можно изменить)
            string[] cities = { "Москва", "Санкт-Петербург", "Казань", "Новосибирск", "Екатеринбург" };

            Console.Write("Введите название города: ");
            string inputCity = Console.ReadLine();

            // Поиск индекса
            int index = Array.IndexOf(cities, inputCity);

            if (index != -1)
            {
                Console.WriteLine($"Город \"{inputCity}\" найден в массиве по индексу {index}.");
            }
            else
            {
                Console.WriteLine($"Город \"{inputCity}\" не найден в массиве.");
            }
        }
    }
}
