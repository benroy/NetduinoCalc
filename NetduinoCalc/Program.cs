using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;


/* This is a simple calculator. Operands and opeartors are input by clicking the button. Values are output by blinking the LED.
 * State is also indicated by a blinking LED. 
 * 
 * InitialState
 * 
 * To enter a number be sure we are in the InitialState, hold the button for 5 seconds to get back to InitialState
 */
namespace NetduinoCalc
{
    public class Program
    {
        private static Evaluator e = new Evaluator();
        private static NumberInputer ni = new NumberInputer();

        public static void Main()
        {
            ni.OnNumberInput += new NumberInputer.OnNumberInputDelegate(e.InputNumber);
            e.OnOutput += new Evaluator.OnOutputDelegate(NumberOutputer.OutputNumber);
            Thread.Sleep(Timeout.Infinite);
        }

        class Evaluator
        {
        
            private uint rvalue = 0;
            private uint lvalue = 0;
            private bool rvalueInput = false;
            private bool lvalueInput = false;
            public delegate void OnOutputDelegate(uint result);
            public event OnOutputDelegate OnOutput;
            
            public Evaluator() { }

            public void InputNumber(uint input)
            {
                if (lvalueInput == false)
                {
                    lvalue = input;
                    lvalueInput = true;
                }

                else if (rvalueInput == false)
                {
                    rvalue = input;
                    rvalueInput = true;
                }

                Evaluate();
            }

            private void Evaluate()
            {
                if (rvalueInput == true && lvalueInput == true)
                {
                    rvalueInput = false;                    
                    lvalueInput = false;
                    OnOutput(rvalue + lvalue);
                }
            }
        }
    }
}
