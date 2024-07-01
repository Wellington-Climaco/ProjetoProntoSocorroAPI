using Application.DTOs;
using Application.DTOs.PacienteDTOs;
using Application.Interface;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Interface;
using Moq;
using Xunit;

namespace ProntoSocorroTests;

public class FuncionarioServiceTests
{

    [Fact]
    public async Task DadoUmFuncionarioQueNaoExisteDeveRetornarMensagem()
    {

        //arrange
        var funcionarioService = new Mock<IFuncionarioService>();

        funcionarioService.Setup(x => x.ChamarProximo(It.IsAny<Guid>())).ReturnsAsync(new ResultDTO<ListarPacienteDTO>("Funcionário não encontrado"));

        var expected = "Funcionário não encontrado";
        //act
        var result = await funcionarioService.Object.ChamarProximo(Guid.NewGuid());

        //assert
        Assert.Equal(expected, result.Errors.First());
    }

    [Fact]
    public async Task DadoUmFuncionarioEmAtendimentoDeveRetornarMensagem()
    {
        //arrange
        var fixture = new Fixture();
        var funcionarioRepository = new Mock<IFuncionarioRepository>();
        var tokenService = new Mock<ITokenService>();
        var pacienteRepository = new Mock<IPacienteRepository>();
        var funcionarioService = new FuncionarioService(funcionarioRepository.Object, pacienteRepository.Object, tokenService.Object);

        Funcionario funcionario = fixture.Create<Funcionario>();
        funcionario.MudarAtendimento();

        funcionarioRepository.Setup(x => x.GetFuncionario(It.IsAny<Guid>())).ReturnsAsync(funcionario);

        var expected = "Dispense o paciente atual para chamar o próximo!!";

        //act
        var result = await funcionarioService.ChamarProximo(It.IsAny<Guid>());

        //assert
        Assert.Equal(expected, result.Errors.First());
    }

    [Fact]
    public async Task QuandoNaoHouverPacienteNaFilaRetornarMensagem()
    {
        //arrange

        var fixture = new Fixture();
        var funcionarioRepository = new Mock<IFuncionarioRepository>();
        var tokenService = new Mock<ITokenService>();
        var pacienteRepository = new Mock<IPacienteRepository>();
        var funcionarioService = new FuncionarioService(funcionarioRepository.Object, pacienteRepository.Object, tokenService.Object);

        var expected = "fila vazia";

        Funcionario funcionario = fixture.Create<Funcionario>();

        funcionarioRepository.Setup(x => x.GetFuncionario(It.IsAny<Guid>())).ReturnsAsync(funcionario);

        funcionarioRepository.Setup(x => x.ExistemPacientesNaoPreferenciais(funcionario)).ReturnsAsync(false);
        funcionarioRepository.Setup(x => x.ExistemPacientesPreferenciais(funcionario)).ReturnsAsync(false);

        //act
        var result = await funcionarioService.ChamarProximo(It.IsAny<Guid>());

        //assert
        Assert.Equal(expected, result.Errors.First());
    }

    [Fact]
    public async Task QuandoHouverSomentePacientePreferencialDeveSerOproximo()
    {
        //arrange
        var fixture = new Fixture();
        var funcionarioRepository = new Mock<IFuncionarioRepository>();
        var tokenService = new Mock<ITokenService>();
        var pacienteRepository = new Mock<IPacienteRepository>();
        var funcionarioService = new FuncionarioService(funcionarioRepository.Object, pacienteRepository.Object, tokenService.Object);
        Funcionario funcionario = fixture.Create<Funcionario>();
        Paciente paciente = fixture.Create<Paciente>();
        ListarPacienteDTO pacienteDTO = fixture.Create<ListarPacienteDTO>();

        funcionarioRepository.Setup(x => x.GetFuncionario(It.IsAny<Guid>())).ReturnsAsync(funcionario);
        funcionarioRepository.Setup(x => x.ChamarProximoPreferencial(It.IsAny<Funcionario>())).ReturnsAsync(paciente);
        funcionarioRepository.Setup(x => x.ExistemPacientesPreferenciais(It.IsAny<Funcionario>())).ReturnsAsync(true);
        //act
        var result = await funcionarioService.ChamarProximo(It.IsAny<Guid>());

        //assert
        Assert.True(result.IsValid);
    }






}
