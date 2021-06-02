using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BongoBlaster
{
    /// <summary>
    /// Interaction logic for InputVisualizer.xaml
    /// </summary>
    public partial class InputVisualizer : Window
    {
        #region Constructor
        public InputVisualizer()
        {
            InitializeComponent();
        }

        private void inputVisualizer_Loaded(object sender, RoutedEventArgs e)
        {
            

            inputEngineP1 = new InputEngine();
            inputEngineP1.ButtonPressed += InputEngineP1_ButtonPressed;
            inputEngineP1.ButtonReleased += InputEngineP1_ButtonReleased;

            inputEngineP1Thread = new Thread(new ThreadStart(inputEngineP1.StartObserving));
            inputEngineP1Thread.IsBackground = true;
            inputEngineP1Thread.Start();

            inputEngineP2 = new InputEngine(1);
            inputEngineP2.ButtonPressed += InputEngineP2_ButtonPressed;
            inputEngineP2.ButtonReleased += InputEngineP2_ButtonReleased;

            inputEngineP2Thread = new Thread(new ThreadStart(inputEngineP2.StartObserving));
            inputEngineP2Thread.IsBackground = true;
            inputEngineP2Thread.Start();
        }
        #endregion Constructor

        #region Private Properties
        InputEngine inputEngineP1;
        Thread inputEngineP1Thread;
        InputEngine inputEngineP2;
        Thread inputEngineP2Thread;
        #endregion Private Properties

        #region Public Properties
        public Boolean TopLeft
        {
            get
            {
                return (Boolean)this.GetValue(TopLeftProperty);
            }
            set
            {
                this.SetValue(TopLeftProperty, value);
            }
        }
        public static readonly DependencyProperty TopLeftProperty = DependencyProperty.Register("TopLeft", typeof(Boolean), typeof(InputVisualizer));

        public Boolean TopRight
        {
            get
            {
                return (Boolean)this.GetValue(TopRightProperty);
            }
            set
            {
                this.SetValue(TopRightProperty, value);
            }
        }
        public static readonly DependencyProperty TopRightProperty = DependencyProperty.Register("TopRight", typeof(Boolean), typeof(InputVisualizer));

        public Boolean BottomLeft
        {
            get
            {
                return (Boolean)this.GetValue(BottomLeftProperty);
            }
            set
            {
                this.SetValue(BottomLeftProperty, value);
            }
        }
        public static readonly DependencyProperty BottomLeftProperty = DependencyProperty.Register("BottomLeft", typeof(Boolean), typeof(InputVisualizer));

        public Boolean BottomRight
        {
            get
            {
                return (Boolean)this.GetValue(BottomRightProperty);
            }
            set
            {
                this.SetValue(BottomRightProperty, value);
            }
        }
        public static readonly DependencyProperty BottomRightProperty = DependencyProperty.Register("BottomRight", typeof(Boolean), typeof(InputVisualizer));

        public int ClapSensorValue
        {
            get
            {
                return (int)this.GetValue(ClapSensorValueProperty);
            }
            set
            {
                this.SetValue(ClapSensorValueProperty, value);
            }
        }
        public static readonly DependencyProperty ClapSensorValueProperty = DependencyProperty.Register("ClapSensorValue", typeof(int), typeof(InputVisualizer));

        public int HighestClapSensorValue
        {
            get
            {
                return (int)this.GetValue(HighestClapSensorValueProperty);
            }
            set
            {
                this.SetValue(HighestClapSensorValueProperty, value);
            }
        }
        public static readonly DependencyProperty HighestClapSensorValueProperty = DependencyProperty.Register("HighestClapSensorValue", typeof(int), typeof(InputVisualizer));

        public Boolean ClapSensor
        {
            get
            {
                return (Boolean)this.GetValue(ClapSensorProperty);
            }
            set
            {
                this.SetValue(ClapSensorProperty, value);
            }
        }
        public static readonly DependencyProperty ClapSensorProperty = DependencyProperty.Register("ClapSensor", typeof(Boolean), typeof(InputVisualizer));

        public Boolean StartButton
        {
            get
            {
                return (Boolean)this.GetValue(StartButtonProperty);
            }
            set
            {
                this.SetValue(StartButtonProperty, value);
            }
        }
        public static readonly DependencyProperty StartButtonProperty = DependencyProperty.Register("StartButton", typeof(Boolean), typeof(InputVisualizer));

        #region P2 Properties
        public Boolean TopLeftP2
        {
            get
            {
                return (Boolean)this.GetValue(TopLeftP2Property);
            }
            set
            {
                this.SetValue(TopLeftP2Property, value);
            }
        }
        public static readonly DependencyProperty TopLeftP2Property = DependencyProperty.Register("TopLeftP2", typeof(Boolean), typeof(InputVisualizer));

        public Boolean TopRightP2
        {
            get
            {
                return (Boolean)this.GetValue(TopRightP2Property);
            }
            set
            {
                this.SetValue(TopRightP2Property, value);
            }
        }
        public static readonly DependencyProperty TopRightP2Property = DependencyProperty.Register("TopRightP2", typeof(Boolean), typeof(InputVisualizer));

        public Boolean BottomLeftP2
        {
            get
            {
                return (Boolean)this.GetValue(BottomLeftP2Property);
            }
            set
            {
                this.SetValue(BottomLeftP2Property, value);
            }
        }
        public static readonly DependencyProperty BottomLeftP2Property = DependencyProperty.Register("BottomLeftP2", typeof(Boolean), typeof(InputVisualizer));

        public Boolean BottomRightP2
        {
            get
            {
                return (Boolean)this.GetValue(BottomRightP2Property);
            }
            set
            {
                this.SetValue(BottomRightP2Property, value);
            }
        }
        public static readonly DependencyProperty BottomRightP2Property = DependencyProperty.Register("BottomRightP2", typeof(Boolean), typeof(InputVisualizer));

        public Boolean ClapSensorP2
        {
            get
            {
                return (Boolean)this.GetValue(ClapSensorP2Property);
            }
            set
            {
                this.SetValue(ClapSensorP2Property, value);
            }
        }
        public static readonly DependencyProperty ClapSensorP2Property = DependencyProperty.Register("ClapSensorP2", typeof(Boolean), typeof(InputVisualizer));

        public Boolean StartButtonP2
        {
            get
            {
                return (Boolean)this.GetValue(StartButtonP2Property);
            }
            set
            {
                this.SetValue(StartButtonP2Property, value);
            }
        }
        public static readonly DependencyProperty StartButtonP2Property = DependencyProperty.Register("StartButtonP2", typeof(Boolean), typeof(InputVisualizer));
        #endregion P2 Properties

        #endregion Public Properties

        #region Control Event Handlers
        private void InputEngineP1_ButtonPressed(object sender, BongoButtonEventArgs e)
        {
            UpdateP1Button(e.BongoButton, true);

            // Play a sound
            if (e.BongoButton == BongoButton.TopLeft || e.BongoButton == BongoButton.BottomLeft)
            {
                
            }
        }

        private void InputEngineP1_ButtonReleased(object sender, BongoButtonEventArgs e)
        {
            UpdateP1Button(e.BongoButton, false);
        }

        private void UpdateP1Button(BongoButton button, bool newState)
        {
            try
            {
                Dispatcher.Invoke((Action)delegate ()
                {
                    switch (button)
                    {
                        case BongoButton.TopLeft:
                            TopLeft = newState;
                            break;
                        case BongoButton.TopRight:
                            TopRight = newState;
                            break;
                        case BongoButton.BottomLeft:
                            BottomLeft = newState;
                            break;
                        case BongoButton.BottomRight:
                            BottomRight = newState;
                            break;
                        case BongoButton.ClapSensor:
                            ClapSensor = newState;
                            break;
                        case BongoButton.StartButton:
                            StartButton = newState;
                            break;
                        default:
                            break;
                    }
                });
            }
            catch
            {
                // This is mostly throwing Thread exceptions when the app is shutting down.
                // Do we need to do anything about that?
            }
        }

        private void InputEngineP2_ButtonPressed(object sender, BongoButtonEventArgs e)
        {
            UpdateP2Button(e.BongoButton, true);
        }

        private void InputEngineP2_ButtonReleased(object sender, BongoButtonEventArgs e)
        {
            UpdateP2Button(e.BongoButton, false);
        }

        private void UpdateP2Button(BongoButton button, bool newState)
        {
            try
            {
                Dispatcher.Invoke((Action)delegate ()
                {
                    switch (button)
                    {
                        case BongoButton.TopLeft:
                            TopLeftP2 = newState;
                            break;
                        case BongoButton.TopRight:
                            TopRightP2 = newState;
                            break;
                        case BongoButton.BottomLeft:
                            BottomLeftP2 = newState;
                            break;
                        case BongoButton.BottomRight:
                            BottomRightP2 = newState;
                            break;
                        case BongoButton.ClapSensor:
                            ClapSensorP2 = newState;
                            break;
                        case BongoButton.StartButton:
                            StartButtonP2 = newState;
                            break;
                        default:
                            break;
                    }
                });
            }
            catch
            {
                // This is mostly throwing Thread exceptions when the app is shutting down.
                // Do we need to do anything about that?
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();
        #endregion Control Event Handlers
    }
}
