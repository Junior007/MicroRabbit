using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRabbit.Domain.Core.Events
{
    public abstract class Event
    {

        public DateTime TimesStamp;

        protected Event()
        {
            TimesStamp = DateTime.Now;
        }
    }
}
