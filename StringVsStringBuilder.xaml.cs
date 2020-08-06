using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using static GlobalExtension;

namespace PerfomanceComparison
{
    public partial class MainWindow
    {
        public Process StringModelApp, StringBuilderModelApp;
        public void StringVsStringBuilder()
        {
            //Запуск приложений
            StringModelApp = Process.Start("Apps\\StringModel\\StringModel.exe");
            StringBuilderModelApp = Process.Start("Apps\\StringBuilderModel\\StringBuilderModel.exe");
            //Слушатели приложений (проверяет адреса памяти приложений)
            taskList.Add(Task.Factory.StartNew(() => {
                Listeners.ListenAppMemory(StringModel, "StringAppMemory");
            }, cancelTokenSource.Token));
            taskList.Add(Task.Factory.StartNew(() =>
            {
                Listeners.ListenAppMemory(StringBuilderModel, "StringBuilderAppMemory");
            }, cancelTokenSource.Token));
            //Обновление прогрессбаров
            taskList.Add(Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    CalculateStringPerformanceDifference();
                    Thread.Sleep(100);
                }
            }, cancelTokenSource.Token));
        }

        void CalculateStringPerformanceDifference()
        {           
            Dispatcher.Invoke(delegate ()
            {
                var currVal = progressbar_StringVsStringBuilder_PerformanceDiff.Value;
                progressbar_StringVsStringBuilder_PerformanceDiff.Value =
                (CompareToPercent(StringModel.Speed,StringBuilderModel.Speed) + currVal * 5) / 6; //*5/6 сглаживание

                currVal = progressbar_StringVsStringBuilder_MemoryDiff.Value;
                progressbar_StringVsStringBuilder_MemoryDiff.Value = (CompareToPercent(StringModel.MemoryUsage,StringBuilderModel.MemoryUsage) + currVal * 5) / 6;
                progressbar_StringVsStringBuilder_PeakMemoryDiff.Value = CompareToPercent(StringBuilderModel.PeakMemoryUsed,StringModel.PeakMemoryUsed);
        });
        }
    }
    
}
