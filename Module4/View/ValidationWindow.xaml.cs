using Module4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Module4.View
{
    /// <summary>
    /// Логика взаимодействия для ValidationWindow.xaml
    /// </summary>
    public partial class ValidationWindow : Window
    {
        ServerRequest request = new ServerRequest();
        WordWriter writer = new WordWriter();
        public ValidationWindow()
        {
            InitializeComponent();
        }

        private async void GetDataButtonClick(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/fullName";
            string result = await request.getRequestAsync(url);
            DataTextBlock.Text = getFullName(result);
        }

        private string getFullName(string result)
        {
            return result.Substring(result.IndexOf(":") + 2).Replace("\"", "").Replace("}", "");
        }

        private bool isContainingUnknownChars(string text)
        {
            return Regex.IsMatch(text, @"[^а-яА-ЯёЁ0-9\s]");
        }

        private void SendResultClickButton(object sender, RoutedEventArgs e)
        {
            bool hasUnknownChars = isContainingUnknownChars(DataTextBlock.Text);
            string result = "";
            string validationResult = hasUnknownChars ? "ФИО содержит запрещённые символы" : "Ошибок нет";
            ResultTextBlock.Text = validationResult;

            if (validationResult.Equals("ФИО содержит запрещённые символы"))
            {
                result = "Не успешно";
            }
            else if (validationResult.Equals("Ошибок нет"))
            {
                result = "Успешно";
            }
            
            string filePath = "C:\\Users\\austr\\OneDrive\\Рабочий стол\\ДЕМО\\Module4\\Module4\\ТестКейс.docx";
            int tableIndex = 1;
            int columnIndex = 3;
            int rowIndex = 3;

            writer.writeToWordTable(filePath, tableIndex, rowIndex, columnIndex, result);
        }
    }
}
