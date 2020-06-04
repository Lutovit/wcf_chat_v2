using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (var host = new ServiceHost(typeof(ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Host is running!");
                Console.ReadLine();
            }
            


            //Testing DB

            /*   В базе данных столбец таблицы куда будет закидываться значение пола пользователя в моем случае должен называться  sex__sex и иметь тип int. 
             *   (общий вид 1ХХХ_2ХХХ где 1ХХХ - имя поля  типа данных Т в классе объекта который вы записываете в базу, а 2ХХХ - имя поля типа перечисления в 
             *  классе Т. Эти имена записываются через "_". У меня поле типа перечисления sex в классе Sex называется _sex , следовательно 
             *  у меня имя столбца таблицы с данными о поле записано через два символа "_", а именно sex__sex):
             sex - поле типа данных Sex (класс Sex) в классе UsersAccaunt
             _sex - поле типа sex (перечисление sex) в классе Sex    */

            /*
            UsersAccaunt usersaccaunt = new UsersAccaunt()
            {
                Login = "Johny",
                Password = "123Fuck123",
                Name = "Semeon",
                Age = 15,
                sex = new Sex(sex.Male)
            };

            
            using (UsersAccauntContext ua = new UsersAccauntContext())
            {
                ua.UsersAccaunts.Add(usersaccaunt);
                ua.SaveChanges();
            }
            

            using (UsersAccauntContext ua = new UsersAccauntContext()) 
            {
                List<UsersAccaunt> list = ua.UsersAccaunts.ToList();
                foreach (var l in list) 
                {
                    Console.WriteLine(l.Id + " ; " + l.Login + " ; " + l.Password + " ; " + l.Name + " ; " + l.Age + " ; " + l.sex);                
                }
            }
                

                Console.ReadLine();
            */


            /*
            try
            {
                using (UsersAccauntContext ua = new UsersAccauntContext())
                {
                    UsersAccaunt temp = new UsersAccaunt();
                    temp = ua.UsersAccaunts.FirstOrDefault(ob => ob.Login == "BulletMaster" && ob.Password == "an7163");

                    if (temp != null)
                    {
                        ua.UsersAccaunts.Remove(temp);
                        ua.SaveChanges();
                        Console.WriteLine("Object is removed from data base! ");

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
            */





            Console.ReadLine();
        }
         
    }
}
