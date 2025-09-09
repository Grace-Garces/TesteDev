namespace PessoasAPI.Controllers
{
    using PessoasAPI.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PessoasAPI.DTOs;


    using Models;

    using PessoasAPI.Repository.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaRepository _repository;

        public PessoasController(IPessoaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{codigo}")]
        public IActionResult GetByCodigo(long codigo)
        {
            var pessoa = _repository.GetByCodigo(codigo);
            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpGet("uf/{uf}")]
        public IActionResult GetByUF(string uf)
        {
            return Ok(_repository.GetByUF(uf));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CriarPessoaDto pessoaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novaPessoa = new Pessoa
            {
                Nome = pessoaDto.Nome,
                CPF = pessoaDto.CPF,
                UF = pessoaDto.UF,
                DataNascimento = pessoaDto.DataNascimento
            };

            var pessoaSalva = _repository.Add(novaPessoa);
            return CreatedAtAction(nameof(GetByCodigo), new { codigo = pessoaSalva.Codigo }, pessoaSalva);
        }

        [HttpPut("{codigo}")]
        public IActionResult Update(long codigo, [FromBody] Pessoa pessoa)
        {
            if (codigo != pessoa.Codigo)
                return BadRequest("O código na URL não corresponde ao código no corpo da requisição.");

            var pessoaAtualizada = _repository.Update(pessoa);
            if (pessoaAtualizada == null)
                return NotFound();

            return Ok(pessoaAtualizada);
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(long codigo)
        {
            var pessoa = _repository.GetByCodigo(codigo);
            if (pessoa == null)
                return NotFound();

            _repository.Delete(codigo);
            return NoContent();
        }
    }

}