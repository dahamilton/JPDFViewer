using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoyPDFViwer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var inputHelper = new InputDeviceHelper();
            Dictionary<Guid, String> devices = inputHelper.getGameDevices();
            deviceListBox.DataSource = new BindingSource(devices, null);
            deviceListBox.DisplayMember = "Value";
            deviceListBox.ValueMember = "Key"; 

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void deviceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    /*
     *    // Instantiate the joystick
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
     * 
     * */

}
