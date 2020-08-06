using PerfomanceComparison.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
using static GlobalExtension;

namespace PerfomanceComparison 
{
    public partial class MainWindow : Window
    {
        public static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public static List<Task> taskList = new List<Task>();
        
        public MainWindow mainWindow;

        public ProcessState StringModel = new ProcessState() { Title = "String" };
        public ProcessState StringBuilderModel = new ProcessState() { Title = "StringBuilder" };

        public ProcessState BoxingWithinModel = new ProcessState() { Title = "Boxing within" };
        public ProcessState BoxingWithoutModel = new ProcessState() { Title = "Boxing without" };
        public MainWindow()
        {
            mainWindow = this;
            InitializeComponent();
            //String vs StringBuilder
            StringContent.DataContext = StringModel;
            StringBuilderContent.DataContext = StringBuilderModel;
            Panel_StringPerformance.Collapse();
            Panel_StringMemoryUsed.Collapse();

            BoxingWithinContent.DataContext = BoxingWithinModel;
            BoxingWithoutContent.DataContext = BoxingWithoutModel;
            Panel_BoxingPerformance.Collapse();
            Panel_BoxingMemoryUsed.Collapse();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == ButtonQuit)
                SureQuit();

            if (sender == ButtonStartTestStringVsStringBuilder)
            {
                Panel_StringPerformance.Show();
                Panel_StringMemoryUsed.Show();
                ButtonStartTestStringVsStringBuilder.Hide();
                StringVsStringBuilder();
            }

            if (sender == ButtonStartTestBoxingWithinVsWithout)
            {
                Panel_BoxingPerformance.Show();
                Panel_BoxingMemoryUsed.Show();
                ButtonStartTestBoxingWithinVsWithout.Hide();
                BoxingWithinVsWithout();
            }
        }

        private void currWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Завершить все задачи и процессы
            cancelTokenSource.Cancel();
            StringModelApp?.Kill();
            StringBuilderModelApp?.Kill();
            BoxingWithinModelApp?.Kill();
            BoxingWithoutModelApp?.Kill();
        }

        /// <summary>
        /// Получение информации о состоянии приложения из общей памяти
        /// </summary>
        public static partial class Listeners
        {
            public static void ListenAppMemory(ProcessState processState, string AppMemoryName)
            {
                var procInfoSize = Marshal.SizeOf<MyProcessInfo>();
                while (true)
                {
                    try
                    {
                        MemoryMappedFile sharedMemory = MemoryMappedFile.OpenExisting(AppMemoryName);
                        while (true)
                        {
                            using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(0, procInfoSize, MemoryMappedFileAccess.Read))
                            {
                                MyProcessInfo output;
                                reader.Read<MyProcessInfo>(0, out output);
                                processState.ExecuteCount = output.executeCount;
                                processState.ExecuteTime = output.executeTime;
                                processState.MemoryUsed = output.memoryUsed;
                                processState.PeakMemoryUsed = output.peakMemoryUsed;
                                Thread.Sleep(200);
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Exception: " + exc.Message);
                        Thread.Sleep(2000);
                    }
                }
            }
        }

    }
}
