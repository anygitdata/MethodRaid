using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using System.Web.Http;

namespace MethodRaid.WebUI.Controllers
{
    public class WebController : ApiController
    {

        [HttpPost]
        public ResAJAX TakeBook(WebBook book)
        {
            var resProc = DB_BooksRead.Web_TakeBook(book);

            return resProc;
        }


        [HttpDelete]
        public ResAJAX ReturnBook(WebBook book)
        {
            var resProc = DB_BooksRead.Web_ReturnBook(book);

            return resProc; 
        }


    }
}
