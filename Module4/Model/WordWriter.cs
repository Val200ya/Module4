using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace Module4.Model
{
    public class WordWriter
    {
        public void writeToWordTable(string filePath, int tableIndex, int rowIndex, int columnIndex, string data)
        {
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                Word.Document wordDocument = wordApp.Documents.Open(filePath);
                Word.Table wordTable = wordDocument.Tables[tableIndex];

                if (columnIndex < 1 || columnIndex > wordTable.Columns.Count)
                {
                    throw new ArgumentException("Неверный индекс колонки.");
                }
                if (rowIndex < 1 || rowIndex > wordTable.Rows.Count)
                {
                    throw new ArgumentException("Неверный индекс строки.");
                }

                wordTable.Cell(rowIndex, columnIndex).Range.Text = data;

                wordDocument.Save();
                wordDocument.Close();
                wordApp.Quit();

                MessageBox.Show("Данные были успешно добавлены в таблицу!", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
