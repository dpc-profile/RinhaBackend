using System.Net;

using Api.Controllers;
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
        var mockRepo = new Mock<IUsuarioServices>();
        mockRepo.Setup(repo => repo.VerificaApelidoCadastradoAsync(It.IsAny<string>()));
        var controller = new UsuarioController(mockRepo.Object);

        // Act
        var result = await controller.GravaUsuarioAsync(pessoaDto) as CreatedResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected: (int)HttpStatusCode.Created, actual: result.StatusCode);
        Assert.NotEmpty(result.Location);
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
        var mockRepo = new Mock<IUsuarioServices>();
        var controller = new UsuarioController(mockRepo.Object);

        // Act
        var result = await controller.GravaUsuarioAsync(pessoaDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected: (int)HttpStatusCode.BadRequest, actual: result.StatusCode);
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
        var mockRepo = new Mock<IUsuarioServices>();
        var controller = new UsuarioController(mockRepo.Object);

        // Act
        var result = await controller.GravaUsuarioAsync(pessoaDto) as UnprocessableEntityObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected: (int)HttpStatusCode.UnprocessableEntity, actual: result.StatusCode);
    }

    [Fact]
    public async void RetornarOkVazio_AoConsultarUUIDInexistente()
    {
        // Arrange
        var mockRepo = new Mock<IUsuarioServices>();
        var controller = new UsuarioController(mockRepo.Object);

        // Act
        var result = await controller.GetPorUuidAsync("uuidQueNaoExiste");

        // Assert
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        Assert.NotNull(actionResult);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(expected: (int)HttpStatusCode.OK, actual: okObjectResult.StatusCode);
        Assert.Null(okObjectResult.Value);
    }

    [Fact]
    public async void RetornarOkComUsuario_AoConsultarUUIDExistente()
    {
        string uuid = "780a47a3-9ef5-4d97-ba9e-b17bab0f2f35";
        UsuarioModel usuario = new ()
        {
            Id = uuid,
            Nome = "Teste1",
            Nascimento = "2023-11-23",
            Apelido = "XxTest360xX",
            Stack = "C#, Java, NodeJS",
            
        };
        var mockRepo = new Mock<IUsuarioServices>();
        mockRepo.Setup(x => x.ConsultaPorUUIDAsync(It.IsAny<string>())).ReturnsAsync(usuario);

        var controller = new UsuarioController(mockRepo.Object);

        var result = await controller.GetPorUuidAsync(uuid);

        // Assert
        var actionResult = Assert.IsType<ActionResult<string>>(result);
        Assert.NotNull(actionResult);

        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(expected: (int)HttpStatusCode.OK, actual: okObjectResult.StatusCode);

        var returnValue = Assert.IsType<UsuarioModel>(okObjectResult.Value);
        Assert.NotNull(returnValue);
    }
}
