using System;

namespace NetduinoCalc
{
    class NumberInputer
    {
        public delegate void OnNumberInputDelegate(uint input);
        public event OnNumberInputDelegate OnNumberInput;

        private uint inputValue = 0;

        ButtonHandler buttonHandler = new ButtonHandler();

        public NumberInputer()
        {
            buttonHandler.ButtonDown += delegate() { Led.On = true; };

            buttonHandler.ButtonUp += delegate() { Led.On = false; };

            buttonHandler.SingleClick += delegate() { inputValue++; };

            buttonHandler.ButtonHeld += delegate() { Led.Blink(2); };

            buttonHandler.LongClick += delegate()
            {
                OnNumberInput(inputValue);
                inputValue = 0;
            };            
        }
    }
}
