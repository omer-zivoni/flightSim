using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Simulator
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        private Boolean firstStart;
        ControllerVM vm;
        Program pr
        {
            get; set;
        }
        public Player()
        {
            InitializeComponent();
            firstStart = true;
            vm = new ControllerVM(new ControllerM(Program.getInstance()));
            DataContext = vm;
        }
        void btnClick(object sender, RoutedEventArgs e)
        {
            graph.Background = Brushes.Orange;

        }
        void backClick(object sender, RoutedEventArgs e)
        {
            vm.VM_currentRowIndex -= 50;
        }
        void forwardClick(object sender, RoutedEventArgs e)
        {
            vm.VM_currentRowIndex += 50;

        }

        void playingClick(object sender, RoutedEventArgs e)
        {
            if (this.firstStart)
            {
                this.firstStart = false;
            }
            if (vm.VM_playString.Equals("stop"))
                vm.VM_playString = "play";
            else
                vm.VM_playString = "stop";

        }
        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
