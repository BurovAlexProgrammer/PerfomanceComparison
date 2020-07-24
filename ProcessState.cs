using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PerfomanceComparison
{
    public class ProcessState : INotifyPropertyChanged
    {
        private string _title;
        private long _executeCount = 0;
        private long _executeTime = 0;
        private long _memoryUsed = 0;
        private long _peakMemoryUsed = 0;
        /// <summary>
        /// Название модели
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        /// <summary>
        /// Количество выполненных операций
        /// </summary>
        public long ExecuteCount
        {
            get { return _executeCount; }
            set
            {
                _executeCount = value;
                NotifyPropertyChanged("ExecuteCount");
            }
        }
        /// <summary>
        /// Затраченное время на выполнение операций
        /// </summary>
        public long ExecuteTime
        {
            get { return _executeTime; }
            set
            {
                _executeTime = value;
                NotifyPropertyChanged("ExecuteTime");
            }
        }
        /// <summary>
        /// Используемая память
        /// </summary>
        public long MemoryUsed
        {
            get { return _memoryUsed; }
            set
            {
                _memoryUsed = value;
                NotifyPropertyChanged("MemoryUsed");
            }
        }
        /// <summary>
        /// Пиковая используемая память
        /// </summary>
        public long PeakMemoryUsed
        {
            get { return _peakMemoryUsed; }
            set
            {
                _peakMemoryUsed = value;
                NotifyPropertyChanged("PeakMemoryUsed");
            }
        }
        /// <summary>
        /// Расчет скорости выполнения итерации
        /// </summary>
        public float Speed
        {
            get
            {
                return 1f * ExecuteCount / Math.Max(1, ExecuteTime);
            }
        } 
        /// <summary>
        /// Рациональность использования памяти
        /// </summary>
        public float MemoryUsage
        {
            get
            {
                return 1f * ExecuteCount/ Math.Max(1, MemoryUsed);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
