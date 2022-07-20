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
    public class ContratoCLTServiceTeste
    {
        private Mock<IContratoCLTRepository> _repository = new Mock<IContratoCLTRepository>();
        private Mock<IFuncionarioService> _funcionarioService = new Mock<IFuncionarioService>();
        private ContratoCLTService? _service;

        [Fact]
        public void Instanciavel()
        {
            _service = new ContratoCLTService(_repository.Object, _funcionarioService.Object, CriarMapper());

            Assert.NotNull(_service);
        }

        [Fact]
        public async void GetAllAsync_Retorna_Lista_De_ContratoCLTDTO()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoCLTService(_repository.Object, _funcionarioService.Object, mapper);

            var contratos = ContratosCLTFactory();

            _repository
                .Setup(x => x.GetContratosAsync())
                .Returns(Task.FromResult(contratos));

            // Act
            var resposta = await _service.GetAllAsync();
            var esperado = mapper.Map<List<ContratoCLTDTO>>(contratos);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        [Fact]
        public async void GetByIdAsync_Retorna_ContratoCLTDTO()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoCLTService(_repository.Object, _funcionarioService.Object, mapper);

            var contrato = new ContratoCLT(
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
            var esperado = mapper.Map<ContratoCLTDTO>(contrato);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        [Fact]
        public async void InsertAsync_Retorna_Null_Se_Funcionario_Nao_Existe()
        {
            // Arrange
            _service = new ContratoCLTService(_repository.Object, _funcionarioService.Object, CriarMapper());

            var funcionarioId = Guid.NewGuid();

            var createRequest = new CreateContratoCLTRequest()
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
        public async void InsertAsync_Retorna_ContratoCLTDTO_Se_Funcionario_Existe()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoCLTService(_repository.Object, _funcionarioService.Object, CriarMapper());

            var funcionarioId = Guid.NewGuid();

            var createRequest = new CreateContratoCLTRequest()
            {
                FuncionarioId = funcionarioId,
                Cargo = "Desenvolvedor",
                SalarioBruto = 4000
            };

            _funcionarioService
                .Setup(x => x.GetByIdAsync(funcionarioId))
                .Returns(Task.FromResult(new FuncionarioDTO()));

            _repository
                .Setup(x => x.InsertContratoAsync(It.IsAny<ContratoCLT>()))
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
        public async void UpdateAsync_Retorna_ContratoCLTDTO_Atualizado()
        {
            // Arrange
            var mapper = CriarMapper();
            _service = new ContratoCLTService(_repository.Object, _funcionarioService.Object, CriarMapper());

            var contratoRegistrado = new ContratoCLT(
                    DateTime.UtcNow,
                    null,
                    3500,
                    "Desenvolvedor Frontend",
                    Guid.NewGuid()
                );

            var updateRequest = new UpdateContratoCLTRequest()
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
            var esperado = mapper.Map<ContratoCLTDTO>(contratoRegistrado);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        private IMapper CriarMapper()
        {
            var mapProfile = new MappingProfile();
            var mapConfig = new MapperConfiguration(x => x.AddProfile(mapProfile));
            return new Mapper(mapConfig);
        }

        public IEnumerable<ContratoCLT> ContratosCLTFactory()
        {
            return new List<ContratoCLT>() {
                new ContratoCLT(
                    DateTime.UtcNow,
                    null,
                    3500,
                    "Desenvolvedor Frontend",
                    Guid.NewGuid()
                ),
                new ContratoCLT(
                    DateTime.UtcNow,
                    null,
                    4000,
                    "Designer",
                    Guid.NewGuid()
                ),
                new ContratoCLT(
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