using System.ComponentModel.DataAnnotations;

namespace Api.Model;

public class DBUsuarioModel
{
    [Key]
    [Required]
    public Guid Id { get; set;}
    [Required(ErrorMessage = "Apelido é obrigatório.")]
    [MaxLength(length: 32, ErrorMessage = "Apelido deve ter no maximo 32 caracteres")]
    public string? Apelido { get; set;}

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(length: 100, ErrorMessage = "Nome deve ter no maximo 100 caracteres")]
    public string? Nome { get; set;}

    [Required(ErrorMessage = "Data de Nascimento é obrigatório.")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "O formato da Data de Nascimento deve ser AAAA-MM-DD.")]
    public string? Nascimento { get; set;}

    public string? Stack { get; set;}
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        string[]? listaStack = Stack?.Split(',');

        foreach (var item in listaStack ?? Enumerable.Empty<string>())
        {
            if (item.Length > 32 || item.Length == 0)
            {
                yield return new ValidationResult("Os elemento da lista de Stack deve ser uma string com no máximo 32 caracteres.", new[] { nameof(Stack) });
                break; // Pode interromper a validação após encontrar um elemento inválido, se desejar.
            }
        }
    }

    public string? CampoSearch {get; set;}
}