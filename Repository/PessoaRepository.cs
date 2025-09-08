 namespace PessoasAPI.Repository
{
    using PessoasAPI.Models;
    using PessoasAPI.Repository.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class PessoaRepository : IPessoaRepository
    {
        private static readonly List<Pessoa> _pessoas = new List<Pessoa>
        {
            // dados iniciais para facilitar testes
            new Pessoa { Codigo = 1, Nome = "João da Silva", CPF = "11122233344", UF = "SP", DataNascimento = new System.DateTime(1990, 1, 15) },
            new Pessoa { Codigo = 2, Nome = "Maria Oliveira", CPF = "55566677788", UF = "RJ", DataNascimento = new System.DateTime(1985, 5, 20) },
            new Pessoa { Codigo = 3, Nome = "Carlos Pereira", CPF = "99988877766", UF = "SP", DataNascimento = new System.DateTime(1992, 11, 30) }
        };

        public IEnumerable<Pessoa> GetAll()
        {
            return _pessoas;
        }

        public Pessoa? GetByCodigo(long codigo)
        {

            return _pessoas.FirstOrDefault(p => p.Codigo == codigo);
        }
        public IEnumerable<Pessoa> GetByUF(string uf)
        {
            return _pessoas.Where(p => p.UF.Equals(uf, System.StringComparison.OrdinalIgnoreCase));
        }
        public Pessoa Add(Pessoa novaPessoa)
        {
            long novoCodigo = _pessoas.Any() ? _pessoas.Max(p => p.Codigo) + 1 : 1;
            novaPessoa.Codigo = novoCodigo;

            _pessoas.Add(novaPessoa);
            return novaPessoa; 
        }
        public Pessoa Update(Pessoa pessoaAtualizada)
        {

            var pessoaExistente = GetByCodigo(pessoaAtualizada.Codigo);

            if (pessoaExistente != null)
            {

                pessoaExistente.Nome = pessoaAtualizada.Nome;
                pessoaExistente.CPF = pessoaAtualizada.CPF;
                pessoaExistente.UF = pessoaAtualizada.UF;
                pessoaExistente.DataNascimento = pessoaAtualizada.DataNascimento;
            }

            return pessoaExistente; 
        }
        public void Delete(long codigo)
        {
            var pessoaParaExcluir = GetByCodigo(codigo);
            if (pessoaParaExcluir != null)
            {
                _pessoas.Remove(pessoaParaExcluir);
            }
        }
    }
}