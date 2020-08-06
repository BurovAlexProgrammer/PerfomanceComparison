using PerfomanceComparison.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

public static class GlobalExtension
{
    public static void Show(this Control control)
    {
        control.Visibility = System.Windows.Visibility.Visible;
    }
    public static void Hide(this Control control)
    {
        control.Visibility = System.Windows.Visibility.Hidden;
    }
    public static void Collapse(this Control control)
    {
        control.Visibility = System.Windows.Visibility.Collapsed;
    }
    public static void Show(this Panel control)
    {
        control.Visibility = System.Windows.Visibility.Visible;
    }
    public static void Hide(this Panel control)
    {
        control.Visibility = System.Windows.Visibility.Hidden;
    }
    public static void Collapse(this Panel control)
    {
        control.Visibility = System.Windows.Visibility.Collapsed;
    }

    /// <summary>
    /// Выдать пользователю диалоговое окно 'Уверены, что хотите выйти?'. Выходит при подтверждении.
    /// </summary>
    public static void SureQuit()
    {
        var dialog = new SureDialog("Уверены, что хотите выйти?");
        dialog.ShowDialog();
        if (dialog.result == SureDialog.Result.Yes)
            Quit();
    }

    /// <summary>
    /// Закрыть приложение
    /// </summary>
    public static void Quit()
    {
        System.Windows.Application.Current.Shutdown();
    }

    /// <summary>
    /// Сравнивает два числа и возвращает разницу от 100%. Т.е.  2.CompareTo(3) = 40%  ->  2/(2+3) * 100
    /// afterPoint - количество знаков после запятой
    /// </summary>
    /// <returns></returns>
    public static double CompareToPercent<T>(T _a, T _b, int afterPoint = 0)
    {
        if (_a == null | _b == null) throw new Exception("Exception: CompareToPercent cannot convert to decimal");
        var a = Convert.ToDouble(_a);
        var b = Convert.ToDouble(_b);
        if ((a + b) == 0) return 0;
        double result = 1f * a / (a + b) * 100;
        return Math.Round(result, afterPoint);
    }
}
