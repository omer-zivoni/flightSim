using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simulator
{
    class JoystickVM : INotifyPropertyChanged
    {

        public double VM_throttle
        {
            get
            {

                return 340 * Program.dataBase.getValue(Program.currentRowIndex, "throttle");
            }

            set { }
        }
        public double VM_rudder
        {
            get { return 340 * Program.dataBase.getValue(Program.currentRowIndex, "rudder"); }
            set { }
        }
        public double VM_aileron
        {
            get { return 125 + 105 * Program.dataBase.getValue(Program.currentRowIndex, "aileron"); }
            set { }
        }
        public double VM_elevator
        {
            get { return 125 + 105 * Program.dataBase.getValue(Program.currentRowIndex, "elevator"); }
            set { }
        }


        Program pr;
        public JoystickVM()
        {
            this.pr = Program.getInstance();
            pr.PropertyChanged +=
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
