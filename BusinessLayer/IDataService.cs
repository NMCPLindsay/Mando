
using MandalorianDB.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MandalorianDB.BusinessLayer
{
    public interface IDataService
    {
        IEnumerable<Episode> GetAll();
        Episode GetById(int id);
        void Add(Episode character);
        void Update(Episode character);
        void Delete(int id);
    }
}
