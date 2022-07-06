using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using MethodRaid.WebUI.Interface;
using System.Web.Mvc;

namespace MethodRaid.WebUI.Controllers
{
    public class ClientController : Controller
    {

        IClientRepository clientRepository;

        public ClientController(IClientRepository clientRepo)
        {
            clientRepository = clientRepo;

        }


        public ActionResult Get_ListClient()
        {
            var lsClient = DB_Clients.Get_ListClients();

            return PartialView("_PartialFiltrClients", lsClient);
        }


        // GET: Client
        public ActionResult Index()
        {
            return View(clientRepository.Clients);
        }

        public ActionResult UpClient(int id)
        {

            var resProc = DB_Clients.Get_Client(id);

            ViewBag.PageTitle = "Изм. фамилии";
            ViewBag.message = "Изменение фамилии пользователя";

            return View("NewClient", (Client)resProc.ResObject);
        }


        [HttpPost]
        public ActionResult UpClient(Client client)
        {
            if (string.IsNullOrEmpty(client.ClientName))
                ModelState.AddModelError("ClientName", "Заполните поле Фамилия И.О.");

            if (!ModelState.IsValid)
                return View("NewClient", client);


            var resProc = DB_Clients.UpdClient(client);

            if (!resProc.Result)
            {
                ModelState.AddModelError("ClientName", "Отклонение операции");
                return View();
            }


            return RedirectToAction("index");
        }


        public ActionResult NewClient()
        {
            ViewBag.PageTitle = "Добавить пользователя";
            ViewBag.message = "Добавление участника библиотеки";

            return View(new Client());
        }


        [HttpPost]
        public ActionResult NewClient(Client client)
        {
            if (string.IsNullOrEmpty(client.ClientName))
                ModelState.AddModelError("ClientName", "Заполните поле");

            if (!ModelState.IsValid)
                return View();

            var resProc = DB_Clients.AddClient(client);

            if (!resProc.Result)
            {
                ModelState.AddModelError("ClientName", "Отклонение операции");
                return View();
            }


            return RedirectToAction("index");

        }

    }
}