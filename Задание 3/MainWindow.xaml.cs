using System;
using System.Windows;
using System.Windows.Controls;

namespace Задание_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Добавление задачи в список
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string description = TaskDescriptionTextBox.Text;
            if (!string.IsNullOrWhiteSpace(description) && ActionSelector.SelectedItem is ComboBoxItem selectedAction)
            {
                Action<string> taskAction = null;

                switch (selectedAction.Content.ToString())
                {
                    case "Отправить уведомление":
                        taskAction = SendNotification;
                        break;
                    case "Записать в журнал":
                        taskAction = WriteToLog;
                        break;
                }

                TaskItem task = new TaskItem(description, taskAction);
                TaskListBox.Items.Add(task);

                TaskDescriptionTextBox.Clear();
                ActionSelector.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Введите описание задачи и выберите действие.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Выполнение выбранной задачи
        private void ExecuteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem is TaskItem selectedTask)
            {
                selectedTask.Execute();
                TaskListBox.Items.Remove(selectedTask);
            }
            else
            {
                MessageBox.Show("Выберите задачу для выполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Действие: Отправка уведомления
        private void SendNotification(string message)
        {
            LogTextBlock.Text = $"Уведомление отправлено: {message}";
        }

        // Действие: Запись в журнал
        private void WriteToLog(string message)
        {
            LogTextBlock.Text = $"Запись в журнал: {message}";
        }
    }
}