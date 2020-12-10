using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Domain.Core.Commands;

namespace MicroRabbit.Banking.Domain.Commands
{
    public abstract class TransferCommand: Command
    {
        public int From { get; internal set; }
        public int To { get; internal set; }
        public decimal Amount { get; internal set; }
    }
}
