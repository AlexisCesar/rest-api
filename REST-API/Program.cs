using AutoMapper;
using Restful_API.Data;
using Restful_API.Mappings;
using Restful_API.Services.Interfaces;
using Restful_API.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

// Configure DI
builder.Services.AddScoped<IContratoCLTRepository, ContratoCLTRepository>();
builder.Services.AddScoped<IContratoPJRepository, ContratoPJRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IFuncionarioContratoRepository, FuncionarioContratoRepository>();

builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IContratoCLTService, ContratoCLTService>();
builder.Services.AddScoped<IContratoPJService, ContratoPJService>();
builder.Services.AddScoped<IFuncionarioContratoService, FuncionarioContratoService>();

// Mapper
var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));

IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
