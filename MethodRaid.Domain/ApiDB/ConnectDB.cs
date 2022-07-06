
using System.Data.SQLite;

namespace MethodRaid.Domain.ApiDB
{
    public class ConnectDB
    {
        public static SQLiteConnection Get_SQLiteConnection()
        {
            string strConn = ApiConfig.Get_SelConnect();

            var res = new SQLiteConnection($"Data Source={strConn}");

            return res;
        }
    }
}
