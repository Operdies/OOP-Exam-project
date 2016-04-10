//20154304_Alexander_Nørskov_Larsen



using System;

namespace Eksamensopgave2016.Core
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(string message) : base(message)
        {
            
        }
    }
}
