using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Entidades.Models;
using Moq;
using Restful_API.Data;
using Restful_API.DTOs;
using Restful_API.Mappings;
using Restful_API.Services.Services;
using Xunit;

namespace Testes.Rest_API.Services
{
    public class FuncionarioServiceTeste
    {
        private Mock<IFuncionarioRepository> _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
        private FuncionarioService? _service;

        [Fact]
        public void Instanciavel()
        {
            _service = new FuncionarioService(_funcionarioRepositoryMock.Object, CriarMapper());

            Assert.NotNull(_service);
        }

        [Fact]
        public async void GetAllAsync_Retorna_Lista_De_FuncionariosDTO()
        {
            // Arrange
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

        [Fact]
        public async void InsertAsync_Retorna_DTO_Do_Funcionario_Criado()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new FuncionarioService(_funcionarioRepositoryMock.Object, mapper);

            var createRequest = new CreateFuncionarioRequest()
            {
                Nome = "Adrian",
                Email = "adrian@gmail.com"
            };

            _funcionarioRepositoryMock
                .Setup(x => x.InsertFuncionarioAsync(It.IsAny<Funcionario>()))
                .Returns(Task.CompletedTask);

            // Act
            var resposta = await _service.InsertAsync(createRequest);

            // Assert
            Assert.NotEqual(Guid.Empty, resposta.Id);
            Assert.Equal(createRequest.Nome, resposta.Nome);
            Assert.Equal(createRequest.Email, resposta.Email);
        }

        [Fact]
        public async void UpdateAsync_Retorna_FuncionarioDTO_Atualizado()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new FuncionarioService(_funcionarioRepositoryMock.Object, mapper);

            var funcionarioRegistrado = new Funcionario("Oliver", "Oliver@gmail.com");

            var updateRequest = new UpdateFuncionarioRequest()
            {
                Nome = "Alexis",
                Email = "alexis@gmail.com"
            };

            var funcionarioId = funcionarioRegistrado.Id;

            _funcionarioRepositoryMock
                .Setup(x => x.GetFuncionarioByIdAsync(funcionarioId))
                .Returns(Task.FromResult(funcionarioRegistrado));

            _funcionarioRepositoryMock
                .Setup(x => x.UpdateFuncionario(funcionarioRegistrado))
                .Returns(Task.CompletedTask);

            // Act
            var resposta = await _service.UpdateAsync(updateRequest, funcionarioId);
            var esperado = mapper.Map<FuncionarioDTO>(funcionarioRegistrado);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        private IMapper CriarMapper()
        {
            var mapProfile = new MappingProfile();
            var mapConfig = new MapperConfiguration(x => x.AddProfile(mapProfile));
            return new Mapper(mapConfig);
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
