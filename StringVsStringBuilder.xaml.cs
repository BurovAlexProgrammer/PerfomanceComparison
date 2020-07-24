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
        public void StringVsStringBuilder()
        {
            StringModelApp = Process.Start("Apps\\StringModel\\StringModel.exe");
            StringBuilderModelApp = Process.Start("Apps\\StringBuilderModel\\StringBuilderModel.exe");

            taskList.Add(Task.Factory.StartNew(() => {
                Listeners.StringApp(StringModel);
            }, cancelTokenSource.Token));
            taskList.Add(Task.Factory.StartNew(() =>
            {
                Listeners.StringBuilderApp(StringBuilderModel);
            }, cancelTokenSource.Token));
            taskList.Add(Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    CalculateStringPerformanceDifference();
                    Thread.Sleep(200);
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

    /// <summary>
    /// Получение информации о состоянии приложения из общей памяти
    /// </summary>
    public static partial class Listeners
    {
        public static void StringApp(ProcessState processState)
        {
            var procInfoSize = Marshal.SizeOf<MyProcessInfo>();
            while (true)
            {
                try
                {
                    MemoryMappedFile sharedMemory = MemoryMappedFile.OpenExisting("StringAppMemory");
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
                    Console.WriteLine("Exception: "+ exc.Message);
                    Thread.Sleep(2000);
                }
            }
        }

        public static void StringBuilderApp(ProcessState processState)
        {
            var procInfoSize = Marshal.SizeOf<MyProcessInfo>();
            while (true)
            {
                try
                {
                    MemoryMappedFile sharedMemory = MemoryMappedFile.OpenExisting("StringBuilderAppMemory");
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
                    Thread.Sleep(2000);
                }
            }
        }
    }

    
}
