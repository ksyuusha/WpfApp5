using System;
using System.Linq;
using System.Windows;

namespace SortingApp
{
    public delegate int[] SortAlgorithm(int[] numbers);

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработка нажатия на кнопку "Сортировать"
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный метод сортировки
            SortAlgorithm sortMethod = GetSelectedSortMethod();
            if (sortMethod == null)
            {
                MessageBox.Show("Выберите метод сортировки.");
                return;
            }

            // Получаем введенные числа
            string input = InputNumbersTextBox.Text;
            int[] numbers;

            // Пробуем преобразовать введенные данные в массив чисел
            try
            {
                numbers = input.Split(',')
                               .Select(n => int.Parse(n.Trim()))
                               .ToArray();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные целые числа через запятую.");
                return;
            }

            // Сортируем числа
            int[] sortedNumbers = sortMethod(numbers);

            // Отображаем отсортированные числа
            SortedNumbersTextBlock.Text = "Отсортированные числа: " + string.Join(", ", sortedNumbers);
        }

        // Получение выбранного метода сортировки
        private SortAlgorithm GetSelectedSortMethod()
        {
            if (SortMethodComboBox.SelectedIndex == 0) // Сортировка пузырьком
            {
                return BubbleSort;
            }
            else if (SortMethodComboBox.SelectedIndex == 1) // Быстрая сортировка
            {
                return QuickSort;
            }
            return null;
        }

        // Метод сортировки пузырьком
        private int[] BubbleSort(int[] numbers)
        {
            int[] sortedArray = (int[])numbers.Clone();
            int n = sortedArray.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (sortedArray[j] > sortedArray[j + 1])
                    {
                        // Обмен
                        int temp = sortedArray[j];
                        sortedArray[j] = sortedArray[j + 1];
                        sortedArray[j + 1] = temp;
                    }
                }
            }
            return sortedArray;
        }

        // Метод быстрой сортировки
        private int[] QuickSort(int[] numbers)
        {
            int[] sortedArray = (int[])numbers.Clone();
            QuickSortAlgorithm(sortedArray, 0, sortedArray.Length - 1);
            return sortedArray;
        }

        private void QuickSortAlgorithm(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                QuickSortAlgorithm(array, left, pivotIndex - 1);
                QuickSortAlgorithm(array, pivotIndex + 1, right);
            }
        }

        private int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {

                if (array[j] < pivot)
                {
                    i++;
                    // Обмен
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            // Обмен с опорным элементом
            int temp1 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp1;
            return i + 1;
        }
    }
}
