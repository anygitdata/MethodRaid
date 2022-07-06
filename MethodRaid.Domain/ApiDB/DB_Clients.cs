using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using System;
using System.Collections.Generic;

namespace MethodRaid.Domain.ApiDB
{
    public class DB_Clients
    {

        public static ResProc Get_Client(int cientId )
        {
            var res = new ResProc();

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {

                string sql = $"Select ClientId, ClientName From Clients WHERE ClientId = @ClientId";

                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;
                    comn.Parameters.AddWithValue("@ClientId", cientId);

                    try
                    {
                        var dr = comn.ExecuteReader();

                        Client client = new Client();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                client.ClientId = dr.GetInt32(0);
                                client.ClientName = dr.GetString(1);
                            }

                            dr.Close();
                            dr.Dispose();
                        }

                        res.ResObject = client;
                        res.Result = true;
                    }
                    catch (Exception ex)
                    {
                        res.Message = ex.Message;
                    }
                }
            }

            return res;
        }


        public static ResProc UpdClient(Client client)
        {
            var res = new ResProc();

            string sql = $"update Clients set ClientName = @clientName where ClientId = @ClientId";


            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    comn.Parameters.AddWithValue("@ClientId", client.ClientId);
                    comn.Parameters.AddWithValue("@clientName", client.ClientName);

                    try
                    {
                        var dr = comn.ExecuteNonQuery();

                        res.Result = true;
                    }
                    catch (Exception ex)
                    {
                        res.Message = ex.Message;
                    }

                }
            }


            return res;

        }


        public static ResProc AddClient(Client client)
        {
            var res = new ResProc();

            string sql = $"insert into Clients (ClientName) values( @ClientName)";

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = sql;

                    comn.Parameters.AddWithValue("@ClientName", client.ClientName);                    

                    try
                    {
                        var dr = comn.ExecuteNonQuery();

                        res.ResInt = ApiDB.GetMaxID(ETypeModel.client);
                        res.Result = true;

                    }
                    catch (Exception ex)
                    {
                        res.Message = ex.Message;
                    }

                }
            }


            return res; 
        }


        public static List<Client> Get_ListClients()
        {

            var res = new List<Client>();

            using (var conn = ConnectDB.Get_SQLiteConnection())
            {
                conn.Open();

                using (var comn = conn.CreateCommand())
                {
                    comn.CommandText = "Select ClientId, ClientName  From Clients";


                    var dr = comn.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            res.Add(
                                new Client { ClientId = dr.GetInt32(0), ClientName = dr.GetString(1) });
                        }
                    }

                    dr.Dispose();
                }


                return res;
            }
        }
    }
}
