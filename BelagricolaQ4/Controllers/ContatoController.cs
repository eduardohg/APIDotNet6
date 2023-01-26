using BelagricolaQ4.Models;
using BelagricolaQ4.Repositorio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BelagricolaQ4.Controllers
{
    [ApiController]
    [Route("Contato")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }


        [HttpGet]
        public async Task<ActionResult<List<Contato>>> Get()
        {
            List<Contato> contatos = await _contatoRepositorio.GetAll();
            return Ok(contatos);
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<Contato>> GetById(int codigo)
        {
            Contato contato = await _contatoRepositorio.GetByCodigo(codigo);
            if (contato == null)
                return NotFound();
            else
                return Ok(contato);
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Contato contato)
        {
            await _contatoRepositorio.Add(contato);
            return Created("Criado", await _contatoRepositorio.GetByCodigo(contato.Codigo));
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Update([FromBody] Contato contato, int codigo)
        {
            Contato con = await _contatoRepositorio.GetByCodigo(codigo);
            if (con == null)
                return NotFound();
            else
            {
                contato.Codigo = codigo;
                con = await _contatoRepositorio.Update(contato, codigo);
                return Ok();

            }
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> Delete(int codigo)
        {
            Contato con = await _contatoRepositorio.GetByCodigo(codigo);
            if(con == null)
                return NotFound();
            else
            {
                await _contatoRepositorio.Delete(codigo);
                return Ok();
            }
        }
    }
}
