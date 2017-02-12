using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPDFConfig
{

    using SharpDX.DirectInput;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ConsoleApplication2
    {
        class Program
        {

            static void Main()
            {

                
                /*
                // Instantiate the joystick
                var joystick = new Joystick(directInput, joystickGuid);

                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

                // Set BufferSize in order to use buffered data.
                joystick.Properties.BufferSize = 128;

                // Acquire the joystick
                joystick.Acquire();

                // Poll events from joystick
                while (true)
                {
                    joystick.Poll();
                    var datas = joystick.GetBufferedData();
                    foreach (var state in datas)
                        Console.WriteLine(state);
                }
            
            */

            }


            Dictionary<Guid,String> getGameDevices(){

                // Initialize DirectInput
                var directInput = new DirectInput();
                Dictionary<Guid, String> devices = new Dictionary<Guid, String>();
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

                return devices;
            }


        }
    }

}

