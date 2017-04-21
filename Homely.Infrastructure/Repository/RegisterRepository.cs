using Homely.Common.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homely.Common.Model;
using Homely.Infrastructure.Database;

namespace Homely.Infrastructure.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        homely_Context db = new homely_Context();

        public LoginViewModel Login(LoginViewModel model)
        {
            var data = db.PROC_Login(model.login_username, model.login_passw).FirstOrDefault();
            if (data != null)
            {
                return new LoginViewModel { login_username = data.EmailId, Name = data.FirstName };
            }
            return null;
        }

        public string Register(Register model, string image)
        {
            string str = string.Empty;
            try
            {
            switch (model.ownerShipType)
            {
                case 1:
                   str = db.PROC_Owner(model.emailId, model.password, model.locality, model.ownerShipType, model.Name, model.mobile, "", "Insert", "").FirstOrDefault();
                    break;
                case 2:
                    str = db.PROC_Agent(model.emailId, model.password, model.locality, model.ownerShipType, model.Name, model.mobile, "", model.companyName, model.companyAddress, "", "", "", image, "Insert", "").FirstOrDefault();
                    break;
                case 3:
                    str = db.PROC_Tenant(model.emailId, model.password, model.locality, model.ownerShipType, model.Name, model.mobile, "", model.companyAddress, model.permanenetAddress, "Insert", "").FirstOrDefault();
                    break;
                case 4:
                    str = db.PROC_Builder(model.emailId, model.password, model.locality, model.ownerShipType, model.Name, model.mobile, "", model.companyName, model.companyAddress, "", "", image, "Insert", "").FirstOrDefault();
                    break;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return str;
        }

        //public ProfileViewModel getUserDetail(string emailId)
        //{

        //}
    }
}
