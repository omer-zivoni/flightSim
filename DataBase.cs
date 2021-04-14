
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
    class DataBase
    {
        private string[][] data;

        public string[] Attributes { get; set; }

        public string localPath { get; set; }

        public int numberOfRows
        {
            get;
        }

        public DataBase(string path)
        {

            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            localPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            var rows = new List<string[]>();
            try
            {

                StreamReader CSVreader = new StreamReader(localPath + "/reg_flight.csv");
               
                numberOfRows = 0;
                while (!CSVreader.EndOfStream)
                {
                    string[] row = CSVreader.ReadLine().Split(',');
                    rows.Add(row);
                    numberOfRows++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("program failed to load csv file");

                System.Environment.Exit(1);
            }

            try
            {

                this.data = rows.ToArray();

                Settings d = new Settings(localPath + "/playback_small.xml");

                Attributes = d.attributes;
            }
            catch (Exception e)
            {
                Console.WriteLine("program failed to load xml file");

                System.Environment.Exit(1);
            }




        }

        public string getRowAtIndex(int index)
        {
            return String.Join(",", data[index]);
        }

        public double getValue(int rowNumber, string para)
        {

            int coloumnNumber = Array.FindIndex(this.Attributes, x => x.Contains(para));
            if (rowNumber < 0 || rowNumber >= data.Length || coloumnNumber < 0 || coloumnNumber > data[0].Length)
            {
                return -1;
            }
            return double.Parse(data[rowNumber][coloumnNumber]);

        }

        public double[] getColumnAtIndex(int index)
        {
            double[] column = new double[numberOfRows];
            for (int i = 0; i < numberOfRows; i++)
            {
                column[i] = float.Parse(data[i][index]);
            }
            return column;
        }

    }
}
