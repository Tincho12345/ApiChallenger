using WebContactos.Modelos;
using WebContactos.Repositorio.IRepositorio;

namespace WebContactos.Repositorio
{
    public class ContactoRepositorio : Repositorio<Contacto>, IContactoRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public ContactoRepositorio(IHttpClientFactory clientFactory) : base(clientFactory) 
        {
            _clientFactory = clientFactory;
        }
    }
}
