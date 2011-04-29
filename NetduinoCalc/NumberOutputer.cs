using System;

namespace NetduinoCalc
{
    public class NumberOutputer
    {
        public static void OutputNumber(uint result)
        {
            Led.Blink(result, 100, 300);            
        }
    }
}
