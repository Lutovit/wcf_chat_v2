using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChatHost
{
    class UsersAccauntContext:DbContext
    {
        public UsersAccauntContext() : base("DefaultConnection") { }
        public DbSet<UsersAccaunt> UsersAccaunts { set; get; }
    }
}
