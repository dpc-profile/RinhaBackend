using System.ComponentModel.DataAnnotations;

namespace TesteAPI;

public class ValidarRegrasTest
{
    [Fact]
    public void PessoaDto_Deve_FalharValidacaoQuandoPropriedadesSaoInvalidas()
    {
        // Arrange
        var pessoaDto = new PessoaDto
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
        Assert.False(valido);
        Assert.Equal(2, resultados.Count);
        Assert.Equal("Apelido deve ter no maximo 32 caracteres", resultados[0].ErrorMessage);
        Assert.Equal("O formato da Data de Nascimento deve ser AAAA-MM-DD.", resultados[1].ErrorMessage);
    }

    [Fact]
    public void PessoaDto_Deve_LancarExcecaoQuandoChamarMetodoValidateComPropriedadesInvalidas()
    {
        // Arrange
        var pessoaDto = new PessoaDto
        {
            Apelido = "Apelido muito longo que excede o limite de 32 caracteres",
            Nome = null, // Nome é obrigatório
            Nascimento = "01-01-2000", // Formato de data inválido
            Stack = new[] { "Item com mais de 32 caracteres" }
        };

        // Act & Assert
        Assert.Throws<UnprocessableEntityException>(() => pessoaDto.Validate());
    }

    [Fact]
    public void PessoaDto_Deve_PopularUsuarioModelCorretamente()
    {
        // Arrange
        var pessoaDto = new PessoaDto
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