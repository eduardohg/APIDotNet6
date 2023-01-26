using BelagricolaQ4.Models;
using BelagricolaQ4.Repositorio;
using BelagricolaQ4.Repositorio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BelagricolaQ4.Controllers
{
    [ApiController]
    [Route("ClienteContato")]
    public class ClienteContatoController : ControllerBase
    {
        private readonly IClienteContatoRepositorio _contatoclienteRepositorio;
        public ClienteContatoController(IClienteContatoRepositorio clienteRepositorio) {
            _contatoclienteRepositorio = clienteRepositorio;

        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteContato>>> Get()
        {
            List<ClienteContato> clienteContatos = await _contatoclienteRepositorio.GetAll();
            return Ok(clienteContatos);
        }

        [HttpGet("{cliente}/{contato}")]
        public async Task<ActionResult<ClienteContato>> GetById(int cliente, int contato)
        {
            ClienteContato clienteContatos = await _contatoclienteRepositorio.GetByCodigo(cliente, contato);
            if (clienteContatos == null)
                return NotFound();
            else
                return Ok(clienteContatos);

        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ClienteContato clienteContato)
        {
            var result = await _contatoclienteRepositorio.Add(clienteContato);
            if(result == null)
                return BadRequest("Já existe esse relacionamento!");
            else
                return Created("Criado", await _contatoclienteRepositorio.GetByCodigo(clienteContato.ClienteCodigo,clienteContato.ContatoCodigo));
        }

        [HttpPut("{cliente}/{contato}")]
        public async Task<ActionResult> Update([FromBody] ClienteContato clienteContato, int cliente, int contato)
        {
            ClienteContato clienteContatos = await _contatoclienteRepositorio.GetByCodigo(cliente, contato);
            if (clienteContatos == null)
                return NotFound();
            else
            {
                clienteContato.ClienteCodigo = cliente;
                clienteContato.ContatoCodigo = contato;
                clienteContatos = await _contatoclienteRepositorio.Update(clienteContato, cliente, contato);
                return Ok(clienteContatos);
            }
        }

        [HttpDelete("{cliente}/{contato}")]
        public async Task<ActionResult> Delete(int cliente, int contato)
        {
            ClienteContato clienteContatos = await _contatoclienteRepositorio.GetByCodigo(cliente, contato);
            if (clienteContatos == null)
                return NotFound();
            else
            {
                await _contatoclienteRepositorio.Delete(cliente, contato);
                return Ok();
            }
        }
    }
}
