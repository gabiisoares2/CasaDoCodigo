using CasaDoCodigo.Interfaces;
using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {

        }

        public async Task<Categoria> FindCategoriaByNome(string nome)
        {
            var categoria = dbSet.Where(x => x.Nome.Contains(nome)).SingleOrDefault();

            return categoria;
        }

        public async Task SaveCategoria(Categoria categoria)
        {            
            if (categoria != null)
            {
                dbSet.Add(categoria);
            }
        }
    }
}
