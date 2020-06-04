using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace wcf_chat
{
    [ServiceContract(CallbackContract = typeof(IPrivateMessageServiceCallBack))]
    public interface IPrivateMessageService
    {
        [OperationContract]
        int CreatePrivateMessage(string SenderName, string AdresseeName);

        [OperationContract(IsOneWay = true)]
        void SendPrivateMsg(string sender, string adressee, string msg);

    }


    [ServiceContract]
    public interface IPrivateMessageServiceCallBack
    {

        [OperationContract(IsOneWay = true)]
        void OnPrivateConnected(string msg);

        [OperationContract(IsOneWay = true)]
        void OnPrivateMessageReceived(string msg);
        [OperationContract(IsOneWay = true)]
        void OnPrivateMessageReceived_v2(string _sender, string _adressee, string message);
    }
}
