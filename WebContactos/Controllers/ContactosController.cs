using Microsoft.AspNetCore.Mvc;
using WebContactos.Modelos;
using WebContactos.Repositorio.IRepositorio;
using WebContactos.Utilidades;

namespace WebContactos.Controllers
{
    public class ContactosController : Controller
    {
        private readonly IContactoRepositorio _repoContactos;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ContactosController(IContactoRepositorio repoContactos, IWebHostEnvironment hostingEnvironment)
        {
            _repoContactos = repoContactos;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Contacto() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosContactos()
        {
            return Json(new { data = await _repoContactos.GetTodoAsync(CT.RutasContactosApi) });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contacto contacto)
        {
            string rutaPrincipal = _hostingEnvironment.WebRootPath;
            if (!ModelState.IsValid)
                return View(contacto);
            //Subida de Imágen
            var archivos = HttpContext.Request.Form.Files;
            if (archivos.Count()>0)
            {
                //Nuevo artículo
                string nombreArchivo = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(archivos[0].FileName);
                var subidas = Path.Combine(rutaPrincipal, @"img\contactos");

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                contacto.UrlImagen = @"\img\contactos\" + nombreArchivo + extension;
                contacto.FechaCreacion = DateTime.Now;
            }           
            await _repoContactos.CrearAsync(CT.RutasContactosApi, contacto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Contacto itemContacto = new Contacto();
            if (id == null) return NotFound();
            itemContacto = await _repoContactos.GetAsync(CT.RutasContactosApi, id.GetValueOrDefault());
            if (itemContacto == null) return NotFound();
            return View(itemContacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                contacto.FechaCreacion = DateTime.Now;
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                int? id = contacto.Id;
                var articuloDesdeBd = await _repoContactos.GetAsync(CT.RutasContactosApi, id.GetValueOrDefault());
                if (archivos.Count() > 0)
                {
                    //Nuevo artículo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"img\contactos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    //Obtenemos la ruta de la imágen del servidor
                    if (articuloDesdeBd.UrlImagen != null)
                    {
                        contacto.UrlImagen = articuloDesdeBd.UrlImagen;
                        var rutaImagenDesdeDb = Path.Combine(rutaPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\'));
                        //Si la Imágen existe se elimina del Servidor
                        if (System.IO.File.Exists(rutaImagenDesdeDb))
                            System.IO.File.Delete(rutaImagenDesdeDb);
                    }
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    contacto.UrlImagen = @"\img\contactos\" + nombreArchivo + extension;
                }
                await _repoContactos.ActualizarAsync(CT.RutasContactosApi +'/'+ contacto.Id, contacto);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        //Borrar Contacto
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var status = await _repoContactos.BorrarAsync(CT.RutasContactosApi, id.GetValueOrDefault());
            if (status)
            {
                return Json(new { success = true, message = "Registro Eliminado Correctamente" });
            }
            return Json(new { success = false, message = "Error Intentando Eliminar el Registro" });
        }
    }
}
