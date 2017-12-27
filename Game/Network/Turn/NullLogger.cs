using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Game.Network.Turn
{
    public class NullLogger : ILogger
    {
        public void WriteError(string message)
        {
        }
        public void WriteWarning(string message)
        {
        }
        public void WriteInformation(string message)
        {
        }
    }
}