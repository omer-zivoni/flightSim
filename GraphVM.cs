using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OxyPlot;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simulator
{
    public class GraphVM : INotifyPropertyChanged
    {
        [DllImport("AttemptDll.dll")]
        public static extern void findCorelitve(string attri, string f1, StringBuilder dst);
        [DllImport("AttemptDll.dll")]
        public static extern IntPtr Create(int a);
        /// <summary>
        /// Initializes a new instance of the GraphVM class.
        /// </summary>
        /// 
        public ICommand SelectedAttributesCommand { get; set; }
        static string path = "";
        public ObservableCollection<DataPoint> dataPoints { get; set; }
        public ObservableCollection<DataPoint> corelativePoints { get; set; }
        public ObservableCollection<DataPoint> regresionPoints { get; set; }
        public ObservableCollection<DataPoint> linePoints { get; set; }

        private int pointsNumberinLine = 1000;


        public GraphVM()
        {
            SelectedAttributesCommand = new RelayCommand(SelectedAttributeFunc);
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = System.IO.Path.GetDirectoryName(strExeFilePath);
            ParseXml(path + "\\playback_small.xml");
            ParseCsv(path);
            
            Program pr = Program.getInstance();
            pr.PropertyChanged +=
              delegate (object sender, PropertyChangedEventArgs e)
              {
                  NotifyPropertyChanged(e.PropertyName);
                  
              };


        }

        DataBase dataBase = new DataBase(path);
        public int SelectedAttributeIndex { get; set; }
        void SelectedAttributeFunc()
        {
            var selection = SelectedAttributeIndex;
            var name = Attributes[selection];
            Title = name;

            dataPoints = new ObservableCollection<DataPoint>();
            double[] coloumn = Program.dataBase.getColumnAtIndex(selection);

            for (int i = 0; i < Program.currentRowIndex; i++)
            {
                dataPoints.Add(new DataPoint(i, coloumn[i]));
            }
            Points = dataPoints;

            showMostCorelativeAtribute();

            showLinearRegresion();
        }

        public void showLinearRegresion()
        {
            /*
             * the dll is not working, but if it work-use this: 
            string l = getLinearRegresion(this.Title, This.CTitle);
            double[] xyxy = l.split(',');
            linePoints = new ObservableCollection<DataPoint>();
            DataPoint start =new DataPoint(xyxy[0], xyxy[1]);
            DataPoint end = new DataPoint(xyxy[2], xyxy[3]);
            double addToX = (end.X - start.X) / pointsNumberinLine;
            double addToY = (end.Y - start.Y) / pointsNumberinLine;
            
            for (int i = 0; i < pointsNumberinLine; i++)
            {
                linePoints.Add(new DataPoint(start.X + addToX * i, start.Y + addToY * i));
            }

            linePoints.Add(end);
            this.Line = linePoints;
            */
            this.RTitle = this.Title + " / " + this.CTitle;
            this.regresionPoints = new ObservableCollection<DataPoint>();

            for (int i = 0; i < Program.currentRowIndex; i++)
            {
                regresionPoints.Add(new DataPoint(this.dataPoints[i].Y, this.corelativePoints[i].Y));
            }

            RPoints = regresionPoints;


        }

        public void showMostCorelativeAtribute()
        {
            /*
            find the most corelative atribute
            the dll is not working, but if it work-use this:
            string attriStrin = string.Join (",", Attributes);
            StringBuilder dst = new StringBuilder(100);
            findCorelitve(attriStrin, this.Title, dst);
            this.CTitle = dst.ToString();
            */


            this.CTitle = "aileron";
            this.corelativePoints = new ObservableCollection<DataPoint>();


            for (int i = 0; i < Program.currentRowIndex; i++)
            {
                corelativePoints.Add(new DataPoint(i, Program.dataBase.getValue(i, this.CTitle)));
            }
            CPoints = corelativePoints;

        }

        public string[] Attributes { get; set; }
        public void ParseXml(string path)
        {
            Settings d = new Settings(path);

            Attributes = d.attributes;
        }


        public void ParseCsv(string path)
        {
            DataBase dataBase = new DataBase(path);

            int currentRowIndex = 0;
            int lastRowIndex = dataBase.numberOfRows;


            while (currentRowIndex != lastRowIndex)
            {
                var row = Encoding.ASCII.GetBytes(dataBase.getRowAtIndex(currentRowIndex) + "\n");

                string test = dataBase.getRowAtIndex(currentRowIndex);

                currentRowIndex++;
                string[] spl = test.Split(',');
            }
        }

        private string cTitle;
        public string CTitle
        {
            get { return cTitle; }
            set
            {
                cTitle = value;
                NotifyPropertyChanged("cTitle");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string rTitle;
        public string RTitle
        {
            get { return rTitle; }
            set
            {
                rTitle = value;
                NotifyPropertyChanged("RTitle");
            }
        }

        private IList<DataPoint> rPoints;
        public IList<DataPoint> RPoints
        {
            get { return rPoints; }
            set
            {
                rPoints = value;
                NotifyPropertyChanged("RPoints");
            }
        }

        private IList<DataPoint> cPoints;
        public IList<DataPoint> CPoints 
        {
            get { return cPoints; }
            set
            {
                cPoints = value;
                NotifyPropertyChanged("CPoints");
            }
        }


        private IList<DataPoint> points;
        public IList<DataPoint> Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                NotifyPropertyChanged("Points");
            }
        }

        private IList<DataPoint> line;
        public IList<DataPoint> Line
        {
            get
            {
                return line;
            }
            set
            {
                line = value;
                NotifyPropertyChanged("Line");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

