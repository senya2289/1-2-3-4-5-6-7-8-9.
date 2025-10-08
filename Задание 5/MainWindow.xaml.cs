using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Задание_5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            string input = NumbersTextBox.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Введите хотя бы одно число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Преобразование строки в массив чисел
            int[] numbers = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(n => int.TryParse(n.Trim(), out int num) ? num : 0)
                                 .ToArray();

            // Проверка на пустые значения
            if (numbers.Length == 0)
            {
                MessageBox.Show("Введите корректные числа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Определение метода сортировки
            Action<int[]> sortingMethod = null;

            if (SortMethodSelector.SelectedItem is ComboBoxItem selectedMethod)
            {
                switch (selectedMethod.Content.ToString())
                {
                    case "Сортировка пузырьком":
                        sortingMethod = Sorter.BubbleSort;
                        break;

                    case "Быстрая сортировка":
                        sortingMethod = array => Sorter.QuickSort(array, 0, array.Length - 1);
                        break;
                }

                // Сортировка и вывод результата
                sortingMethod?.Invoke(numbers);
                ResultTextBox.Text = string.Join(", ", numbers);
            }
            else
            {
                MessageBox.Show("Выберите метод сортировки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}