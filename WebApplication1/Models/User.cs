using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public User(string email) 
        { 
            this.email = email;
        }
        public string email {  get; set; }
        public string name { get; set; }
    }
}
