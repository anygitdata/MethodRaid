using MethodRaid.Domain.Models;
using System.Text;

namespace MethodRaid.InitDB
{
    public class SeedData
    {
        public static List<Client> SeedClients_fromFile()
        {
            var res = new List<Client>();

            string curDir = Directory.GetCurrentDirectory();
            string pathFile = Path.Combine(curDir, "SeedClients.txt");

            if (!File.Exists(pathFile))
            {
                throw new Exception("Файл не найден");
            }

            using (var f = new StreamReader(pathFile, Encoding.UTF8))
            {
                string _s;
                int id = 1;

                while ((_s = f.ReadLine()) != null)
                {
                    var buf = _s.Trim();
                    if (buf.Length == 0) continue;

                    if (buf[0] == '#') continue;

                    res.Add(new Client { ClientId = id++, ClientName = buf });
                }

            }

            return res;
        }


        public static List<Book> SeedBooks_fromFile()
        {
            var res = new List<Book>();

            string curDir = Directory.GetCurrentDirectory();
            string pathFile = Path.Combine(curDir, "SeedBooks.txt");

            if (!File.Exists(pathFile))
            {
                throw new Exception("Файл не найден");
            }


            using (var f = new StreamReader(pathFile, Encoding.UTF8))
            {
                string _s;
                int id = 1;

                while ((_s = f.ReadLine()) != null)
                {
                    var buf = _s.Trim();
                    if (buf.Length == 0) continue;

                    if (buf[0] == '#') continue;


                    var ar = buf.Split("##");

                    res.Add(new Book { BookId = id++,  Title = ar[0], Description = ar[1] });

                }
            
            }

            return res; 
        }

    }
}
