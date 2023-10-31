using System.Linq;
using System.Net;

using Api.Controllers;
using Api.Repository;
using Api.Services;

using Microsoft.AspNetCore.Mvc;

using Moq;

namespace TesteAPI;

public class ValidaRetornosAPITest
{
    [Fact]
    public async void RetornaCreated_AoGravarUsuario()
    {
        // Arrange
        PessoaDto? pessoaDto = new()
        {
            Apelido = "Meu Apelido",
            Nome = "Meu Nome",
            Nascimento = "2000-01-01",
            Stack = new[] { "Item1", "Item2" }
        };
        var mockServices = new Mock<IUsuarioServices>();
        mockServices.Setup(repo => repo.VerificaApelidoCadastradoAsync(It.IsAny<string>()));
        var controller = new UsuarioController(mockServices.Object);

        // Act
        var resultado = await controller.GravaUsuarioAsync(pessoaDto) as CreatedResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(expected: (int)HttpStatusCode.Created, actual: resultado.StatusCode);
        Assert.NotEmpty(resultado.Location);
    }

    [Fact]
    public async void RetornaBadRequest_AoTentarGravarUsuarioComStackInvalido()
    {
        // Arrange
        PessoaDto? pessoaDto = new()
        {
            Apelido = "Meu Apelido",
            Nome = "Meu Nome",
            Nascimento = "2000-01-01",
            Stack = new[] { "Item valido", "Item da Stack com mais de 32 caracteres" }
        };
        var mockServices = new Mock<IUsuarioServices>();
        var controller = new UsuarioController(mockServices.Object);

        // Act
        var resultado = await controller.GravaUsuarioAsync(pessoaDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(expected: (int)HttpStatusCode.BadRequest, actual: resultado.StatusCode);
    }

    [Fact]
    public async void RetornaUnprocessableEntity_AoTentarGravarUsuarioComApelidoNulo()
    {
        // Arrange
        PessoaDto? pessoaDto = new()
        {
            Apelido = null,
            Nome = "Meu Nome",
            Nascimento = "2000-01-01",
            Stack = new[] { "Item 1", "Item 2" }
        };
        var mockServices = new Mock<IUsuarioServices>();
        var controller = new UsuarioController(mockServices.Object);

        // Act
        var resultado = await controller.GravaUsuarioAsync(pessoaDto) as UnprocessableEntityObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(expected: (int)HttpStatusCode.UnprocessableEntity, actual: resultado.StatusCode);
    }

    [Fact]
    public async void RetornarOkVazio_AoConsultarUUIDInexistente()
    {
        // Arrange
        var mockServices = new Mock<IUsuarioServices>();
        var controller = new UsuarioController(mockServices.Object);

        // Act
        var resultado = await controller.GetPorUuidAsync("uuidQueNaoExiste");

        // Assert
        var actionResult = Assert.IsType<ActionResult<string>>(resultado);
        Assert.NotNull(actionResult);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(expected: (int)HttpStatusCode.OK, actual: okObjectResult.StatusCode);
        Assert.Null(okObjectResult.Value);
    }

    [Fact]
    public async void RetornarOkComUsuario_AoConsultarUUIDExistente()
    {
        // Arrange
        string uuid = "780a47a3-9ef5-4d97-ba9e-b17bab0f2f35";
        UsuarioModel usuario = new ()
        {
            Id = uuid,
            Nome = "Teste1",
            Nascimento = "2023-11-23",
            Apelido = "XxTest360xX",
            Stack = "C#, Java, NodeJS",
            
        };

        var mockServices = new Mock<IUsuarioServices>();
        mockServices.Setup(x => x.ConsultaPorUUIDAsync(It.IsAny<string>()))
                    .ReturnsAsync(usuario);
        var controller = new UsuarioController(mockServices.Object);

        // Act
        var result = await controller.GetPorUuidAsync(uuid);

        // Assert
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        Assert.NotNull(actionResult);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(expected: (int)HttpStatusCode.OK, actual: okObjectResult.StatusCode);

        var returnValue = Assert.IsType<UsuarioModel>(okObjectResult.Value);
        Assert.NotNull(returnValue);
    }

    [Fact]
    public async Task RetornaListaVazia_AoNaoTerNenhumUsuarioComOTermo()
    {
        // Arrange
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(x => x.ConsultarUsuarioPorTermoAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<UsuarioModel>());
        var service = new UsuarioServices(mockRepo.Object);

        // Act
        var resultado = await service.ConsultaPorTermoAsync("Termo");

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }

    [Fact]
    public async Task RetornaListaComUmUsuario_AoPesquisarTermo()
    {
        // Arrange
        List<UsuarioModel> listaUsuarios = new();

        UsuarioModel usuario1 = new ()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = "Teste1",
            Nascimento = "2023-11-23",
            Apelido = "XxTest360xX",
            Stack = "C#, Java, NodeJS",
            
        };

        listaUsuarios.Add(usuario1);
        
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(x => x.ConsultarUsuarioPorTermoAsync(It.IsAny<string>()))
                .ReturnsAsync(listaUsuarios);
        var service = new UsuarioServices(mockRepo.Object);

        // Act
        var resultado = await service.ConsultaPorTermoAsync(It.IsAny<string>());

        // Assert
        Assert.NotNull(resultado);
        Assert.NotEmpty(resultado);
        Assert.Single(resultado);
    }

    [Fact]
    public async Task RetornaListaComDoisUsuario_AoPesquisarTermo()
    {
        // Arrange
        List<UsuarioModel> listaUsuarios = new();

        UsuarioModel usuario1 = new ()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = "Teste1",
            Nascimento = "2023-11-23",
            Apelido = "XxTest360xX",
            Stack = "C#, Java, NodeJS, Python"
        };

        UsuarioModel usuario2 = new ()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = "Teste2",
            Nascimento = "2023-11-23",
            Apelido = "Testtt",
            Stack = "Python"
        };

        listaUsuarios.Add(usuario1);
        listaUsuarios.Add(usuario2);
        
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(x => x.ConsultarUsuarioPorTermoAsync("Python"))
                .ReturnsAsync(listaUsuarios);
        var service = new UsuarioServices(mockRepo.Object);

        // Act
        var resultado = await service.ConsultaPorTermoAsync("Python");

        // Assert
        Assert.NotNull(resultado);
        Assert.True(resultado.Any(), "A lista precisa ter conteudo");
        Assert.Equal(expected: 2, actual: resultado.Count());
    }
}
