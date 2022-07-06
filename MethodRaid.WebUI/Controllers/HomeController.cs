using MethodRaid.WebUI.Interface;
using System.Web.Mvc;

namespace MethodRaid.WebUI.Controllers
{
    public class HomeController : Controller
    {
        ISummarydata Summarydata;

        public HomeController(ISummarydata sumData)
        {
            Summarydata = sumData;
        }

        // GET: Home
        public ActionResult Index()
        {

            ViewBag.countBookFree = Summarydata.countBookFree();
            ViewBag.countBookReading = Summarydata.countBookReading();
            ViewBag.CountBooks = Summarydata.CountBooks();
            ViewBag.CountClients = Summarydata.CountClients();


            return View(); 
        }
    }
}