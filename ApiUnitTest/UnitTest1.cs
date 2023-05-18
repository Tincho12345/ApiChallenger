using ApiContactos.Controllers;
using ApiContactos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiUnitTest
{
    public class UnitTest1
    {
        private readonly ContactosController _controller;
        private readonly ContactoRepository _service;

        public UnitTest1(ContactosController controller, ContactoRepository service)
        {
            _service = service;
            _controller = controller;
        }
        [Fact]
        public void Test1()
        {
            var result = _controller.GetContactos();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}