using PruebaTecnica.DTO.Account;
using PruebaTecnica.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PruebaTecnica.Controllers
{
    public class AccountController : Controller
    {
        AccountViewModel viewModel = new AccountViewModel();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegistration dto)
        {

            if (!await viewModel.CheckUsernameAvailability(dto.Username))
            {
                ModelState.AddModelError("Username", "El nombre de usuario no esta disponible.");
            }

            if (ModelState.IsValid)
            {
                await viewModel.CreateUser(dto);
            }


            return View(dto);
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginRequest login)
        {
            if (viewModel.ValidateUser(login))
            {
                FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);
                Session["UserName"] = login.Username;
                if (login.RememberMe == true)
                {
                    int userTicketVersion = 1;
                    var authTicket = new FormsAuthenticationTicket(userTicketVersion, login.Username, DateTime.Now, DateTime.Now.AddMonths(1), login.RememberMe, "", "/");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Usuario Invalido !");
            return View(login);
        }


        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
           var users = await viewModel.GetAllUser();
            
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var user = await viewModel.GetUser(id);
            if (user != null) 
            {
                return View(user);
            }



         return new HttpStatusCodeResult(400, "Console ID es invalido.");

        }

        public async Task<ActionResult> DisabledUser(int id)
        {
            try
            {
                string username = User.Identity.Name;


                await viewModel.DisabledUser(id);

                return Json(new { success = true, message = "Usuario Eliminado con éxito." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        
    }
}
