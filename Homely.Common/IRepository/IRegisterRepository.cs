using Homely.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.IRepository
{
   public interface IRegisterRepository
    {
        string Register(Register model,string image);

        LoginViewModel Login(LoginViewModel model);
    }
}
