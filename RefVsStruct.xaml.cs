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
        public Process RefModelApp, StructModelApp;
        public void RefVsStruct()
        {
            //Запуск приложений
            RefModelApp = Process.Start("Apps\\ReferencesModel\\ReferencesModel.exe");
            StructModelApp = Process.Start("Apps\\StructModel\\StructModel.exe");
            //Слушатели приложений (проверяет адреса памяти приложений)
            taskList.Add(Task.Factory.StartNew(() => {
                Listeners.ListenAppMemory(StructModel, "StructAppMemory");
            }, cancelTokenSource.Token));
            taskList.Add(Task.Factory.StartNew(() =>
            {
                Listeners.ListenAppMemory(ReferencesModel, "RefAppMemory");
            }, cancelTokenSource.Token));
            //Обновление прогрессбаров
            taskList.Add(Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    CalculateStructPerformanceDifference();
                    Thread.Sleep(50);
                }
            }, cancelTokenSource.Token));
        }

        void CalculateStructPerformanceDifference()
        {
            if (cancelTokenSource.Token.IsCancellationRequested) return; //Если задача отменена, то закончить исполнение
            Dispatcher.Invoke(delegate ()
            {
                var currVal = progressbar_Struct_PerformanceDiff.Value;
                progressbar_Struct_PerformanceDiff.Value =
                (CompareToPercent(ReferencesModel.Speed, StructModel.Speed) + currVal * 5) / 6; //*5/6 сглаживание

                currVal = progressbar_Struct_MemoryDiff.Value;
                progressbar_Struct_MemoryDiff.Value = (CompareToPercent(ReferencesModel.MemoryUsage, StructModel.MemoryUsage) + currVal * 5) / 6;
                progressbar_Struct_PeakMemoryDiff.Value = CompareToPercent(ReferencesModel.PeakMemoryUsed, StructModel.PeakMemoryUsed);
            });
        }
    }
    
}
