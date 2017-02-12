using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPDFConfig
{
    class GameDeviceManager
    {

        private Dictionary<Guid, String> devices = new Dictionary<Guid, String>();

        public GameDeviceManager()
        {

        }
  
       public Dictionary<Guid, String> getGameDevices()
        {

            // Initialize DirectInput
            var directInput = new DirectInput();
            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
            {


                joystickGuid = deviceInstance.InstanceGuid;
                Console.WriteLine(deviceInstance.ProductName);
                devices.Add(joystickGuid, deviceInstance.ProductName);

            }
            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid = deviceInstance.InstanceGuid;
                    devices.Add(joystickGuid, deviceInstance.ProductName);

                }
                if (devices.Count < 1)
            {
                Console.WriteLine("Error: No usable input device was detected...Exiting");
                Environment.Exit(-1);
            }
            return devices;
        }

        /**
         * Show a list of Gamepads/Joysticks that have been detected and ask the user to choose the one they want
         * to use.
         * 
        */
        public Guid ChooseDevice()
        {

            Console.WriteLine("**** Choose Which Device Will Controll JPDF Press ESC to close this program. ****");
            for (int i = devices.Count -1; i >= 0; i--)
            {
                var item = devices.ElementAt(i);
                var itemKey = item.Key;
                var itemValue = item.Value;
                Console.WriteLine("[{0}] {1} ",i,item.Value);
            }


            do
            {
                while (!Console.KeyAvailable)
                {

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        }


    }
}
