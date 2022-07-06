using MethodRaid.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodRaid.Domain.ApiDB
{
    public class ApiDB
    {

        public static int GetMaxID(ETypeModel typeModel)
        {
            int res = 0;

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {

                    switch (typeModel)
                    {
                        case ETypeModel.book:
                            comn.CommandText = "SELECT MAX(BookId) From Books";
                            break;
                        case ETypeModel.client:
                            comn.CommandText = "SELECT MAX(ClientId) From Clients";
                            break;
                        case ETypeModel.getbook:
                            comn.CommandText = "select MAX(GetBookId) from GetBooks";
                            break;
                            
                        default:
                            throw new Exception("ApiDB.GetMaxId: Ошибка параметра typeModel");
                    }                        

                    var data = comn.ExecuteScalar();
                    var sData = data.ToString();

                    res = string.IsNullOrEmpty(sData) ? 0 : int.Parse(sData);
                }
            }

            return res;
        }

    }
}
