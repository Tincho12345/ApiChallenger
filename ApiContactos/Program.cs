using ApiContacto.Data;
using ApiContactos.ContactosMappers;
using ApiContactos.Repository;
using ApiContactos.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuramos la Cadena de Conexi�n a sql server
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Ac� Agregamos los Repositorio
builder.Services.AddScoped<IContactoRepository, ContactoRepository>();

//Agregamos AutoMaper
builder.Services.AddAutoMapper(typeof(ContactosMapper));
//Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
