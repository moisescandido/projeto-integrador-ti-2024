using System.Collections;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using Contatos.Models;
using Microsoft.EntityFrameworkCore;
using projeto.Models;

namespace projeto.db
{
    public class Consultas
    {
        private ClimaContexto _db;
        public Consultas(ClimaContexto db)
        {
            _db = db;
        }
        public void SetClima(Clima clima)
        {
            _db.Clima.Add(clima);
            _db.SaveChanges();
        }
        public async Task<Clima> GetClima()
        {
            Clima? clima = _db.Clima.OrderByDescending(p => p.Data)
                       .FirstOrDefault();
            return clima;
        }
    }
}