using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PerfomanceComparison
{
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
    }
}
