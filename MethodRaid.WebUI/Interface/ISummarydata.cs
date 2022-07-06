using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodRaid.WebUI.Interface
{
    public interface ISummarydata
    {
        int CountClients();
        int CountBooks();
        int countBookReading();
        int countBookFree();

    }
}
