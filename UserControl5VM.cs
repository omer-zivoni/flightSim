using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    class UserControl5VM : INotifyPropertyChanged
    {
        public Program pr;
        public event PropertyChangedEventHandler PropertyChanged;

        public double VM_altimeter
        {
            get { return Program.dataBase.getValue(Program.currentRowIndex, "altimeter"); }
            set { }
        }
        public double VM_airspeed
        {
            get { return Program.dataBase.getValue(Program.currentRowIndex, "airspeed"); }
            set { }
        }
        public string VM_airspeedArm
        {
            get
            {  
                return "Rotate180";
            }
            set { }
        }
        public double VM_oriention
        {
            get { return Program.dataBase.getValue(Program.currentRowIndex, "heading-deg"); }
            set { }
        }
        public double VM_yaw
        {
            get { return Program.dataBase.getValue(Program.currentRowIndex, "side-slip-deg"); }
            set { }
        }
        public double VM_roll
        {
            get { return Program.dataBase.getValue(Program.currentRowIndex, "roll-deg"); }
            set { }
        }
        public double VM_pitch
        {
            get { return Program.dataBase.getValue(Program.currentRowIndex, "pitch-deg"); }
            set { }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public UserControl5VM()
        {
            this.pr = Program.getInstance();
            pr.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);

                };


        }


    }
}
