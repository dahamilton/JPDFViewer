using PdfiumViewer;
using System;
using System.Windows.Forms;
using SharpDX.DirectInput;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;

namespace JPDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            String file;

            if (args.Length > 1)
            {
                file = args[1];
            }
            else
            {
                file = @"C:\temp\test.pdf";
            }
            pdfViewer1.Dock = DockStyle.Fill;
            //Full Screen. No controls, this is a viewer only...
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            var doc = PdfDocument.Load(file);
            pdfViewer1.Renderer.Load(doc);
            pdfViewer1.ZoomMode = PdfViewerZoomMode.FitBest;
            pdfViewer1.ShowToolbar = false;
            /**
             * We just grab the first controller that is detected. This will eventually be replaced with a configuration window. 
             * If no controllers are detected, keyboard controls will still work. The form has a hook to allow the ESC key to exit the viewer 
             * when used in keyboard mode.
             */

            if (JoyManager.GetControllers().Count > 0)
            {
                var manager = new JoyManager(JoyManager.GetControllers()[0]);
                manager.OnControllerEvent += Manager_OnControllerEvent;
            }
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

        }

        /**
         * This method is a NASTY proof of concept hack. It provides hardcoded support for XBOX 360 gamepads (tested with wireless version)
         * as well as SOME game pads (tested with iBuffalo SNES pad). The thread handling (sorta) works, but this needs major refactoring. 
         */
        private void Manager_OnControllerEvent(object sender, JoyManager.ControllerEventArgs e)
        {
            Console.WriteLine(e.ButtonID);
            Console.WriteLine(e.ButtonValue);

            switch (e.ButtonID)
            {
                case "PointOfViewControllers0":
                    /**
                     * This is used for both XBOX and game pads.
                     * On an XBOX controller, this corresponds to the down on the POV hat. For the one tested controller, it's down on the dpad.
                    */
                    if (e.ButtonValue == 18000 || e.ButtonValue == 13500 || e.ButtonValue == 65535)
                    {

                        this.Invoke((MethodInvoker)delegate
                        {
                            pdfViewer1.Renderer.PerformScroll(ScrollAction.LineDown, Orientation.Vertical);
                        });

                    }

                    if (e.ButtonValue == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            pdfViewer1.Renderer.PerformScroll(ScrollAction.LineUp, Orientation.Vertical);
                        });

                    }
                    break;


                case "Buttons0":
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            pdfViewer1.Renderer.PerformScroll(ScrollAction.PageDown, Orientation.Vertical);
                        });


                    }
                    break;

                case "Buttons1":
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            pdfViewer1.Renderer.PerformScroll(ScrollAction.PageUp, Orientation.Vertical);
                        });


                    }
                    break;
                case "Buttons6":
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Close();
                        });


                    }
                    break;

                case "Y":
                    {
                        goto case "PointOfViewControllers0";


                    }
            }

        }



    /**
     * Configuration dialog not implemnet yet...
     */
        private void showConfig()
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pdfViewer1.Renderer.PerformScroll(ScrollAction.LineDown, Orientation.Vertical);
        }
    }
}
