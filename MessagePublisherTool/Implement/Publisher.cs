using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePublisherTool.Interface;

namespace MessagePublisherTool.Implement
{
    public class Publisher:IPublihser
    {
        #region field
        private readonly Dictionary<Type,List<WeakReference>> _subscriberTypeObjPairs=new Dictionary<Type, List<WeakReference>>();
        #endregion

        #region Singleton

        private static readonly Lazy<Publisher> _publiserInstance = new Lazy<Publisher>(() => new Publisher());

        public static Publisher Instance => _publiserInstance.Value;

        /// <summary>
        /// 
        /// </summary>
        private Publisher()
        {

        }

        #endregion

        #region implement IPublihser
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriber"></param>
        public void AddSubscriber<T>(ISubscriber<T> subscriber)
        {
            Type subscriType = typeof(T);
            if (!_subscriberTypeObjPairs.ContainsKey(subscriType))
            {
                var lst=new List<WeakReference>() {new WeakReference(subscriber) };
                _subscriberTypeObjPairs.Add(subscriType, lst);
            }
            else
            {
                var lst = _subscriberTypeObjPairs[subscriType];
                var index = GetSubscriberIndex(subscriber, lst);
                if (index == -1)
                {
                    lst.Add(new WeakReference(subscriber));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriber"></param>
        public void RemoveSubscriber<T>(ISubscriber<T> subscriber)
        {
            Type subscriType = typeof(T);
            if (!_subscriberTypeObjPairs.ContainsKey(subscriType))
            {
                return;
            }
            else
            {
                var lst = _subscriberTypeObjPairs[subscriType];
                var index = GetSubscriberIndex(subscriber, lst);
                if (index != -1)
                {
                    lst.RemoveAt(index);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void RemoveAllSubscribers()
        {
            _subscriberTypeObjPairs.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public void Publish<T>(T message)
        {
            Type subscriType = typeof(T);
            if (!_subscriberTypeObjPairs.ContainsKey(subscriType))
            {
                return;
            }
            else
            {
                var lst = _subscriberTypeObjPairs[subscriType];
                foreach (var obj in lst)
                {
                     var item = obj.Target as ISubscriber<T>;
                    if (item != null)
                    {
                        item.Handler(message);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriber"></param>
        /// <param name="lst"></param>
        /// <returns></returns>
        private int GetSubscriberIndex<T>(ISubscriber<T> subscriber, List<WeakReference> lst)
        {
            var index = lst.FindIndex(item =>
            {
                var obj = item.Target as ISubscriber<T>;
                if (obj!=null)
                {
                    return obj.Equals(subscriber);
                }
                return false;
            });
            return index;
        }
       #endregion
    }
}
