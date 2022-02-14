using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<Categoria> FindCategoriaByNome(string nome);
        Task SaveCategoria(Categoria categoria);
    }
}
