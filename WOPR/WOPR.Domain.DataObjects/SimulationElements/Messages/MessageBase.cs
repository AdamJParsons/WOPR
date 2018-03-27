using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.Domain.DataObjects.SimulationElements.Messages
{
    public abstract class MessageBase : BaseDO, IMessage
    {
        public Guid Identifier { get; private set; }

        public DateTime Timestamp { get; private set; }

        public MessageBase()
        {
            Identifier = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
    }

    public interface IMessage
    {
        Guid Identifier { get; }

        DateTime Timestamp { get; }
    }
}
