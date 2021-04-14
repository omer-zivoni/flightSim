using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    
    class MainWindowVM
    {
        
        public string path { 
            get 
            {
                string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                return System.IO.Path.GetDirectoryName(strExeFilePath);
            } 
            set { } 
        }
        
        public MainWindowVM ()
        {
     
        }
    }
}
