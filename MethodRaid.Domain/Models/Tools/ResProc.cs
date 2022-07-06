using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodRaid.Domain.Models.Tools
{
    public class ResProc
    {
        public bool Result { get; set; } = false;
        public string Message { get; set; }

        public object ResObject { get; set; }

        public int ResInt { get; set; }

        public void Fill_intoResAJAX(ResAJAX resAjax)
        {
            if (Result)
                resAjax.result = "ok";
            else
            {
                resAjax.result = "err";
                resAjax.message = Message;
            }
        }

    }
}
