using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoCalc
{
    public class ButtonHandler
    {

        public delegate void ButtonEvent();
        public event ButtonEvent ButtonDown;
        public event ButtonEvent ButtonUp;
        public event ButtonEvent SingleClick;
        public event ButtonEvent ButtonHeld;
        public event ButtonEvent LongClick;

        private System.Threading.Timer holdTimer = null;

        public ButtonHandler()
        {
            InterruptPort button = new InterruptPort(Pins.ONBOARD_SW1, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            button.OnInterrupt += new NativeEventHandler(OnInterrupt);
        }

        private void OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if (data2 == 0)
            {
                ButtonDown();

                holdTimer = new System.Threading.Timer(
                    HoldTimerExpired, 
                    null, 
                    new TimeSpan(0, 0, 2), 
                    new TimeSpan(0, 0, 2));
            }
            else
            {
                ButtonUp();

                if (holdTimer != null)
                {
                    holdTimer.Dispose();
                    holdTimer = null;
                    SingleClick();
                }
                else
                    LongClick();
            }

        }

        private void HoldTimerExpired(object state)
        {
            if (holdTimer != null)
            {
                holdTimer.Dispose();
                holdTimer = null;
                ButtonHeld();
            }
        }

    }
}
