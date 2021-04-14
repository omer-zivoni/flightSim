using System;
using System.ComponentModel;
using System.Threading;

namespace Simulator
{
    internal class ControllerM : INotifyPropertyChanged
    {
        public Program pr;

        public event PropertyChangedEventHandler PropertyChanged;

        private double playSpeed;
        public double PlaySpeed
        {
            get { return pr.playSpeed; }
            set { pr.playSpeed = value; }
        }

        public string TimeString
        {
            get
            {
                int totalSec = CurrentRowIndex / 10;
                int sec = totalSec % 60;
                int min = totalSec / 60;
                return min + ":" + sec;
            }
        }

        private int lastLine;

        public int LastLine
        {
            get { return lastLine; }
        }

        public string fgPath
        {
            get { return pr.fgPath; }
            set { pr.fgPath = value; }
        }

        public int CurrentRowIndex
        {
            get { return Program.currentRowIndex; }
            set
            {
                if (Program.currentRowIndex != value)
                {
                    if (value < 0) { Program.currentRowIndex = 0; }
                    else if (value > LastLine) { Program.currentRowIndex = lastLine; }
                    else { Program.currentRowIndex = value; }
                    this.NotifyPropertyChanged("CurrentRowIndex");
                    this.NotifyPropertyChanged("Sec"); //??
                    this.NotifyPropertyChanged("TimeString");
                }
            }
        }
        public string PlayString
        {
            get
            {
                return pr.playString;

            }
            set
            {
                pr.playString = value;
                this.NotifyPropertyChanged("PlayString");


            }
        }


        public bool isInitialize
        {
            get
            {
                return pr.isInitialize;

            }
            set {  }
        }


        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public ControllerM(Program pr)
        {
            this.pr = pr;

            pr.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged(e.PropertyName);
                    NotifyPropertyChanged("TimeString");
                };

            lastLine = pr.getRowsNumber();        

            Thread thr = new Thread(new ThreadStart(this.pr.func));
            thr.Start();


        }
    }
}
