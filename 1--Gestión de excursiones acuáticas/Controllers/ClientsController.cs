using _1__Gestión_de_excursiones_acuáticas.Data;
using _1__Gestión_de_excursiones_acuáticas.Models;
using _1__Gestión_de_excursiones_acuáticas.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace _1__Gestión_de_excursiones_acuáticas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> GetAll()
        {
            var clientes = _context.Clientes.Select(c => new ClientDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                DocumentoIdentificacion = c.DocumentoIdentificacion,
                Telefono = c.Telefono,
                CorreoElectronico = c.CorreoElectronico
            }).ToList();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDto> GetById(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            var clientDto = new ClientDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                DocumentoIdentificacion = cliente.DocumentoIdentificacion,
                Telefono = cliente.Telefono,
                CorreoElectronico = cliente.CorreoElectronico
            };

            return Ok(clientDto);
        }

        [HttpPost]
        public ActionResult<ClientDto> Create([FromBody] ClientCreateDto request)
        {
            var cliente = new Cliente
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                DocumentoIdentificacion = request.DocumentoIdentificacion,
                Telefono = request.Telefono,
                CorreoElectronico = request.CorreoElectronico
            };

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            var clientDto = new ClientDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                DocumentoIdentificacion = cliente.DocumentoIdentificacion,
                Telefono = cliente.Telefono,
                CorreoElectronico = cliente.CorreoElectronico
            };

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, clientDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return NoContent();
        }
    }
}