using System.Collections;

namespace WebContactos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<IEnumerable> GetTodoAsync(string url);

        Task<T> GetAsync(string url, int id);

        Task<bool> CrearAsync(string url,T itemCrear);

        Task<bool> ActualizarAsync(string url, T itemActualizar);

        Task<bool> BorrarAsync(string url, int Id);
    }
}
