using MethodRaid.Domain.ApiDB;
using MethodRaid.Domain.Models;
using MethodRaid.Domain.Models.Tools;
using MethodRaid.InitDB.Models;


namespace MethodRaid.Test.ModelTest
{
    public class Client_Test
    {
        [Fact]
        public void Get_Client_test()
        {
            // arranme
            var context = new DataContext_init();

            var clientId = (API_models.AddClient(context)).ClientId;

            // act
            var resProc = DB_Clients.Get_Client(clientId);


            // assert
            Assert.IsType<ResProc>(resProc);
            Assert.True(resProc.Result);

            Assert.IsType<Client>(resProc.ResObject);

            Assert.Equal(clientId, ((Client)resProc.ResObject).ClientId);
        }



        [Fact]
        public void ClientAdd_test()
        {
            // arrange
            var context = new DataContext_init(); 

            var client = new Client { ClientName = "Новый читатель" };

            // act
            var resProc = DB_Clients.AddClient(client);

            var findClient = context.Clients.Find(resProc.ResInt);

            // assert
            Assert.IsType<ResProc>(resProc);
            Assert.True(resProc.Result);

            Assert.NotNull(findClient);

        }


        [Fact]
        public void Update_clientName_test()
        {
            // arrange
            var context = new DataContext_init();//  APIcontext_Test.Context; // new DataContext_init();

            string clientName = "Измененное имя читателя";

            var client = API_models.AddClient(context);
            client.ClientName = clientName;

            // act 
            var resProc = DB_Clients.UpdClient(client);

            // assert
            Assert.IsType<ResProc>(resProc);
            Assert.True(resProc.Result);

            var clientromDB = context.Clients.Find(client.ClientId);
            Assert.Equal(clientName, clientromDB.ClientName);

        }

    }
}
