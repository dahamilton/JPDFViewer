using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace JPDF
{
    class JoyManager
    {
        /**
         * Provides handling of the Joystick buttons
         * @TODO: This class needs major refactoring. 
         * XBOX 360 controllers need special handiling that is currently not provided.
         */
        private Joystick joystick;
        public delegate void ControllerUpdateHandler(object sender, ControllerEventArgs e);
        public event ControllerUpdateHandler OnControllerEvent;

        public JoyManager(DeviceInstance device)
        {
            startDevice(device);
        }


        public static IList<DeviceInstance> GetControllers()
        {
            // Find a Joystick Guid
            var devices = new List<DeviceInstance>();
            var directInput = new DirectInput();
            //Add all connected gamepads to the device list.
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
            {
                devices.Add(deviceInstance);

            }

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                    DeviceEnumerationFlags.AllDevices))
            {
                devices.Add(deviceInstance);
            }

            return devices;
        }


        public void startDevice(DeviceInstance controller)
        {
            Task.Factory.StartNew(() => listen(controller));
        }


        private void listen(DeviceInstance controller)
        {
            var directInput = new DirectInput();
            joystick = new Joystick(directInput, controller.InstanceGuid);
            joystick.Properties.BufferSize = 128;
            joystick.Acquire();
            while (true)
            {
                joystick.Poll();
                var datas = joystick.GetBufferedData();
                foreach (var state in datas)
                {
                    updateStatus(state.Offset.ToString(), state.Value);
                }
            }

        }


        private void updateStatus(string button, int value)
        {

            if (OnControllerEvent == null) return;
            ControllerEventArgs c = new ControllerEventArgs(button, value);
            OnControllerEvent(this, c);
        }


        public class ControllerEventArgs
        {
            public string ButtonID { get; private set; }
            public int ButtonValue { get; private set; }
            public ControllerEventArgs(String buttonID, int buttonValue)
            {
                ButtonID = buttonID;
                ButtonValue = buttonValue;
            }

        }

    }
}






