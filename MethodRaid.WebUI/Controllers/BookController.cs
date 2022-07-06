using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using MethodRaid.WebUI.Interface;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace MethodRaid.WebUI.Controllers
{
    public class BookController : Controller
    {

        IBookRepository bookRepository;

        public BookController(IBookRepository bookRepos)
        {
            bookRepository = bookRepos;

        }


        // GET: Book
        public ActionResult Index()
        {

            ViewBag.lsClient = DB_Clients.Get_ListClients();

            return View(bookRepository.Books);
        }


        public ActionResult FiltrBooks(string id)
        {

            return PartialView("_PartialFiltrBooks", id);

        }


        public ActionResult UpDiscr(int id)
        {
            var resProc = DB_Books.Get_Book(id);


            return View((Book)resProc.ResObject);
        }


        [HttpPost]
        public string UpDiscr(Book book)
        {
            var res = new ResAJAX();

            var resProc = DB_Books.UpdDescr(book);

            resProc.Fill_intoResAJAX(res);


            string resSer = JsonConvert.SerializeObject(res);

            return resSer;
        }


        public ActionResult NewBook()
        {
            var book = new Book();

            return View(book);
        }


        [HttpPost]
        public string NewBook(Book book)
        {
            var res = new ResAJAX();


            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Description))
            {
                res.result = "err";
                res.message = "Заполните поля формы";
            }
            else
            {
                var resProc = DB_Books.AddBook(book);
                resProc.Fill_intoResAJAX(res);
            }


            string resSer = JsonConvert.SerializeObject(res);

            return resSer;

        }


    }
}