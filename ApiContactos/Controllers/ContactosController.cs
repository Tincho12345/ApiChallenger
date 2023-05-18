using ApiContactos.Modelos;
using ApiContactos.Modelos.Dtos;
using ApiContactos.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiContactos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly IContactoRepository _contRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public ContactosController(IContactoRepository pelRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _contRepo = pelRepo;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// Obtener todas las peliculas
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(201, Type = typeof(ContactoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetContactos()
        {
            var listaPeliculas = _contRepo.GetContactos();

            var listaPeliculasDto = new List<ContactoDto>();

            foreach (var lista in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<ContactoDto>(lista));
            }
            return Ok(listaPeliculasDto);
        }



        /// </summary>
        /// <param name="Telefono"> Este es el nombre a buscar</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get Phone or Email")]

        public IActionResult BuscarTelefono(string Telefono, string email)
        {
            try
            {
                var listaContacto = _contRepo.GetContactosCorreo(Telefono, email);
                if (listaContacto.Count() == 0)
                    return NotFound();

                var itemContacto = new List<ContactoDto>();
                foreach (var item in listaContacto)
                {
                    itemContacto.Add(_mapper.Map<ContactoDto>(item));
                }

                return Ok(itemContacto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicación");
            }
        }

        [HttpGet("{contactoId:int}", Name = "GetContacto")]
        public IActionResult GetContacto(int contactoId)
        {
            var itemContactos = _contRepo.GetContacto(contactoId);

            if (itemContactos == null)
            {
                return NotFound();
            }

            var itemContactosDto = _mapper.Map<ContactoDto>(itemContactos);
            return Ok(itemContactosDto);
        }

     

        /// <summary>
        /// Buscar contactos por nombre o Email
        /// </summary>
        /// <param name="nombre"> Este es el nombre a buscar</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _contRepo.BuscarContacto(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicación");
            }
        }

        /// <summary>
        /// Crear una nuevo contacto
        /// </summary>
        /// <param name="contactoDto"></param>
        /// <returns></returns>       
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ContactoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearContacto([FromBody] ContactoDto contactoDto)
        {
            if (contactoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_contRepo.ExisteContacto(contactoDto.Nombre))
            {
                ModelState.AddModelError("", "La película ya existe");
                return StatusCode(404, ModelState);
            }

            /////*subida de archivos*/
            //var archivo = contactoDto.Foto;
            //string rutaPrincipal = _hostingEnvironment.WebRootPath;
            //var archivos = HttpContext.Request.Form.Files;

            //if (archivos != null)
            //{
            //    //Nueva imagen
            //    var nombreFoto = Guid.NewGuid().ToString();
            //    var subidas = Path.Combine(rutaPrincipal, @"fotos");
            //    var extension = Path.GetExtension(archivos[0].FileName);

            //    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create))
            //    {
            //        archivos[0].CopyTo(fileStreams);
            //    }
            //    contactoDto.RutaImagen = @"\fotos\" + nombreFoto + extension;
            //}

            var contacto = _mapper.Map<Contacto>(contactoDto);

            if (!_contRepo.CrearContacto(contacto))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{contacto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetContacto", new { contactoId = contacto.Id }, contacto);
        }

        /// <summary>
        /// Actualizar un contacto existente
        /// </summary>
        /// <param name="contactoId"></param>
        /// <param name="contactoDto"></param>
        /// <returns></returns>   
        [HttpPatch("{contactoId:int}", Name = "ActualizarContacto")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarContacto(int contactoId, [FromBody] ContactoDto contactoDto)
        {
            if (contactoDto == null || contactoId != contactoDto.Id)
            {
                return BadRequest(ModelState);
            }

            var contacto = _mapper.Map<Contacto>(contactoDto);

            bool Actualizado = _contRepo.ActualizarContacto(contacto);

            if (Actualizado==false)
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{contacto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Borrar una contacto existente
        /// </summary>
        /// <param name="contactoId"> Este es el id de la película</param>
        /// <returns></returns>
        [HttpDelete("{contactoId:int}", Name = "BorrarContacto")]
        public IActionResult BorrarContacto(int contactoId)
        {
            if (!_contRepo.ExisteContacto(contactoId))
            {
                return NotFound();
            }

            var contacto = _contRepo.GetContacto(contactoId);

            if (!_contRepo.BorrarContacto(contacto))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{contacto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

