using System.ComponentModel.DataAnnotations;

namespace TesteAPI;

public class ValidarRegrasPessoaDtoTest
{
    [Fact]
    public void DeveFalharValidacao_QuandoPropriedadesSaoInvalidas()
    {
        // Arrange
        PessoaDto? pessoaDto = new ()
        {
            Apelido = "Apelido muito longo que excede o limite de 32 caracteres",
            Nome = "Meu Nome",
            Nascimento = "01-01-2000", // Formato de data inválido
            Stack = new[] { "Item1", "Item2" }
        };

        // Act
        ValidationContext? context = new(pessoaDto);
        List<ValidationResult>? resultados = new();
        bool valido = Validator.TryValidateObject(instance: pessoaDto, validationContext: context, validationResults: resultados, validateAllProperties: true);

        // Assert
        Assert.False(valido, "pessoaDTO não pode ser valido.");
        Assert.Equal(2, resultados.Count);
        Assert.Equal("Apelido deve ter no maximo 32 caracteres", resultados[0].ErrorMessage);
        Assert.Equal("O formato da Data de Nascimento deve ser AAAA-MM-DD.", resultados[1].ErrorMessage);
    }

    [Fact]
    public void LancarExcecao_NomeForNulo()
    {
        // Arrange
        PessoaDto? pessoaDto = new ()
        {
            Apelido = "Meu Apelido",
            Nome = null,
            Nascimento = "2000-01-01",
            Stack = new[] { "Item1", "Item2" }
        };

        // Act & Assert
        var mensagem = Assert.Throws<UnprocessableEntityException>(() => pessoaDto.Validate());

        Assert.Equal("O Nome está vazio ou nulo.", mensagem.Message);
    }

    [Fact]
    public void DeveLancarExcecao_QuandoApelidoForNulo()
    {
        // Arrange
        PessoaDto? pessoaDto = new ()
        {
            Apelido = null,
            Nome = "Meu Nome",
            Nascimento = "2000-01-01",
            Stack = new[] { "Item1", "Item2" }
        };

        // Act & Assert
        var mensagem = Assert.Throws<UnprocessableEntityException>(() => pessoaDto.Validate());

        Assert.Equal("O Apelido está vazio ou nulo.", mensagem.Message);
    }

    [Fact]
    public void DeveLancarExcecao_QuandoItemDaStackMaiorQue32()
    {
        // Arrange
        PessoaDto? pessoaDto = new ()
        {
            Apelido = "Meu Apelido",
            Nome = "Meu Nome",
            Nascimento = "2000-01-01",
            Stack = new[] { "Item valido" ,"Item da Stack com mais de 32 caracteres" }
        };

        // Act & Assert
        var mensagem = Assert.Throws<BadRequestException>(() => pessoaDto.Validate());

        Assert.Equal("Os elemento da lista de Stack deve ser uma string com no máximo 32 caracteres.", mensagem.Message);
    }

    [Fact]
    public void DevePopularUsuarioModelCorretamente_ComStackSendoNulo()
    {
        
        // Arrange
        PessoaDto? pessoaDto = new ()
        {
            Apelido = "Meu Apelido",
            Nome = "Meu Nome",
            Nascimento = "2000-01-01"
        };

        // Act
        var usuarioModel = pessoaDto.PopulaUsuarioModel();
        pessoaDto.Validate();

        // Assert
        Assert.NotNull(usuarioModel);
    }

    [Fact]
    public void DevePopularUsuarioModelCorretamente()
    {
        
        // Arrange
        PessoaDto? pessoaDto = new ()
        {
            Apelido = "Meu Apelido",
            Nome = "Meu Nome",
            Nascimento = "2000-01-01",
            Stack = new[] { "Item1", "Item2" }
        };

        // Act
        var usuarioModel = pessoaDto.PopulaUsuarioModel();

        // Assert
        Assert.NotNull(usuarioModel);
        Assert.Equal("Meu Apelido", usuarioModel.Apelido);
        Assert.Equal("Meu Nome", usuarioModel.Nome);
        Assert.Equal("2000-01-01", usuarioModel.Nascimento);
        Assert.Equal("Item1, Item2", usuarioModel.Stack);
    }

}