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
using Restful_API.Services.Interfaces;
using Restful_API.Services.Services;
using Xunit;

namespace Testes.Rest_API.Services
{
    public class ContratoPJServiceTeste
    {
        private Mock<IContratoPJRepository> _repository = new Mock<IContratoPJRepository>();
        private Mock<IFuncionarioService> _funcionarioService = new Mock<IFuncionarioService>();
        private ContratoPJService? _service;

        [Fact]
        public void Instanciavel()
        {
            _service = new ContratoPJService(_repository.Object, _funcionarioService.Object, CriarMapper());

            Assert.NotNull(_service);
        }

        [Fact]
        public async void GetAllAsync_Retorna_Lista_De_ContratoPJDTO()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoPJService(_repository.Object, _funcionarioService.Object, mapper);

            var contratos = ContratosPJFactory();

            _repository
                .Setup(x => x.GetContratosAsync())
                .Returns(Task.FromResult(contratos));

            // Act
            var resposta = await _service.GetAllAsync();
            var esperado = mapper.Map<List<ContratoPJDTO>>(contratos);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        [Fact]
        public async void GetByIdAsync_Retorna_ContratoPJDTO()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoPJService(_repository.Object, _funcionarioService.Object, mapper);

            var contrato = new ContratoPJ(
                    DateTime.UtcNow,
                    null,
                    3500,
                    "Desenvolvedor Frontend",
                    Guid.NewGuid()
                );

            _repository
                .Setup(x => x.GetContratoByIdAsync(contrato.Id))
                .Returns(Task.FromResult(contrato));

            // Act
            var resposta = await _service.GetByIdAsync(contrato.Id);
            var esperado = mapper.Map<ContratoPJDTO>(contrato);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        [Fact]
        public async void InsertAsync_Retorna_Null_Se_Funcionario_Nao_Existe()
        {
            // Arrange
            _service = new ContratoPJService(_repository.Object, _funcionarioService.Object, CriarMapper());

            var funcionarioId = Guid.NewGuid();

            var createRequest = new CreateContratoPJRequest()
            {
                FuncionarioId = funcionarioId
            };

            _funcionarioService
                .Setup(x => x.GetByIdAsync(funcionarioId))
                .Returns(Task.FromResult<FuncionarioDTO>(null));

            // Act
            var resposta = await _service.InsertAsync(createRequest);

            // Assert
            Assert.Null(resposta);
        }

        [Fact]
        public async void InsertAsync_Retorna_ContratoPJDTO_Se_Funcionario_Existe()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoPJService(_repository.Object, _funcionarioService.Object, CriarMapper());

            var funcionarioId = Guid.NewGuid();

            var createRequest = new CreateContratoPJRequest()
            {
                FuncionarioId = funcionarioId,
                Cargo = "Desenvolvedor",
                SalarioBruto = 4000
            };

            _funcionarioService
                .Setup(x => x.GetByIdAsync(funcionarioId))
                .Returns(Task.FromResult(new FuncionarioDTO()));

            _repository
                .Setup(x => x.InsertContratoAsync(It.IsAny<ContratoPJ>()))
                .Returns(Task.CompletedTask);

            // Act
            var resposta = await _service.InsertAsync(createRequest);

            // Assert
            Assert.NotEqual(Guid.Empty, resposta.Id);
            Assert.Equal(createRequest.Cargo, resposta.Cargo);
            Assert.Equal(createRequest.SalarioBruto, resposta.SalarioBruto);
            Assert.NotEqual(DateTime.MinValue, resposta.Inicio);
        }

        [Fact]
        public async void UpdateAsync_Retorna_ContratoPJDTO_Atualizado()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoPJService(_repository.Object, _funcionarioService.Object, CriarMapper());

            var contratoRegistrado = new ContratoPJ(
                    DateTime.UtcNow,
                    null,
                    3500,
                    "Desenvolvedor Frontend",
                    Guid.NewGuid()
                );

            var updateRequest = new UpdateContratoPJRequest()
            {
                Cargo = "Designer",
                SalarioBruto = 5000,
                Termino = null
            };

            var contratoId = contratoRegistrado.Id;

            _repository
                .Setup(x => x.GetContratoByIdAsync(contratoId))
                .Returns(Task.FromResult(contratoRegistrado));

            _repository
                .Setup(x => x.UpdateContrato(contratoRegistrado))
                .Returns(Task.CompletedTask);

            // Act
            var resposta = await _service.UpdateAsync(updateRequest, contratoId);
            var esperado = mapper.Map<ContratoPJDTO>(contratoRegistrado);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        private IMapper CriarMapper()
        {
            var mapProfile = new MappingProfile();
            var mapConfig = new MapperConfiguration(x => x.AddProfile(mapProfile));
            return new Mapper(mapConfig);
        }

        public IEnumerable<ContratoPJ> ContratosPJFactory()
        {
            return new List<ContratoPJ>() {
                new ContratoPJ(
                    DateTime.UtcNow,
                    null,
                    3500,
                    "Desenvolvedor Frontend",
                    Guid.NewGuid()
                ),
                new ContratoPJ(
                    DateTime.UtcNow,
                    null,
                    4000,
                    "Designer",
                    Guid.NewGuid()
                ),
                new ContratoPJ(
                    DateTime.UtcNow,
                    null,
                    5000,
                    "Desenvolvedor Fullstack",
                    Guid.NewGuid()
                )
            };
        }
    }
}