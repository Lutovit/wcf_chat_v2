using MyChatClient.wcf_chat;
using System;
using System.Collections.ObjectModel;

namespace MyChatClient
{
    public interface IServiceManager
    {
        ServiceChatClient ServiceChatClient { get; }
        PrivateMessageServiceClient PrivateMessagesServiceClient { get; }

        event EventHandler<string> onMessageReceived;
        event EventHandler<Collection<string>> onUsersListChanged;
        event EventHandler<string> onPrivateConnected;

        event EventHandler<string> onMessageBoxShow; //Max
        event EventHandler onEnterIntoChatIsAllowed; //Max

        event EventHandler<string> onPrivateMessageReceived;  //Max
        event EventHandler<MessageEventArgs> onPrivateMessageReceived_v2; //Max
        event EventHandler<MessageEventArgs> onPrivateChatAccepted;
    }
}