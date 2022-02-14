using CasaDoCodigo.Interfaces;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<BuscaDeProdutosViewModel> GetProdutos(string pesquisa)
        {
            List<Produto> produtos = new List<Produto>();
            if(string.IsNullOrWhiteSpace(pesquisa))
                produtos = await dbSet.Select(p => p).Include(c => c.Categoria).ToListAsync();
            else
                produtos = await dbSet.Select(p => p).Where(x => x.Nome.Contains(pesquisa) || x.Categoria.Nome.Contains(pesquisa)).Include(c => c.Categoria).ToListAsync();
            var buscaProdutos = new BuscaDeProdutosViewModel(produtos);
            return buscaProdutos;
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    if(string.IsNullOrWhiteSpace(livro.Categoria))
                        throw new ArgumentException("Categoria não pode estar vazia.");

                    var categoria = _categoriaRepository.FindCategoriaByNome(livro.Categoria).Result;

                    if (categoria == null)
                    {
                        categoria = new Categoria() { Nome = livro.Categoria };
                        await _categoriaRepository.SaveCategoria(categoria);
                    }
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                    await contexto.SaveChangesAsync();
                }
            }
            
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
