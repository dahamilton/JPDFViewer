using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyPDFViwer
{
    class InputDeviceHelper
    {
        public Dictionary<Guid, String> getGameDevices()
        {

            // Initialize DirectInput
            var directInput = new DirectInput();
            Dictionary<Guid, String> devices = new Dictionary<Guid, String>();
            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
                devices.Add(joystickGuid, deviceInstance.ProductName);

            }


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
