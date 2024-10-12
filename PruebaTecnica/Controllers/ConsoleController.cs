
using DataAccess;
using PruebaTecnica.DTO.Console;
using PruebaTecnica.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PruebaTecnica.Controllers
{
    [Authorize]
    public class ConsoleController : Controller
    {

        ConsoleViewModel viewModel = new ConsoleViewModel();

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var consoles = await viewModel.GetConsoles();
            return View(consoles);
        }
        
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var console = await viewModel.GetConsoleDetails(id);
            console.Brands = await viewModel.GetBrandSelector();
            if (console != null)
            {
                return View(console);
            }
            return new HttpStatusCodeResult(400, "Console ID es invalido.");
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateConsoleDTO dto)
        {

            string username = User.Identity.Name;
            dto.username = username;

            if (ModelState.IsValid)
            {
                await viewModel.UpdateConsole(dto);

                return RedirectToAction("Index");

            }

            return RedirectToAction("Details","Console");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {   
            CreateConsoleDTO consoleCreate = new CreateConsoleDTO();
            consoleCreate.Brands = await viewModel.GetBrandSelector();
            return View(consoleCreate);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateConsoleDTO dto)
        {
            string username = User.Identity.Name;
            dto.username = username;

            if (ModelState.IsValid)
            {
                await viewModel.CreateConsole(dto);
                return RedirectToAction("Index");

            }
            dto.Brands = await viewModel.GetBrandSelector();

            return View(dto);


        }

        [HttpPost]
        public async Task<ActionResult> DisabledConsole(int id)
        {
            try
            {
                string username = User.Identity.Name;


                await viewModel.DisableConsole(id,username);

                return Json(new { success = true, message = "Consola Eliminada con éxito." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}