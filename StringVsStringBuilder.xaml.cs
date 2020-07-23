using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PerfomanceComparison
{
    public partial class MainWindow
    {
        public void StringVsStringBuilder()
        {
            /*
            //Создаем потоки без всяких TaskFactory для уверенности, что это не старый поток
            new Thread(() => {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var resultList = new List<string>();
                for (int i = 0; i < 100000; i++)
                {
                    resultList.Add(i.ToString());
                    Console.WriteLine("Time: "+stopwatch.ElapsedMilliseconds);
                }
                stopwatch.Stop();
            }).Start();

            new Thread(() => {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var resultList = new List<string>();
                for (int i = 0; i < 100000; i++)
                {
                    resultList.Add(i.ToString());
                    Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds);
                }
                stopwatch.Stop();
            }).Start();
            */
        }
    }
}
