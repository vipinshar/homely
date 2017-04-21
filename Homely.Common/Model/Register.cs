using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.Model
{
    public class Register
    {
        public int ownerShipType { get; set; }

        public string Name { get; set; }
        public string userName { get; set; }

        public string emailId { get; set; }

        public string mobile { get; set; }

        public string password { get; set; }

        public int reg_state { get; set; }
        public int locality { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }

        public string permanenetAddress { get; set; }

    }

    public class LoginViewModel
    {
        public string login_username { get; set; }
        public string login_passw { get; set; }

        public string Name { get; set; }
    }
}
