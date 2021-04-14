using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Simulator
{

    internal class ControllerVM : INotifyPropertyChanged
    {
        private ControllerM controllerM;

        public event PropertyChangedEventHandler PropertyChanged;

        public int VM_currentRowIndex
        {
            get { return controllerM.CurrentRowIndex; }
            set
            {
                controllerM.CurrentRowIndex = value;
            }
        }

        public string VM_playString
        {
            get
            {
                return controllerM.PlayString;
            }
            set { controllerM.PlayString = value; }
        }


        public string VM_timeString
        {
            get
            {
                return controllerM.TimeString;
            }

        }

        public int VM_lastLine
        {
            get { return controllerM.LastLine; }

        }

        public string VM_fgPath
        {
            get { return controllerM.fgPath; }
            set { controllerM.fgPath = value; }
        }


        public bool VM_isInitialize
        {
            get { return controllerM.isInitialize; }
            set { controllerM.isInitialize = value; }
        }



        public double VM_playSpeed
        {
            get { return controllerM.PlaySpeed; }
            set { controllerM.PlaySpeed = value; }
        }

        public ControllerVM(ControllerM controllerM)
        {
            this.controllerM = controllerM;
            controllerM.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public void playNot()
        {
            if (VM_playString.Equals("play"))
                VM_playString = "stop";
            else
                VM_playString = "play";
        }
    }
}
