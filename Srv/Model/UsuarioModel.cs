using System.ComponentModel.DataAnnotations;

namespace Api.Model;

public class UsuarioModel
{
    [Key]
    [Required]
    public string? Id { get; set;}
    [Required(ErrorMessage = "Apelido é obrigatório.")]
    [MaxLength(length: 32, ErrorMessage = "Apelido deve ter no maximo 32 caracteres")]
    public string? Apelido { get; set;}

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MaxLength(length: 100, ErrorMessage = "Nome deve ter no maximo 100 caracteres")]
    public string? Nome { get; set;}

    [Required(ErrorMessage = "Data de Nascimento é obrigatório.")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "O formato da Data de Nascimento deve ser AAAA-MM-DD.")]
    [MaxLength(10)]
    public string? Nascimento { get; set;}
    public string? Stack { get; set;}
    public string? CampoSearch {get; set;}
}