
using System;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;

namespace Simulator
{

    class Program : INotifyPropertyChanged
    {
        public static DataBase dataBase { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public double playSpeed { get; set; }

        public string playString { get; set; }

        public static int currentRowIndex { get; set; }

        public string fgPath { get; set; }

        public bool isInitialize { get; set; }



        private Program()
        {
            this.playString = "play";
            currentRowIndex = 0;


            dataBase = new DataBase("");

            this.playSpeed = 1;

        }
        static private Program pr;
        public static Program getInstance()
        {
            if (pr == null)
            {
                pr = new Program();
            }
            return pr;
        }

        private void Exit(int v)
        {
            throw new NotImplementedException();
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));

        }


        public void func()
        {

            var client = new TcpClient("localhost", 5400);
            var stream = client.GetStream();

            currentRowIndex = 0;
            int lastRowIndex = dataBase.numberOfRows;
            int timeToSleep = 100;

            while (currentRowIndex != lastRowIndex)
            {
                if (playString.Equals("play"))
                {
                    continue;
                }
                var row = Encoding.ASCII.GetBytes(dataBase.getRowAtIndex(currentRowIndex) + "\n");
                stream.Write(row, 0, row.Length);

                Thread.Sleep(Convert.ToInt32(timeToSleep / playSpeed));
                currentRowIndex++;
                this.NotifyPropertyChanged("currentRowIndex");
                PropertyChanged(this, new PropertyChangedEventArgs("altimeter"));
                PropertyChanged(this, new PropertyChangedEventArgs("airspeed"));
                PropertyChanged(this, new PropertyChangedEventArgs("oriention"));
                PropertyChanged(this, new PropertyChangedEventArgs("yaw"));
                PropertyChanged(this, new PropertyChangedEventArgs("roll"));
                PropertyChanged(this, new PropertyChangedEventArgs("pitch"));
                PropertyChanged(this, new PropertyChangedEventArgs("throttle"));
                PropertyChanged(this, new PropertyChangedEventArgs("rudder"));
                PropertyChanged(this, new PropertyChangedEventArgs("aileron"));
                PropertyChanged(this, new PropertyChangedEventArgs("elevator"));
                PropertyChanged(this, new PropertyChangedEventArgs("dataPoints"));
            }

            stream.Close();
            client.Close();
        }

        public int getRowsNumber()
        {
            if (dataBase == null)
            {
                return 0;
            }
            return dataBase.numberOfRows;

        }
    }
}
