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
        private int _executeCount;
        private int _executeTime;
        private long _memoryUsed;
        private long _peakMemoryUsed;
        //Название модели
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        //Количество выполненных операций
        public int ExecuteCount
        {
            get { return _executeCount; }
            set
            {
                _executeCount = value;
                NotifyPropertyChanged("ExecuteCount");
            }
        }
        //Затраченное время на выполнение операций
        public int ExecuteTime
        {
            get { return _executeTime; }
            set
            {
                _executeTime = value;
                NotifyPropertyChanged("ExecuteTime");
            }
        }
        //Используемая память
        public long MemoryUsed
        {
            get { return _memoryUsed; }
            set
            {
                _memoryUsed = value;
                NotifyPropertyChanged("MemoryUsed");
            }
        }
        //Пиковая используемая память
        public long PeakMemoryUsed
        {
            get { return _peakMemoryUsed; }
            set
            {
                _peakMemoryUsed = value;
                NotifyPropertyChanged("PeakMemoryUsed");
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
