using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePublisherTool.Interface;
using MessagePublishUseWindow.Model;
using MessagePublishUseWindow.View;
using Wpf.MVVM.Foundation.Interfaces;
using Wpf.MVVM.Foundation.MVVM;

namespace MessagePublishUseWindow.ViewModel
{
    [ExportViewModel("MainWindowViewModel", typeof(MainWindowViewModel))]
    public class MainWindowViewModel:ViewModelBase
    {
        public IRelayCommand SendMessageCommand
        {
            get
            {
                if (_sendRelayCommand == null)
                {
                    _sendRelayCommand = new RelayCommand("", OnSendMessage);
                }
                return _sendRelayCommand;
            }
        }

        private IRelayCommand _sendRelayCommand;

        public ObservableCollection<string> ReciveMessagesCollection
        {
            get { return _reciveMessagesCollection; }
            set
            {
                _reciveMessagesCollection = value;
                OnPropertyChanged(() => ReciveMessagesCollection);
            }
        }
        private ObservableCollection<string> _reciveMessagesCollection=new ObservableCollection<string>();

        public String InputMessage
        {
            get { return _inputMessage; }
            set
            {
                _inputMessage = value;
                OnPropertyChanged(()=> InputMessage);
            }
        }

        private String _inputMessage = String.Empty;

        public void Handler(MainLoadedMessage message)
        {
           
        }

        private void OnSendMessage()
        {
            SecondLoadedMessage message =new SecondLoadedMessage();
            Random rand=new Random();
            message.Name = "MainWindowViewModel";
            message.Content = InputMessage+rand.Next(0, 1000).ToString();
            MessagePublisherTool.Implement.Publisher.Instance.Publish(message);
        }

        public MainWindowViewModel()
        {
            //MessagePublisherTool.Implement.Publisher.Instance.AddSubscriber(this);
        }


    }
}
