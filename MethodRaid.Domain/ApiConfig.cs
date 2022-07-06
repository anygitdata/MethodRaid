using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Linq;

namespace MethodRaid.Domain
{
    public class ApiConfig
    {
        private static XElement Get_XElement(string arg)
        {
            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "App.config");

            XElement config = XElement.Load(pathFile);

            XElement res = config.Element(arg);

            if (res is null)
                throw new Exception($"Узел {arg} не найден");

            return res;
        }

        public static IDictionary<string, string> ConnectionString()
        {
            IDictionary<string, string> res = new Dictionary<string, string>();

            XElement xConnection = Get_XElement("connectionStrings");
            foreach (XElement el in xConnection.Elements())
            {
                string key = el.Attribute("name")?.Value;
                string val = el.Attribute("connectionString")?.Value;

                if (key is null || val is null)
                    throw new Exception("Ошибка структуры секции connectionStrings");

                res[key] = val;
            }


            return res;

        }

        public static string Get_SelConnect()
        {
            string resConn = "";
            string strSel = "";

            try
            {
                strSel =
                    ConfigurationManager.ConnectionStrings["KeySelectedConnect"].ConnectionString;

                resConn = ConfigurationManager.ConnectionStrings[strSel].ConnectionString;
            }
            catch
            {
                var dictCon = ConnectionString();
                strSel = dictCon["KeySelectedConnect"];
                resConn =  dictCon[strSel];
            }


            return resConn;
        }

    }
}
