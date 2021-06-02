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
using SharpDX.DirectInput;
using SharpDX.XInput;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace BongoBlaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Properties

        #endregion Public Properties

        #region Private Properties
        private IXbox360Controller virtualGamepad;
        private DeviceInstance myBongo;
        #endregion Private Properties

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            //ConnectVirtualController();
        }
        #endregion Constructor

        #region Event Handlers
        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();

        private void grdTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            string controllerList = string.Empty;
            int controllerPort = 0;

            /*// Get XInput Controllers
            List<Controller> controllers = GetXInputControllers();
            for (int i = 0; i < controllers.Count; i++)
            {
                if (controllers[i].IsConnected)
                {
                    controllerPort++;
                    controllerList += controllerPort.ToString() + ": Gamepad\r\n";
                }
            }*/

            // Get DirectInput Controllers
            DirectInput directInput = new DirectInput();
            IList<DeviceInstance> devices = directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly);
            
            for (int i = 0; i < devices.Count; i++)
            {
                controllerPort++;
                controllerList += controllerPort.ToString() + ": " + devices[i].ProductName + "\r\n";
            }
            myBongo = devices[0];
            
            directInput.Dispose();
            MessageBox.Show(controllerList);
        }

        //private bool isButtonPressed = false;
        private void btnTest2_Click(object sender, RoutedEventArgs e)
        {
            /*if (!isButtonPressed)
            {
                isButtonPressed = true;
                virtualGamepad.SetButtonState(Xbox360Button.A, true);
            }
            else
            {
                isButtonPressed = false;
                virtualGamepad.SetButtonState(Xbox360Button.A, false);
            }*/

            new InputVisualizer().Show();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            virtualGamepad.Disconnect();
        }
        #endregion Event Handlers

        #region Private Methods
        private void ConnectVirtualController()
        {
            ViGEmClient vigem = new();
            virtualGamepad = vigem.CreateXbox360Controller();
            virtualGamepad.Connect();
        }

        private List<Controller> GetXInputControllers()
        {
            List<Controller> controllers = new();
            controllers.Add(new Controller(UserIndex.One));
            controllers.Add(new Controller(UserIndex.Two));
            controllers.Add(new Controller(UserIndex.Three));
            controllers.Add(new Controller(UserIndex.Four));
            return controllers;
        }

        #endregion Private Methods

        
    }
}
