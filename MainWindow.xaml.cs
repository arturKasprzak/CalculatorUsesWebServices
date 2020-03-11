using AK_Calculator.Model;
using AK_Calculator.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AK_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculatorSoapClient calculator;
        List<COper> operationList;

        public MainWindow()
        {
            InitializeComponent();

            calculator = new ServiceReference.CalculatorSoapClient();
            operationList = new List<COper>();

            RefreshList();
        }

        private void RefreshList()
        {
            lvLista.ItemsSource = null;
            lvLista.ItemsSource = operationList;
        }

        private void btnMultiply_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(editA.Text) || String.IsNullOrWhiteSpace(editB.Text)) || (String.IsNullOrWhiteSpace(editA.Text) && String.IsNullOrWhiteSpace(editB.Text)))
            {
                MessageBox.Show("Proszę wpisać liczby.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            int a;
            int b;

            if (int.TryParse(editA.Text, out a) && int.TryParse(editB.Text, out b))
            {
                var wynik = calculator.Multiply(a, b);

                editWynik.Text = wynik.ToString();

                operationList.Add(new COper() { NumberA = a, NumberB = b, Result = wynik, Operation = "Multiply" });

                RefreshList();
            }
            else
            {
                MessageBox.Show("Niepoprawne liczby.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(editA.Text) || String.IsNullOrWhiteSpace(editB.Text)) || (String.IsNullOrWhiteSpace(editA.Text) && String.IsNullOrWhiteSpace(editB.Text)))
            {
                MessageBox.Show("Proszę wpisać liczby.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            int a;
            int b;

            if (int.TryParse(editA.Text, out a) && int.TryParse(editB.Text, out b))
            {
                var wynik = calculator.Subtract(a, b);

                editWynik.Text = wynik.ToString();

                operationList.Add(new COper() { NumberA = a, NumberB = b, Result = wynik, Operation = "Minus" });

                RefreshList();
            }
            else
            {
                MessageBox.Show("Niepoprawne liczby.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(editA.Text) || String.IsNullOrWhiteSpace(editB.Text)) || (String.IsNullOrWhiteSpace(editA.Text) && String.IsNullOrWhiteSpace(editB.Text)))
            {
                MessageBox.Show("Proszę wpisać liczby.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            int a;
            int b;

            if (int.TryParse(editA.Text, out a) && int.TryParse(editB.Text, out b))
            {
                var wynik = calculator.Add(a,b);

                editWynik.Text = wynik.ToString();

                operationList.Add(new COper() { NumberA = a, NumberB = b, Result = wynik, Operation = "Add" });

                RefreshList();
            }
            else
            {
                MessageBox.Show("Niepoprawne liczby.","Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (operationList.Count < 1)
            {
                return;
            }

            
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(operationList.GetType());
            x.Serialize(Console.Out, operationList);

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//operationResults.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            x.Serialize(file, operationList);
            file.Close();
        }
    }
}
