using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Practices.Interface;

namespace Practices
{
    class StartUseingTimerInBravoMessage : IDealMessage
    {
        public StartUseingTimerInBravoMessage()
        {
            ResetEvent = new AutoResetEvent(false);
            MyTimer = new Timer(this.CallBack, ResetEvent, 0, 3000);
        }

        public int dealMessage(int flag)
        {
            dealTime++;
            MesFlag = flag;
            MyTimer.Change(0, 3000);
            return 0;
        }

        public void CallBack(object sender)
        {
            Console.WriteLine($"the Bravo message is using timer and the time is {DateTime.Now}");
            Console.WriteLine($"And the Flag is {MesFlag}");
            Console.WriteLine($"Is this invoke the deal?, see the {dealTime}");
            return;
        }

        private Timer MyTimer { get; set; }
        private AutoResetEvent ResetEvent { get; set; }
        private int MesFlag { get; set; }
        private int dealTime { get; set; }
    }
}
