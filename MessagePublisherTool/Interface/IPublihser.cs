using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisherTool.Interface
{
    public interface IPublihser
    {
        void AddSubscriber<T>(ISubscriber<T> subscriber);
        void RemoveSubscriber<T>(ISubscriber<T> subscriber);

        void RemoveAllSubscribers();
        void Publish<T>(T message);

    }
}
