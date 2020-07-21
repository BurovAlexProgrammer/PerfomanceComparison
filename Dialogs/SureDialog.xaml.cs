using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PerfomanceComparison.Dialogs
{
    public partial class SureDialog : Window
    {
        public enum Result { None, Yes, No}
        public Result result = Result.None;

        /// <summary>
        /// Диалоговое окно спрашивает пользователя текст вопроса title. При закрытии окна ответ остается  в переменной result
        /// </summary>
        /// <param name="title">текст вопроса</param>
        /// <param name="buttonYes">надпись на кнопке Да</param>
        /// <param name="buttonNo">надпись на кнопке Нет</param>
        public SureDialog(string title, string buttonYes = "Да", string buttonNo = "Нет")
        {
            InitializeComponent();
            Label.Content = title;
            ButtonYes.Content = buttonYes;
            ButtonNo.Content = buttonNo;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            result = Result.Yes;
            Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            result = Result.No;
            Close();
        }
    }
}
