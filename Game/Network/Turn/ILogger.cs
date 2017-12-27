using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.Turn
{
    public interface ILogger
    {
        void WriteError(string message);
        void WriteWarning(string message);
        void WriteInformation(string message);
    }
}
