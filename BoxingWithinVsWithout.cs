using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GlobalExtension;

namespace PerfomanceComparison
{
    public partial class MainWindow
    {
        public Process BoxingWithinModelApp, BoxingWithoutModelApp;
        public void BoxingWithinVsWithout()
        {
            //Запуск приложений
            BoxingWithinModelApp = Process.Start("Apps\\BoxingWithin\\BoxingWithin.exe");
            BoxingWithoutModelApp = Process.Start("Apps\\BoxingWithout\\BoxingWithout.exe");
            //Слушатели приложений (проверяет адреса памяти приложений)
            taskList.Add(Task.Factory.StartNew(() => {
                Listeners.ListenAppMemory(BoxingWithinModel, "BoxingWithinAppMemory");
            }, cancelTokenSource.Token));
            taskList.Add(Task.Factory.StartNew(() =>
            {
                Listeners.ListenAppMemory(BoxingWithoutModel, "BoxingWithoutAppMemory");
            }, cancelTokenSource.Token));
            //Обновление прогрессбаров
            taskList.Add(Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    CalculateBoxingPerformanceDifference();
                    Thread.Sleep(100);
                }
            }, cancelTokenSource.Token));
        }

        void CalculateBoxingPerformanceDifference()
        {
            Dispatcher.Invoke(delegate ()
            {
                var currVal = progressbar_Boxing_PerformanceDiff.Value;
                progressbar_Boxing_PerformanceDiff.Value =
                (CompareToPercent(BoxingWithinModel.Speed, BoxingWithoutModel.Speed) + currVal * 5) / 6; //*5/6 сглаживание

                currVal = progressbar_Boxing_MemoryDiff.Value;
                progressbar_Boxing_MemoryDiff.Value = (CompareToPercent(BoxingWithinModel.MemoryUsage, BoxingWithoutModel.MemoryUsage) + currVal * 5) / 6;
                progressbar_Boxing_PeakMemoryDiff.Value = CompareToPercent(BoxingWithinModel.PeakMemoryUsed, BoxingWithoutModel.PeakMemoryUsed);
            });
        }
    }
}
