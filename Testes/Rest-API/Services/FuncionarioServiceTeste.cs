using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Entidades.Models;
using Moq;
using Restful_API.Data;
using Restful_API.DTOs;
using Restful_API.Mappings;
using Restful_API.Services.Interfaces;
using Restful_API.Services.Services;
using Xunit;

namespace Testes.Rest_API.Services
{
    public class FuncionarioServiceTeste
    {
        private Mock<IFuncionarioRepository> _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
        private FuncionarioService _service;

        [Fact]
        public async void GetAllAsync_Retorna_Lista_De_FuncionariosDTO()
        {
            // Arrange
            //var service = CriarService();
            var mapper = CriarMapper();
            _service = new FuncionarioService(_funcionarioRepositoryMock.Object, mapper);

            var funcionarios = FuncionariosFactory();

            _funcionarioRepositoryMock
                .Setup(x => x.GetFuncionariosAsync())
                .Returns(Task.FromResult(funcionarios));

            // Act
            var resposta = await _service.GetAllAsync();
            var esperado = mapper.Map<List<FuncionarioDTO>>(funcionarios);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        [Fact]
        public async void GetBydId_Retorna_FuncionarioDTO()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new FuncionarioService(_funcionarioRepositoryMock.Object, mapper);

            var funcionario = new Funcionario("Alexis", "alexis@gmail.com");

            _funcionarioRepositoryMock
                .Setup(x => x.GetFuncionarioByIdAsync(funcionario.Id))
                .Returns(Task.FromResult(funcionario));

            // Act
            var resposta = await _service.GetByIdAsync(funcionario.Id);
            var esperado = mapper.Map<FuncionarioDTO>(funcionario);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        private IMapper CriarMapper()
        {
            var mapProfile = new MappingProfile();
            var mapConfig = new MapperConfiguration(x => x.AddProfile(mapProfile));
            return new Mapper(mapConfig);
        }

        private IFuncionarioService CriarService()
        {
            return new FuncionarioService(_funcionarioRepositoryMock.Object, CriarMapper());
        }

        private IEnumerable<Funcionario> FuncionariosFactory()
        {
            return new List<Funcionario>() {
                new Funcionario("Alexis", "alexis@gmail.com"),
                new Funcionario("Oliver", "oliver@outlook.com"),
                new Funcionario("Amelie", "amelie@terra.com")
            };
        }
    }
}
