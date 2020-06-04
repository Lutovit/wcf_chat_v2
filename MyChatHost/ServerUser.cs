using System.Runtime.Serialization;
using System.ServiceModel;
using wcf_chat;

namespace MyChatHost
{
    public class ServerUser
    {     
        public int Id { set; get; }

        public string UserName { set; get; }

        //public OperationContext OperationContext { get; set; }

        public IServiceChatCallBack ChatCallback { get; set; }

        public IPrivateMessageServiceCallBack PrivateMessageCallback { get; set; }
    }
}
