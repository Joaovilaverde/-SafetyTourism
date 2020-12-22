using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS_API.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
