using FinalProject.Business;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ICategoryManager categoryManager;
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;

        public HomeController(/*ICategoryManager categoryManager,*/
                              IClassManager classManager,
                              IUserManager userManager)
        {
            //this.categoryManager = categoryManager;
            this.classManager = classManager;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Home/Register
        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.Email, registerModel.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Password and Confirm password do not match.");
                    return View();
                }
                else
                {
                    var loginModel = new LoginModel() { UserEmail = user.UserEmail, UserPassword = user.UserPassword };
                    return LogIn(loginModel, null);
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        // GET: /Home/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserEmail, loginModel.UserPassword);

                if (user == null)
                {
                    ModelState.AddModelError("", "User email and password do not match.");
                }
                else
                {
                    Session["User"] = new FinalProject.Models.UserModel { UserId = user.UserId, UserEmail = user.UserEmail, UserPassword = user.UserPassword };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserEmail, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        public ActionResult ClassList()
        {
            var classList = classManager.ClassList();
            return View(classList);
        }

        public ActionResult StudentClasses()
        {
            if (Session["User"] != null)
            {
                var user = Session["User"] as FinalProject.Models.UserModel;
                var studentClasses = classManager.StudentClasses(user.UserId);
                return View(studentClasses);
            }
            else
            {               
                return RedirectToAction("Login");
            }
                
        }

        public ActionResult EnrollInClass()
        {
            if (Session["User"] != null)
            {
                var classList = classManager.ClassList();
                var select = new SelectList(classList, "ClassId", "ClassName");
                var enrollModel = new FinalProject.Models.EnrollClassModel() { ClassList = select };
                return View(enrollModel);
            }
            else
            {
                return RedirectToAction("Login"); 
            } 
        }

        [HttpPost]
        public ActionResult EnrollInClass(FinalProject.Models.EnrollClassModel enrollClassModel)
        {
            var user = Session["User"] as FinalProject.Models.UserModel;
            var userClass = classManager.AddClass(enrollClassModel.SelectedClassId, user.UserId);
            return RedirectToAction("StudentClasses");
        }
    }
}