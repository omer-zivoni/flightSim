using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simulator
{
    class Settings
    {
        public string[] attributes;

        public Settings(string path)
        {

            XDocument xdoc = XDocument.Load(path);
            attributes = xdoc.Descendants("output").Descendants("name").Select(e => e.Value).ToArray();
        }

        public int getIndex(string s)
        {
            return Array.IndexOf(attributes, s);
        }


    }
}
