using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisherTool.Interface
{
    public interface ISubscriber<T>
    {
        void Handler(T message);
    }
}
