using System.Web.Mvc;
using RegistrationApplication.Models;

namespace RegistrationApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Create", "User");
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDBHandler UserDBH = new UserDBHandler();
                    if(UserDBH.CheckForDuplicate(user.Email))
                    {
                        ViewBag.Message = "User has already been registered";
                        ModelState.Clear();
                    }
                    else if (UserDBH.AddUsers(user))
                    {
                        ViewBag.Message = "User has been registered successfully";
                        ModelState.Clear();
                    }
                }
                return View("Create");
            }
            catch
            {
                ViewBag.Message = "Unable to add user. Please try Again.";
                return View();
            }
        }

    }
}
