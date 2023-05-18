using ApiContactos.Modelos;
using ApiContactos.Modelos.Dtos;
using AutoMapper;

namespace ApiContactos.ContactosMappers
{
    public class ContactosMapper : Profile
    {
        public ContactosMapper()
        {
            CreateMap<Contacto, ContactoDto>().ReverseMap();
            CreateMap<Contacto, CrearContactoDto>().ReverseMap();
        }
    }
}
