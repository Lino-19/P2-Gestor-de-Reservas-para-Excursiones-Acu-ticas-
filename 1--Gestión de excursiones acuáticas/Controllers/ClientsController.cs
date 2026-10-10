using _1__Gestión_de_excursiones_acuáticas.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1__Gestión_de_excursiones_acuáticas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private static readonly List<Cliente> _clientes = new List<Cliente>();

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            return Ok(_clientes);
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Cliente> Create(Cliente cliente)
        {
            bool existeDocumento = _clientes.Any(c =>
                c.DocumentoIdentificacion.Equals(cliente.DocumentoIdentificacion, StringComparison.OrdinalIgnoreCase));

            if (existeDocumento)
            {
                return BadRequest("Ya existe un cliente registrado con este documento de identificación.");
            }

            cliente.Id = _clientes.Any() ? _clientes.Max(c => c.Id) + 1 : 1;
            _clientes.Add(cliente);

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Cliente cliente)
        {
            var clienteExistente = _clientes.FirstOrDefault(c => c.Id == id);
            if (clienteExistente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            bool documentoDuplicado = _clientes.Any(c =>
                c.Id != id &&
                c.DocumentoIdentificacion.Equals(cliente.DocumentoIdentificacion, StringComparison.OrdinalIgnoreCase));

            if (documentoDuplicado)
            {
                return BadRequest("El documento de identificación ya pertenece a otro cliente.");
            }

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.DocumentoIdentificacion = cliente.DocumentoIdentificacion;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.CorreoElectronico = cliente.CorreoElectronico;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            _clientes.Remove(cliente);
            return NoContent();
        }
    }
}