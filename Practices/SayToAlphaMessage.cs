using System;
using System.Collections.Generic;
using System.Text;
using Practices.Interface;

namespace Practices
{
    class SayToAlphaMessage : IDealMessage
    {
        public int dealMessage(int flag)
        {
            Console.WriteLine($"The Alpha message is {flag} plus 114, that means {flag * 114}");
            return 0;
        }
    }
}
