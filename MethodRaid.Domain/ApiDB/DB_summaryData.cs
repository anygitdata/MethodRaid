using System.Collections;
using System.Collections.Generic;

namespace MethodRaid.Domain.ApiDB
{
    public class DB_summaryData
    {

        IDictionary<string, int> SummaryData { get; }

        public int Free { get => SummaryData["free"]; }
        public int Read { get => SummaryData["read"]; }
        public int AllBooks { get => SummaryData["allbook"]; }
        public int AllClients { get => SummaryData["allclient"]; }

        public DB_summaryData()
        {
            SummaryData = Init_SummaryData();
        }

        private IDictionary<string, int> Init_SummaryData()
        {
            IDictionary<string, int> res = new Dictionary<string, int>();

            var sql = @"select
(SELECT count(*) From Books WHERE GetBookId is null) free,
(SELECT count(*) From Books WHERE GetBookId is not null) read,
(SELECT count(*) From Books) allbook,
(select count(*) from Clients) allclient";

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {

                conn.Open();
                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    var dr = comn.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            
                            res["free"] = dr.GetInt32(0);
                            res["read"] = dr.GetInt32(1);
                            res["allbook"] = dr.GetInt32(2);
                            res["allclient"] = dr.GetInt32(3);
                        }
                    }

                    dr.Dispose();
                    
                }

            }

            return res; 

        }

    }
}
