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
    public class PostRequirmentRepository : IPostRequirmentRepository
    {
        homely_Context db = new homely_Context();
        /// <summary>
        /// Responsible for Post requirment.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int PostRequirment(PostRequirmentViewModel model)
        {
            try
            {
                string amen = string.Empty;
                string bhk = string.Empty;
                var _data = model.amenities.Where(x => x.isSelected == true).Select(x => x.AmenityId.ToString()).ToList();
                //var _bhk = model.chbox.Where(x => x.isSelect == true).Select(x => x.val.ToString()).ToList();

                if (_data != null)
                {
                    amen = string.Join(",", _data);
                }
                //if (_bhk != null)
                //{
                //    bhk = string.Join(",", _bhk);
                //}

                //var data = db.PROC_PostRequirment(model.email
                //    , model.SubPropertyType
                //    , model.PropertyCity, "",model.Locality.ToString(), model.location, model.minArea,amen, 1, model.minValue
                //    , model.maxValue
                //    , model.Bedroom
                //    , 1
                //    , model.ownerName
                //    , model.mobile
                //    , model.clientEmail, model.OwnerState, model.OwnerCity, "", model.address1, model.address2, model.address3
                //    , model.pincode);

                var s= db.PROC_PostRequirment(model.email,model.SubPropertyType,model.PropertyCity,"",model.Locality.ToString(),model.minArea,amen,1,model.minValue,model.maxValue,model.Bedroom,1, model.ownerName
                    , model.mobile
                    , model.clientEmail, model.OwnerState, model.OwnerCity, "", model.address1, model.address2, model.address3
                    , model.pincode);

                return s;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// REsponsible for send feedback.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int PostFeedback(PostFeedbackViewModel model)
        {
            tbl_Feedback feedback = new tbl_Feedback();
            feedback.createdDate = DateTime.UtcNow;
            feedback.email = model.Email;
            feedback.isActive = true;
            feedback.mobile = model.Mobile;
            feedback.name = model.Name;
            feedback.feedback = model.Feedback;
            db.tbl_Feedback.Add(feedback);
            int i=db.SaveChanges();
            if (i > 0)
                return i;
            else
                return 0;
        }
    }
}
