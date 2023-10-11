using System.Net;

using Api.Controllers;
using Api.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

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
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        Assert.NotEmpty(result.Location);
    }

    [Fact]
    public async void RetornaBadRequest_AoTentarGravarUsuario()
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
        Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async void RetornaUnprocessableEntity_AoTentarGravarUsuario()
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
        Assert.Equal((int)HttpStatusCode.UnprocessableEntity, result.StatusCode);
    }
}
