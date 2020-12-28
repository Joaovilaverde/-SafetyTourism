using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class UserInfo
    {
        private string email = "cliente@upskill.pt";
        public string password = "123Pa$$word";

        public string Email
        {
            get { return email; }
        }
        public string Password
        {
            get { return password; }
        }
    }
}
