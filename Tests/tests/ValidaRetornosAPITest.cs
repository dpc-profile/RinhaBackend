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

        // Verifica se o status é Created
        Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);

        // Verifique se o "Location" contém uma URL
        Assert.NotEmpty(result.Location);
    }

    [Fact]
    public void RetornaBadRequest_AoTentarGravarUsuario()
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

        // Instanciar o controller usando o obj do mockRepo
        var controller = new UsuarioController(mockRepo.Object);

        // Act
        var result = controller.GravaUsuarioAsync(pessoaDto);

        // Assert
         // Confere o se o tipo é IActionResult
        var actionResult = Assert.IsAssignableFrom<Task<IActionResult>>(result);

        // Confere se o actionResult é uma BadRequest
        Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public void RetornaUnprocessableEntity_AoTentarGravarUsuario()
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

        // Instanciar o controller usando o obj do mockRepo
        var controller = new UsuarioController(mockRepo.Object);

        // Act
        var result = controller.GravaUsuarioAsync(pessoaDto);

        // Assert
         // Confere o se o tipo é IActionResult
        var actionResult = Assert.IsAssignableFrom<Task<IActionResult>>(result);

        // Confere se o actionResult é uma BadRequest
        Assert.IsAssignableFrom<UnprocessableEntityObjectResult>(actionResult.Result);
    }
}
