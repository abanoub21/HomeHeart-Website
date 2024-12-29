using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Application.Dtos
{
    public class CustomToken
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
