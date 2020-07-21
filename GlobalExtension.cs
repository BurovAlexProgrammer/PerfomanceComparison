using PerfomanceComparison.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

public static class GlobalExtension
{
    public static void Show(this Control control)
    {
        control.Visibility = System.Windows.Visibility.Hidden;
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
        control.Visibility = System.Windows.Visibility.Hidden;
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



}
