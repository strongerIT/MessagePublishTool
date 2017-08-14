using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePublisherTool.Interface;
using MessagePublishUseWindow.Model;
using Wpf.MVVM.Foundation.MVVM;

namespace MessagePublishUseWindow.ViewModel
{
    [ExportViewModel("SecondWindowViewModel",typeof(SecondWindowViewModel))]
    public class SecondWindowViewModel:ViewModelBase,ISubscriber<SecondLoadedMessage>
    {
        public void Handler(SecondLoadedMessage message)
        {
            string str = String.Format("{0}:{1},{2}", DateTime.Now, message.Name, message.Content);
            ReciveMessagesCollection.Add(str);
        }

        public ObservableCollection<string> ReciveMessagesCollection
        {
            get { return _reciveMessagesCollection; }
            set
            {
                _reciveMessagesCollection = value;
                OnPropertyChanged(() => ReciveMessagesCollection);
            }
        }
        private ObservableCollection<string> _reciveMessagesCollection = new ObservableCollection<string>();

        public SecondWindowViewModel()
        {
            MessagePublisherTool.Implement.Publisher.Instance.AddSubscriber(this);
           // MessagePublisherTool.Implement.Publisher.Instance.AddSubscriber(this);
        }
    }
}
