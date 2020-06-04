using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using wcf_chat;
using MyChatClient.wcf_chat;

namespace MyChatClient
{   // Все колбэки теперь лежат в объект ServiceManager, чтобы не добавлять лишние связи на формочку.
    //IncludeExceptionDetailInFaults = true
    [CallbackBehavior(IncludeExceptionDetailInFaults = true, UseSynchronizationContext = true, ValidateMustUnderstand = true)]
    class ServiceManager : wcf_chat.IServiceChatCallback, wcf_chat.IPrivateMessageServiceCallback, IServiceManager
    {
        public event EventHandler<string> onMessageReceived;
        public event EventHandler<Collection<string>> onUsersListChanged;
        public event EventHandler<string> onPrivateConnected;

        public event EventHandler<string> onPrivateMessageReceived;         // Max
        public event EventHandler<MessageEventArgs> onPrivateMessageReceived_v2; // Max

        public event EventHandler<MessageEventArgs> onPrivateChatAccepted;

        public event EventHandler<string> onMessageBoxShow;                         //Max
        public event EventHandler onEnterIntoChatIsAllowed;                         //Max


        public ServiceChatClient ServiceChatClient { get;  private set; }
        public PrivateMessageServiceClient PrivateMessagesServiceClient { get;  private set; }

        public ServiceManager()
        {
            ServiceChatClient = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            PrivateMessagesServiceClient = new PrivateMessageServiceClient(new System.ServiceModel.InstanceContext(this));
        }
        



        public void OnMessageReceived(string msg)
        {
            onMessageReceived?.Invoke(this, msg);
        }

        public void OnPrivateConnected(string msg)
        {
            onPrivateConnected?.Invoke(this, msg);
        }

        public void OnUsersListChanged(Collection<string> names)
        {
            onUsersListChanged?.Invoke(this, names);
        }

        public void OnPrivateMessageReceived(string msg)     //Max
        {
            onPrivateMessageReceived?.Invoke(this, msg);
        }

        public void OnPrivateMessageReceived_v2(string sender, string adressee, string message)     //Max
        {
            MessageEventArgs e = new MessageEventArgs()
            {
                _sendername = sender,
                _adressename = adressee,
                _message = message
            };

            onPrivateMessageReceived_v2?.Invoke(this, e);
        }

        public void OnPrivateChatAskAccepted(string adresseeName, string senderName)
        {
            onPrivateChatAccepted?.Invoke(this, new MessageEventArgs {
                _adressename = adresseeName,
                _sendername = senderName,
                _message = String.Empty
            });
        }






        public void OnMessageBoxShow(string msg) 
        {
            onMessageBoxShow?.Invoke(this, msg);
        }

        public void OnEnterIntoChatIsAllowed()
        {
            onEnterIntoChatIsAllowed?.Invoke(this, EventArgs.Empty);
        }





    }









    public class MessageEventArgs : EventArgs
    {
        public string _sendername;
        public string _adressename;
        public string _message;
    }



    public class AccauntArgs : EventArgs
    {
        public string _login { set; get; }
        public string _password { set; get; }
        public string _name { set; get; }
        public int _age { set; get; }

        public Sex _sex { set; get; }

        public AccauntArgs() { }
        public AccauntArgs(string L, string P) 
        {
            _login = L;
            _password = P;
            _name = "";
            _age = 0;
            _sex = null;       
        }
    }
}
