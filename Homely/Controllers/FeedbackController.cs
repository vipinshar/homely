using Homely.Common.IRepository;
using Homely.Common.Model;
using Homely.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homely.Controllers
{
    public class FeedbackController : Controller
    {
        private IPostRequirmentRepository _IPostRequirmentRepository = null;

        public FeedbackController(IPostRequirmentRepository IPostRequirmentRepository)
        {
            _IPostRequirmentRepository = IPostRequirmentRepository;
        }

        [Route("Feedback/Index")]
        public ActionResult Index()
        {
            return View(new PostFeedbackViewModel());
        }

        [HttpPost]

        [Route("Feedback/Index")]
        public ActionResult Index(PostFeedbackViewModel model)
        {
           int value= _IPostRequirmentRepository.PostFeedback(model);
            if(value>0)

                return View("thankyouIndex", new SuccessMessageViewModel { message = "Thank you for giving your feedback." });
            else
            return View();
        }
    }
}