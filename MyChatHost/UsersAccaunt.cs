using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcf_chat;

namespace MyChatHost
{
    public enum sex
    {
        Male,
        Female
    }

    public class Sex
    {
        public sex _sex { set; get; }

        public Sex() { }

        public Sex(sex s)
        {
            _sex = s;
        }

        public override string ToString()
        {
            return _sex.ToString();
        }
    }

    class UsersAccaunt
    {
        public int Id { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }

        public Sex sex { set; get; }
    }




}
