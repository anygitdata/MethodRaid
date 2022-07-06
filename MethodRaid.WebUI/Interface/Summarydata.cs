using MethodRaid.Domain.ApiDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MethodRaid.WebUI.Interface
{
    public class Summarydata : ISummarydata
    {

        private DB_summaryData summaryData;

        public Summarydata()
        {
            summaryData = new DB_summaryData();
        }

        public int countBookFree()
        {
            return summaryData.Free;
        }

        public int countBookReading()
        {
            return summaryData.Read;
        }

        public int CountBooks()
        {
            return summaryData.AllBooks;
        }

        public int CountClients()
        {
            return summaryData.AllClients;
        }
    }
}