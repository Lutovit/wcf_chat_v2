using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wcf_chat;



namespace MyChatClient
{

    public partial class MainWindow : Window
    { 
        private bool isConnected = false;      
        private int _currentUserId = 0;
        IServiceManager _serviceManager = new ServiceManager();
        public string _username { set; get; }      //Max

        public bool registIsInProgress = false;    //запрещает вызов окна регистрации более чем один раз, пока оно открыто
        public bool enterIntoChat = false;         //запрет или разрешение входя в чат
       


        public MainWindow()
        {
            
            InitializeComponent();

            _serviceManager.onMessageReceived += OnMessageReceived;

            _serviceManager.onUsersListChanged += OnUsersListChanged;

            // _serviceManager.onPrivateMessageReceived += OnPrivateMessageReceived;   //Max
            //_serviceManager.onPrivateMessageReceived_v2 += OnPrivateMessageReceived_v2;   //Max

            _serviceManager.onPrivateChatAccepted += OnPrivatMessageInitAnswer;               //Max           

            _serviceManager.onMessageBoxShow += OnMessageBoxShow;                             //Max

            _serviceManager.onEnterIntoChatIsAllowed += OnEnterIntoChatIsAllowed;             //Max


        }

        /*
        private void OnPrivateMessageReceived(object sender, string msg)               ///Max
        {
            listBoxMsgList.Items.Add(msg);
            listBoxMsgList.ScrollIntoView(listBoxMsgList.Items[listBoxMsgList.Items.Count - 1]);
            MessageBox.Show("Личное сообщение получено пользователем - " + _username );
        }
        */

        private async void OnPrivateMessageReceived_v2(object sender, MessageEventArgs e)               ///Max
        {
            if (_username == e._adressename)
            {
                string _sender = _serviceManager.ServiceChatClient.GetUserName(_currentUserId);
                PrivateMessage privateMessage = new PrivateMessage(_sender, e._sendername, _serviceManager);
                privateMessage.Owner = this;
                privateMessage.Show();
                await privateMessage.Init();
                privateMessage.listBoxPrivateMsgList.Items.Add(e._message);
                privateMessage.listBoxPrivateMsgList.ScrollIntoView(listBoxMsgList.Items[listBoxMsgList.Items.Count - 1]);
            }
        }




        async Task ConnectUser()
        {
            if (!isConnected)
            { if (_serviceManager.ServiceChatClient.State != System.ServiceModel.CommunicationState.Opened)
                {
                    _serviceManager.ServiceChatClient.Open();
                }
                if (_serviceManager.PrivateMessagesServiceClient.State != System.ServiceModel.CommunicationState.Opened)
                {
                    _serviceManager.PrivateMessagesServiceClient.Open();
                }

                _username = textBoxUsername.Text;

                _currentUserId = await _serviceManager.ServiceChatClient.ConnectAsync(textBoxUsername.Text);

                textBoxUsername.IsEnabled = false;

                textBoxUserPassword.IsEnabled = false;

                buttonConDisc.Content = "Disonnect";

                isConnected = true;
                listBoxMsgList.Items.Add("______________Вы вошли в чат =)!______________");
            }
        }

        async Task DisconnectUser()
        {
            if (isConnected)
            {
                await _serviceManager.ServiceChatClient.DisconnectAsync(_currentUserId);

                textBoxUsername.IsEnabled = true;

                textBoxUserPassword.IsEnabled = true;

                buttonConDisc.Content = "Connect";

                isConnected = false;

                listBoxUserList.Items.Clear();

                listBoxMsgList.Items.Add("______________Вы покинули чат =(!______________");

            }
        }

        private async void ConnectDisconnectButtonClick(object sender, RoutedEventArgs e) // Чтобы корректно работали обработчики кнопок пришлось переделать вызов
            //методов сервиса, которые возвращают значения. В вашей версии (да и в версии базового примера коннект работал некорректно и вешал форму, если
            // в нем срабатывал колбэк. Без асинков колбэки работают только для методов контракта, возвращающих void.
        {


            // _serviceManager.ServiceChatClient.MayIComeIn(textBoxUsername.Text, textBoxUserPassword.Text);  // если все ок, enterIntoChat присваивается значение true.

            enterIntoChat = await _serviceManager.ServiceChatClient.MayIComeIn_v2Async(textBoxUsername.Text, textBoxUserPassword.Text);  // этот варик посимпатишнее =)

            if (!isConnected)
            {
                if (enterIntoChat == true)    // проверка правильно/неправильно введен логин/пароль
                {
                    await ConnectUser();
                }
                else
                {
                    MessageBox.Show("Your Accaunt is not found or your Login and Password are not valid. ");
                }

            }
            else
            {
                await DisconnectUser();
                enterIntoChat = false;
            }
        }
        


        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await DisconnectUser();
        }

        private void textBoxWrightMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_serviceManager.ServiceChatClient != null)
                {
                    _serviceManager.ServiceChatClient.SendMsg(textBoxWrightMsg.Text, _currentUserId);
                    textBoxWrightMsg.Text = string.Empty;
                } 
            }
        }
        
        public void OnMessageReceived(object sender, string msg)
        {
            listBoxMsgList.Items.Add(msg);
            listBoxMsgList.ScrollIntoView(listBoxMsgList.Items[listBoxMsgList.Items.Count - 1]);
            WrightIntoTextFile(msg);
        }
        
        public void OnUsersListChanged(object sender, Collection<string> names)
        {
            listBoxUserList.Items.Clear();          

            foreach (var name in names)
            {
                listBoxUserList.Items.Add(name);
            }
            
            listBoxUserList.ScrollIntoView(listBoxUserList.Items[listBoxUserList.Items.Count - 1]);
            
        }
      
        private async void ListBoxUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             string _adressee = listBoxUserList.SelectedItem.ToString();
            if (_adressee != _username)
            {
                string _sender = _serviceManager.ServiceChatClient.GetUserName(_currentUserId);
                bool success =_serviceManager.ServiceChatClient.AskAdresseeToPrivateMessage(_adressee, _sender);
                PrivateMessage privateMessage = new PrivateMessage(_sender, _adressee, _serviceManager);
                privateMessage.Owner = this;

                privateMessage.Title ="Private message mode: " + _sender + "-->" + _adressee;

                privateMessage.Show();
                await privateMessage.Init();
            }
            else MessageBox.Show("Вы не можете отправить личное сообщение сами себе! "); 
        }



        /*----------------------------------------похоже не используется----------------------------------------------------------------------------*/
        private async void OnPrivatMessageInitAnswer(object sender, MessageEventArgs e)
        {
            string _sender = e._adressename;
            string _adressee = e._sendername;

            PrivateMessage privateMessage = new PrivateMessage(_sender, _adressee, _serviceManager, e._message);            

            privateMessage.Owner = this;

            privateMessage.Title = "Private message mode: " + _adressee + "-->" + _sender;

            privateMessage.Show();
            await privateMessage.Init();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/


        public static void WrightIntoTextFile(string msg)  // метод записывает данные в текстовый файл
        {
            /*
            string adr = "E:\\generalChatUserLogfile.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(adr, true))
                {
                    sw.WriteLine(msg);
                }
            }

            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            */
        }




        /*----------------------------------User profile creating---------------------------------------------------------------------*/



        private void registationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!registIsInProgress) 
            {
                registIsInProgress = true;

                UserProfile usprof = new UserProfile();
                usprof.Owner = this;
                usprof.Show();

                

                //usprof.onAccauntRegistration += ShowAccauntArgs;

                usprof.onAccauntRegistration += RegistrateUsersAccauntAsync;

                usprof.onAccauntDelete += DeleteUsersAccauntAsync;

                usprof.Closed += OnUsProfClosed;
            }

        }



        private async void DeleteUsersAccauntAsync(object sender, AccauntArgs e)
        {
           await _serviceManager.ServiceChatClient.DeleteUsersAccauntAsync(e._login, e._password);
        }



        private async void RegistrateUsersAccauntAsync(object sender, AccauntArgs e)
        {
            int i;

            if (e._sex._sex == sex.Male)
            { i = 0; }
            else 
            {
                i = 1;
            }

            await _serviceManager.ServiceChatClient.RegistrateUsersAccauntAsync(e._login, e._password, e._name, e._age, i);
           
        }



        private void ShowAccauntArgs(object sender, AccauntArgs e)
        {
            MessageBox.Show(e._login + " ; " + e._password + " ; " + e._name + " ; " + e._age + " ; " + e._sex);
        }



        private void OnUsProfClosed(object sender, EventArgs e)
        {
            registIsInProgress = false;
        }




        public void OnMessageBoxShow(object sender, string msg)
        {
            MessageBox.Show(msg);
        }


        private void OnEnterIntoChatIsAllowed(object sender, EventArgs e)
        {
            enterIntoChat = true;
        }



    }
}
