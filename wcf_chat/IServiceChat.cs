using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace wcf_chat
{  


    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService_Chat" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServiceChatCallBack))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);

        [OperationContract]
        IEnumerable<string> GetUsersList();

        [OperationContract]
        string GetUserName(int id);

        [OperationContract]
        bool AskAdresseeToPrivateMessage(string adressee, string sender);



        [OperationContract(IsOneWay = true)]
        void RegistrateUsersAccaunt(string login, string password, string name, int age, int i);

        [OperationContract(IsOneWay = true)]
        void DeleteUsersAccaunt(string login, string password);

        [OperationContract(IsOneWay = true)]
        void MayIComeIn(string login, string password);

        [OperationContract]
        bool MayIComeIn_v2(string login, string password);




    }

    [ServiceContract]
    public interface IServiceChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void OnMessageReceived(string msg);


        [OperationContract(IsOneWay = true)]
        void OnUsersListChanged(IEnumerable<string> names);

        [OperationContract(IsOneWay = true)]
        void OnPrivateChatAskAccepted(string adresseeName, string senderName);





        [OperationContract(IsOneWay = true)]
        void OnMessageBoxShow(string msg);

        [OperationContract(IsOneWay = true)]
        void OnEnterIntoChatIsAllowed();

    }    
}




