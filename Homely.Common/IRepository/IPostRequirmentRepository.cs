using Homely.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homely.Common.IRepository
{
    public interface IPostRequirmentRepository
    {
        int PostRequirment(PostRequirmentViewModel model);

        int PostFeedback(PostFeedbackViewModel model);
    }
}
