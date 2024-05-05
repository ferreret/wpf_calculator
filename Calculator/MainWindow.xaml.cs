using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        Operation? operation;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += acButton_Click;
            negativeButton.Click += negativeButton_Click;
            percentageButton.Click += percentageButton_Click;
            equalButton.Click += equalButton_Click;
            pointButton.Click += pointButton_Click;
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains('.'))
                resultLabel.Content += ".";
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (operation)
                {
                    case Operation.Add:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case Operation.Subtract:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case Operation.Multiply:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case Operation.Divide:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;

            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber /= 100;
                if (lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void negativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void acButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            lastNumber = 0;
            result = 0;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = selectedValue.ToString();
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue.ToString()}";
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplyButton)
                operation = Operation.Multiply;
            
            if (sender == divisionButton)
                operation = Operation.Divide;
            
            if (sender == plusButton)
                operation = Operation.Add;
            
            if (sender == minusButton)
                operation = Operation.Subtract;
            
        }   
    }
    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class SimpleMath
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                MessageBox.Show("Division by zero is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return a / b;
        }
    }
}