using ApiContacto.Data;
using ApiContactos.Modelos;
using ApiContactos.Repository.IRepository;

namespace ApiContactos.Repository
{
    public class ContactoRepository : IContactoRepository
    {
        private readonly ApplicationDbContext _bd;
        public ContactoRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarContacto(Contacto contacto)
        {           
            _bd.Contactos.Update(contacto);
            return Guardar();
        }

        public bool BorrarContacto(Contacto contacto)
        {
            _bd.Contactos.Remove(contacto);
            return Guardar();
        }

        public IEnumerable<Contacto> BuscarContacto(string nombre)
        {
            IQueryable<Contacto> query = _bd.Contactos;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre));
            }

            return query.ToList();
        }

        public bool CrearContacto(Contacto contacto)
        {
            _bd.Contactos.Add(contacto);
            return Guardar();
        }

        public bool ExisteContacto(string nombre)
        {
            bool valor = _bd.Contactos.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteContacto(int id)
        {
            return _bd.Contactos.Any(c => c.Id == id);
        }

        public Contacto GetContacto(int ContactoId)
        {
            return _bd.Contactos.FirstOrDefault(c => c.Id == ContactoId);
        }

        public ICollection<Contacto> GetContactos()
        {
            return _bd.Contactos.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        //Implementar para Filtrar por Correo o Número de Teléfono
        public ICollection<Contacto> GetContactosCorreo(string telefono, string email)
        {
            return _bd.Contactos.Where(e => e.Email.Contains(email) || e.PhoneNumber.Contains(telefono)).ToList();
        }
    }
}
