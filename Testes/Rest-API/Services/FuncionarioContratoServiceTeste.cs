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
    public class FuncionarioContratoServiceTeste
    {
        private Mock<IFuncionarioContratoRepository> _repository = new Mock<IFuncionarioContratoRepository>();
        private FuncionarioContratoService? _service;

        [Fact]
        public void Instanciavel()
        {
            _service = new FuncionarioContratoService(_repository.Object, CriarMapper());

            Assert.NotNull(_service);
        }

        [Fact]
        public async void GetContratosCLTAsync_Retorna_Lista_De_ContratoCLTDTO()
        {
            // Arrange
            var funcionarioId = Guid.NewGuid();

            var mapper = CriarMapper();
            _service = new FuncionarioContratoService(_repository.Object, mapper);

            var contratos = ContratosCLTFactory();

            _repository
                .Setup(x => x.GetContratosCLTAsync(funcionarioId))
                .Returns(Task.FromResult(contratos));

            // Act
            var resposta = await _service.GetContratosCLTAsync(funcionarioId);
            var esperado = mapper.Map<List<ContratoCLTDTO>>(contratos);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        [Fact]
        public async void GetContratosPJAsync_Retorna_Lista_De_ContratoPJDTO()
        {
            // Arrange
            var funcionarioId = Guid.NewGuid();

            var mapper = CriarMapper();
            _service = new FuncionarioContratoService(_repository.Object, mapper);

            var contratos = ContratosPJFactory();

            _repository
                .Setup(x => x.GetContratosPJAsync(funcionarioId))
                .Returns(Task.FromResult(contratos));

            // Act
            var resposta = await _service.GetContratosPJAsync(funcionarioId);
            var esperado = mapper.Map<List<ContratoPJDTO>>(contratos);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(esperado), JsonSerializer.Serialize(resposta));
        }

        private IMapper CriarMapper()
        {
            var mapProfile = new MappingProfile();
            var mapConfig = new MapperConfiguration(x => x.AddProfile(mapProfile));
            return new Mapper(mapConfig);
        }

        private IEnumerable<ContratoCLT> ContratosCLTFactory()
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

        private IEnumerable<ContratoPJ> ContratosPJFactory()
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