using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using wcf_chat;

namespace MyChatHost
{ 
    
   [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat, IPrivateMessageService
    {
        List<ServerUser> _users = new List<ServerUser>();           //список объектов User
        List<PrivateMessage> privateMessageList = new List<PrivateMessage>();  // список объектов PrivateMessage


        int nextID = 1;
        int nextPmId = 1;


        // Методы интерфейса IServiceChat
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {                
                Id = nextID,
                UserName = name,
                ChatCallback = OperationContext.Current.GetCallbackChannel<IServiceChatCallBack>(),
            };

            

            nextID++;
            _users.Add(user);
            SendMsg(user.UserName + "   подключился к чату! ", user.Id);   // ID начинаются с 1, по id=0 сервер ничего непошлет. 
            Console.WriteLine("объект ServerUser создан. ID=" + user.Id + " ; name= " + user.UserName);
            UpdateUsersList();
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = _users.FirstOrDefault(i => i.Id == id);
            if (user != null)
            {
                SendMsg(user.UserName + "   покинул чат! ", user.Id);
                _users.Remove(user);                
                Console.WriteLine("объект ServerUser отсоединился ID=" + user.Id + " ; name=" + user.UserName);
                UpdateUsersList();
            }
            
        }

        public void SendMsg(string msg, int senderId)
        {
            var sender = _users.FirstOrDefault(i => i.Id == senderId);
            if (sender != null)
            {
                string fullMessage = String.Format("{0}: {1}: {2} ", DateTime.Now.ToShortTimeString(), sender.UserName, msg);

                //WrightIntoTextFile(fullMessage);

                foreach (var user in _users)
                {
                    user.ChatCallback.OnMessageReceived(fullMessage);
                }
                UpdateUsersList();
            }
            
        }

        public void UpdateUsersList()
        {
            var userNames = GetUsersList().OrderBy(u => u).ToList();

            foreach (var item in _users)
            {
                item.ChatCallback.OnUsersListChanged(userNames);
            }
        }

        public bool AskAdresseeToPrivateMessage(string adresseeName, string senderName)
        {
            bool success = false;
            ServerUser adressee = _users.Where(x => x.UserName == adresseeName).FirstOrDefault();
            if (adressee != null && adressee.PrivateMessageCallback==null)
            {
                adressee.ChatCallback.OnPrivateChatAskAccepted(adresseeName, senderName);
                success = true;
            }
            return success;
        }

        // Методы интерфейса IPrivateMessage


        public int CreatePrivateMessage(string SenderName, string AdresseeName)
        {
            PrivateMessage pm = new PrivateMessage()
            {
                Sender = SenderName,
                Adressee = AdresseeName,
                ID = nextPmId
            }; //Определитесь с сущностями PrivateMessage и юзер

            string answer = DateTime.Now.ToShortTimeString();
            
            Console.WriteLine(answer + " : Объект личное сообщение создан!");
            Console.WriteLine("отправитель - " + pm.Sender + " ; получатель - " + pm.Adressee + "; ID=" + pm.ID);

            var ob = _users.FirstOrDefault(i => i.UserName == pm.Sender);//Оба колбэка я положил в юзер. При этом важно, чтобы контекст приватного сообщения был создан во время исполнения именно методов его интерфейса.

            if(ob!=null && ob.PrivateMessageCallback==null)
            {
                ob.PrivateMessageCallback = OperationContext.Current.GetCallbackChannel<IPrivateMessageServiceCallBack>();
            }
            if (ob != null && ob.PrivateMessageCallback != null)
            {
                ob.PrivateMessageCallback.OnPrivateConnected("Личный чат с " + pm.Adressee + " создан!");
            }


            nextPmId++;
            privateMessageList.Add(pm);
            return pm.ID;          
        }


        public void SendPrivateMsg(string sender, string adressee, string msg)    //Max
        {
            Console.WriteLine(sender + " ; " + adressee + " ; " + msg);

            string answer = DateTime.Now.ToShortTimeString();
            msg = answer + " : " + msg;

            var ob = _users.FirstOrDefault(i => i.UserName == adressee);
            if (ob != null && ob.PrivateMessageCallback == null)
            {
                //ob.PrivateMessageCallback = OperationContext.Current.GetCallbackChannel<IPrivateMessageServiceCallBack>(); Вы записываете контекст сендера на место контекста адресата
                // Правильно работает только когда два окна открыту друг другу(т.е. контексты приватных сообщений создались при создании окон
            }
            if (ob != null && ob.PrivateMessageCallback != null)
            {
                //ob.PrivateMessageCallback.OnPrivateMessageReceived(answer + " : " + msg);
                ob.PrivateMessageCallback.OnPrivateMessageReceived_v2(sender, adressee, msg);
                Console.WriteLine("получатель - " + ob.UserName);
            }

        }


        public IEnumerable<string> GetUsersList()
        {
            return _users.Select(u => u.UserName);
        }

        public string GetUserName(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            return user != null ? user.UserName : String.Empty;
        }

        public static void WrightIntoTextFile(string msg)  // метод записывает данные в текстовый файл
        {
            string adr = "E:\\generalChatServerLogfile.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(adr, true))
                {
                    sw.WriteLine(msg);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        /*-------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public  async void RegistrateUsersAccaunt(string login, string password, string name, int age, int i)
        {
            //Console.WriteLine(login +" ; " + password + " ; " + name + " ; " + age + " ; " + i);

            sex s;

            if (i == 0)
            {
                s = sex.Male;
            }
            else 
            {
                s = sex.Female;            
            }

            UsersAccaunt usersaccaunt = new UsersAccaunt()
            {
                Login = login,
                Password = password,
                Name = name,
                Age = age,
                sex = new Sex(s)
            };



            try 
            {
                using (UsersAccauntContext ua = new UsersAccauntContext())
                {
                    ua.UsersAccaunts.Add(usersaccaunt);
                    await ua.SaveChangesAsync();
                    Console.WriteLine("New object UsersAccaunt: ");
                    Console.WriteLine("----New object UsersAccaunt:  " + usersaccaunt.Login + " ; " +  usersaccaunt.Name + " ; " + 
                        usersaccaunt.sex.ToString() + " has been added to the database. ");



                    /*-----------------------------------Doesnt worK i dont know why =(------------------------------------------------*/

                    string msg = "Your accaunt was created saccessfully =)!";

                    ServerUser temp = new ServerUser()
                    {
                        Id = nextID,
                        UserName = name,
                        ChatCallback = OperationContext.Current.GetCallbackChannel<IServiceChatCallBack>(),
                    };                   

                    temp.ChatCallback.OnMessageBoxShow(msg);             // не срабтывает колбэк      ..... блин =) ... ссылку на службу обновил и все заработало! =))    
                   
                    /*-----------------------------------------------------------------------------------------------------------------*/

                }

            }
            catch 
            {
                Console.WriteLine("Thomething is wrong with adding an object to DataBase! ");            
            }





        }

        public async void DeleteUsersAccaunt(string login, string password)
        {
            try
            {
                using (UsersAccauntContext ua = new UsersAccauntContext())
                {
                    UsersAccaunt usersaccaunt = new UsersAccaunt();
                    usersaccaunt = ua.UsersAccaunts.FirstOrDefault(ob => ob.Login == login && ob.Password == password);
                    if (usersaccaunt != null)
                    {

                        /*-----------------------------------------------------------------------------------------------------*/
                        string msg = "Your accaunt was deleted saccessfully =(!";
                       
                        ServerUser temp = new ServerUser()
                        {
                            Id = nextID,
                            UserName = usersaccaunt.Name,
                            ChatCallback = OperationContext.Current.GetCallbackChannel<IServiceChatCallBack>(),
                        };

                        ua.UsersAccaunts.Remove(usersaccaunt);
                        await ua.SaveChangesAsync();

                        temp.ChatCallback.OnMessageBoxShow(msg);
                        /*-----------------------------------------------------------------------------------------------------*/


                        Console.WriteLine("---UsersAccaunt: " + " : " + usersaccaunt.Login + " ; " + usersaccaunt.Name + " ; " 
                            + usersaccaunt.sex.ToString() + " :   has been removed from the database.");                        
                    }
                    else 
                    {
                        Console.WriteLine("Object is not found in data base! ");
                    }                   
                    
                }
            }
            catch
            {
                Console.WriteLine("Thomething is wrong with deleting an object from DataBase! ");
            }
        }

        public void MayIComeIn(string login, string password)
        {
            try
            {
                using (UsersAccauntContext ua = new UsersAccauntContext())
                {
                    UsersAccaunt usersaccaunt = new UsersAccaunt();
                    usersaccaunt = ua.UsersAccaunts.FirstOrDefault(ob => ob.Login == login && ob.Password == password);
                    if (usersaccaunt != null)
                    {
                        ServerUser temp = new ServerUser()
                        {
                            Id = nextID,
                            UserName = usersaccaunt.Name,
                            ChatCallback = OperationContext.Current.GetCallbackChannel<IServiceChatCallBack>(),
                        };

                        temp.ChatCallback.OnEnterIntoChatIsAllowed();
                        Console.WriteLine("User " + login + " is allowed to connect!");
                    }

                    else
                    {
                        Console.WriteLine("Object is not found in data base! ");
                    }

                }
            }
            catch
            {
                Console.WriteLine("Thomething is wrong with MayIComeIn method! ");
            }
        }

        public bool MayIComeIn_v2(string login, string password)
        {
            try
            {
                using (UsersAccauntContext ua = new UsersAccauntContext())
                {
                    UsersAccaunt usersaccaunt = new UsersAccaunt();
                    usersaccaunt = ua.UsersAccaunts.FirstOrDefault(ob => ob.Login == login && ob.Password == password);
                    if (usersaccaunt != null)
                    {
                        return true;
                    }

                    else
                    {
                        Console.WriteLine("Object is not found in data base! ");
                        return false;
                    }

                }
            }
            catch
            {
                Console.WriteLine("Thomething is wrong with MayIComeIn_v2 method! ");
                return false;
            }
        }
    }
}
