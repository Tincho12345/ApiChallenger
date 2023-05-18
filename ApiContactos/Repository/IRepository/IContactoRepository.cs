using ApiContactos.Modelos;

namespace ApiContactos.Repository.IRepository
{
    public interface IContactoRepository
    {
        ICollection<Contacto> GetContactos();
        ICollection<Contacto> GetContactosCorreo(string telefono, string Email);
        Contacto GetContacto(int ContactoId);
        bool ExisteContacto(string nombre);
        IEnumerable<Contacto> BuscarContacto(string nombre);
        bool ExisteContacto(int id);
        bool CrearContacto(Contacto contacto);
        bool ActualizarContacto(Contacto contacto);
        bool BorrarContacto(Contacto contacto);
        bool Guardar();
    }
}
