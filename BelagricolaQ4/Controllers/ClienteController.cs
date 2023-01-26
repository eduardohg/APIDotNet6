using Microsoft.AspNetCore.Mvc;
using BelagricolaQ4.Models;
using BelagricolaQ4.Repositorio;
using BelagricolaQ4.Repositorio.Interface;

namespace BelagricolaQ4.Controllers{
    [ApiController]
    [Route("Cliente")]
    public class ClienteController : ControllerBase{

        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio= clienteRepositorio;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get(){
            List<Cliente> clientes = await _clienteRepositorio.GetAll();
            return Ok(clientes);
        }
        
        [HttpGet("{codigo}")]
        public async Task<ActionResult<Cliente>> GetById(int codigo){
            Cliente cliente = await _clienteRepositorio.GetByCodigo(codigo);
            if(cliente == null)
                return NotFound();
            else
                return Ok(cliente);
            
        }
       
       
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Cliente cliente){
            await _clienteRepositorio.Add(cliente);
            return Created("Criado", await _clienteRepositorio.GetByCodigo(cliente.Codigo));
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Update([FromBody] Cliente cliente, int codigo)
        {
            Cliente cli = await _clienteRepositorio.GetByCodigo(codigo);
            if (cliente == null)
                return NotFound();
            else
            {
                cliente.Codigo = codigo;
                cli = await _clienteRepositorio.Update(cliente,codigo);
                return Ok(cli);
            }
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> Delete(int codigo)
        {
            Cliente cli = await _clienteRepositorio.GetByCodigo(codigo);
            if (cli == null)
                return NotFound();
            else
            {
                await _clienteRepositorio.Delete(codigo);
                return Ok();
            }
        }

    }
}