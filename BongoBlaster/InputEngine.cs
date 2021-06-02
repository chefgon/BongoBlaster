using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BongoBlaster
{
    public class InputEngine : IDisposable
    {
        #region Constructor
        public InputEngine() : this(0) { }

        public InputEngine(int playerNumber)
        {
            Initialize(playerNumber);
        }
        
        public void Dispose()
        {
            isObserving = false;
            directInput.Dispose();
        }
        #endregion Constructor

        #region Private Properties
        private DirectInput directInput;
        private DeviceInstance bongoDevice;
        private Joystick myBongo;
        private string productName = "GameCube Controller Adapter";
        private int playerNum;
        private bool isObserving = false;




        private bool lastKnownTopLeft;
        public bool TopLeft
        {
            get
            {
                return lastKnownTopLeft;
            }
            private set
            {
                if (value != lastKnownTopLeft)
                {
                    lastKnownTopLeft = value;
                    if (value == true)
                    {
                        OnButtonPressed(BongoButton.TopLeft);
                    }
                    else
                    {
                        OnButtonReleased(BongoButton.TopLeft);
                    }
                }
            }
        }

        private bool lastKnownTopRight;
        public bool TopRight
        {
            get
            {
                return lastKnownTopRight;
            }
            private set
            {
                if (value != lastKnownTopRight)
                {
                    lastKnownTopRight = value;
                    if (value == true)
                    {
                        OnButtonPressed(BongoButton.TopRight);
                    }
                    else
                    {
                        OnButtonReleased(BongoButton.TopRight);
                    }
                }
            }
        }

        private bool lastKnownBottomLeft;
        public bool BottomLeft
        {
            get
            {
                return lastKnownBottomLeft;
            }
            private set
            {
                if (value != lastKnownBottomLeft)
                {
                    lastKnownBottomLeft = value;
                    if (value == true)
                    {
                        OnButtonPressed(BongoButton.BottomLeft);
                    }
                    else
                    {
                        OnButtonReleased(BongoButton.BottomLeft);
                    }
                }
            }
        }

        private bool lastKnownBottomRight;
        public bool BottomRight
        {
            get
            {
                return lastKnownBottomRight;
            }
            private set
            {
                if (value != lastKnownBottomRight)
                {
                    lastKnownBottomRight = value;
                    if (value == true)
                    {
                        OnButtonPressed(BongoButton.BottomRight);
                    }
                    else
                    {
                        OnButtonReleased(BongoButton.BottomRight);
                    }
                }
            }
        }

        private int lastKnownClapSensorValue;
        public int ClapSensorValue
        {
            get
            {
                return lastKnownClapSensorValue;
            }
            private set
            {
                if (value != lastKnownClapSensorValue)
                {
                    lastKnownClapSensorValue = value;
                    OnClapSensorValueUpdated(value);
                }
            }
        }

        private long enableClapSensorOnTick;
        private long EnableClapSensorOnTick
        {
            get
            {
                return enableClapSensorOnTick;
            }
            set
            {
                if (value > enableClapSensorOnTick)
                {
                    enableClapSensorOnTick = value;
                }
            }
        }

        public bool IsClapSensorEnabled
        {
            get
            {
                return DateTime.UtcNow.Ticks > EnableClapSensorOnTick;
            }
        }

        private bool lastKnownClapSensor;
        public bool ClapSensor
        {
            get
            {
                return lastKnownClapSensor;
            }
            private set
            {
                if (value != lastKnownClapSensor)
                {
                    lastKnownClapSensor = value;
                    if (value == true)
                    {
                        OnButtonPressed(BongoButton.ClapSensor);
                    }
                    else
                    {
                        OnButtonReleased(BongoButton.ClapSensor);
                    }
                }
            }
        }

        private bool lastKnownStartButton;
        public bool StartButton
        {
            get
            {
                return lastKnownStartButton;
            }
            private set
            {
                if (value != lastKnownStartButton)
                {
                    lastKnownStartButton = value;
                    if (value == true)
                    {
                        OnButtonPressed(BongoButton.StartButton);
                    }
                    else
                    {
                        OnButtonReleased(BongoButton.StartButton);
                    }
                }
            }
        }
        #endregion Private Properties

        #region Events
        private void OnButtonPressed(BongoButton button)
        {
            if (button != BongoButton.ClapSensor)
            {
                // Stop responding to the clap sensor for a short while to avoid accidental triggers
                DisableClapSensor();
            }

            BongoButtonEventArgs args = new BongoButtonEventArgs();
            args.BongoButton = button;
            EventHandler<BongoButtonEventArgs> buttonPressed = ButtonPressed;
            if (buttonPressed != null)
            {
                buttonPressed(this, args);
            }
        }
        public event EventHandler<BongoButtonEventArgs> ButtonPressed;

        private void OnButtonReleased(BongoButton button)
        {
            if (button == BongoButton.TopLeft ||
                button == BongoButton.TopRight ||
                button == BongoButton.BottomLeft ||
                button == BongoButton.BottomRight)
            {
                // Stop responding to the clap sensor for a short while to avoid accidental triggers
                DisableClapSensor();
            }
            else if (button == BongoButton.StartButton)
            {
                // Oddly the Start button is a little noisier for a little longer, so we'll disable the sensor for a little longer
                DisableClapSensor(0.2);
            }

            BongoButtonEventArgs args = new BongoButtonEventArgs();
            args.BongoButton = button;
            EventHandler<BongoButtonEventArgs> buttonReleased = ButtonReleased;
            if (buttonReleased != null)
            {
                buttonReleased(this, args);
            }
        }
        public event EventHandler<BongoButtonEventArgs> ButtonReleased;

        private void OnClapSensorValueUpdated(int clapSensorValue)
        {
            ClapSensorValueEventArgs args = new ClapSensorValueEventArgs();
            args.ClapSensorValue = clapSensorValue;
            EventHandler<ClapSensorValueEventArgs> clapSensorValueUpdated = ClapSensorValueUpdated;
            if (clapSensorValueUpdated != null)
            {
                clapSensorValueUpdated(this, args);
            }
        }
        public event EventHandler<ClapSensorValueEventArgs> ClapSensorValueUpdated;
        #endregion Events

        #region Public Methods
        public void StartObserving()
        {
            if (myBongo != null)
            {
                isObserving = true;

                myBongo.Acquire();
                while (isObserving)
                {
                    myBongo.Poll();
                    
                    JoystickState state = myBongo.GetCurrentState();
                    ParseButtons(state.Buttons);
                    ParseClapSensor(state.RotationY, state.Buttons);
                    //System.Diagnostics.Trace.WriteLine(DateTime.UtcNow.Ticks + " " + EnableClapSensorOnTick + IsClapSensorEnabled);


                    // Wait a moment before looping so we don't spike the CPU core to 100% for millions of refreshes per second.
                    // Sleeping for even 1ms will limit us to 1000 loops per second (down from ~2,000,000 loops per second uncapped on my dev machine)
                    // but we'll try to be considerate and find the longest sleep time possible that doesn't affect reliability.
                    Thread.Sleep(40);
                }
                myBongo.Unacquire();
            }
        }

        public void StopObserving()
        {
            isObserving = false;
        }
        #endregion Public Methods

        #region Private Methods
        private void Initialize(int playerNumber)
        {
            playerNum = playerNumber;
            bongoDevice = GetBongo();
            
            if (bongoDevice != null)
            {
                myBongo = new Joystick(directInput, bongoDevice.InstanceGuid);
                myBongo.Properties.BufferSize = 128;
            }
        }

        private void ParseButtons(bool[] buttonStates)
        {
            // Go through the five buttons we care about
            TopRight = buttonStates[0];
            BottomRight = buttonStates[1];
            BottomLeft = buttonStates[2];
            TopLeft = buttonStates[3];
            StartButton = buttonStates[9];
        }

        private void ParseClapSensor(int clapSensorData, bool[] buttonStates)
        {
            ClapSensorValue = clapSensorData;

            if (IsClapSensorEnabled &&
                ClapSensorValue >= 8000)
            {
                ClapSensor = true;
            }
            else
            {
                ClapSensor = false;
            }
        }

        private void DisableClapSensor()
        {
            DisableClapSensor(0.1);
        }

        private void DisableClapSensor(double seconds)
        {
            int ticks = (int)(seconds * 10000000);
            EnableClapSensorOnTick = DateTime.UtcNow.Ticks + ticks;
        }

        private DeviceInstance GetBongo()
        {
            // Offset for hard-coded player 2 logic
            int playerOffsetCounter = playerNum;

            // Get DirectInput Controllers
            directInput = new DirectInput();
            IList<DeviceInstance> devices = directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly);

            foreach (DeviceInstance device in devices)
            {
                if (device.ProductName.CompareTo(productName) == 0)
                {
                    if (playerOffsetCounter > 0)
                    {
                        // Not us, keep looking
                        playerOffsetCounter--;
                    }
                    else
                    {
                        // This is our bongo!
                        return device;
                    }
                }
            }

            // We didn't find it!
            return null;
        }
        #endregion Private Methods
    }
}
