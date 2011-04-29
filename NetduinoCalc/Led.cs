using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoCalc
{
    class Led
    {
        private static OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);

        public static void Blink(uint count)
        {
            Blink(count, 100, 100);
        }

        public static void Blink(uint count, int msecOn, int msecOff)
        {
            if (On)
            { 
                On = false;
                Thread.Sleep(msecOff);
            }

            while (count-- > 0)
            {
                On = true;
                Thread.Sleep(msecOn);

                On = false;
                Thread.Sleep(msecOff);
            }
        }

        public static bool On
        {
            get 
            {
                return led.Read();
            }
            set
            {
                led.Write(value);
            }
        }
    }
}
