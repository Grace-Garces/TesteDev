namespace PessoasAPI.Repository.Interfaces
{
    using PessoasAPI.Models;

    public interface IPessoaRepository
    {
        IEnumerable<Pessoa> GetAll();
        Pessoa? GetByCodigo(long codigo);
        IEnumerable<Pessoa> GetByUF(string uf);
        Pessoa Add(Pessoa novaPessoa);
        Pessoa Update(Pessoa pessoaAtualizada);
        void Delete(long codigo);
    }
}