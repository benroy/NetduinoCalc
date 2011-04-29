using System;
using Microsoft.SPOT;

namespace NetduinoCalc
{
    class StateMachine
    {
        private static StateMachine instance;

        enum StateEnum
        {
            Invalid,
            InputingNumber            
        };

        private StateEnum state = StateEnum.Invalid;
        private State currentState = null;

        private StateMachine() 
        {
            Initialize();
            EventHandler.Instance.SingleClick += new EventHandler.ClickEvent(SingleClick);
        }

        void SingleClick(object sender, EventArgs e)
        {
            currentState.SingleClick();
        }

        abstract class State {
            public abstract void SingleClick();
        }

        class NumberAcceptor : State {

            public NumberAcceptor() { }

            private uint number = 0;
            
            public override void SingleClick()
            {
                number++;
            }
        }

        public void Initialize()
        {
            state = StateEnum.InputingNumber;
            currentState = new NumberAcceptor();
        }

        public static StateMachine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StateMachine();
                }
                return instance;
            }
        }
    }
}
