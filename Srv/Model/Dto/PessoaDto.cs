namespace Api.Model.Dto;

public class PessoaDto
{
    [StringLength(32, ErrorMessage = "Apelido deve ter no maximo 32 caracteres")]
    public string? Apelido { get; set;}

    [StringLength(100, ErrorMessage = "Nome deve ter no maximo 100 caracteres")]
    public string? Nome { get; set;}

    [Required(ErrorMessage = "Data de Nascimento é obrigatório.")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "O formato da Data de Nascimento deve ser AAAA-MM-DD.")]
    public string? Nascimento { get; set;}

    public IEnumerable<string>? Stack { get; set; }
    
    public void Validate()
    {
        if (string.IsNullOrEmpty(Nome))
            throw new UnprocessableEntityException("O Nome está vazio ou nulo.");
        
        if (string.IsNullOrEmpty(Apelido))
            throw new UnprocessableEntityException("O Apelido está vazio ou nulo.");
        
        foreach (string item in Stack ?? Enumerable.Empty<string>())
        {
            if (item.Length > 32 || item.Length == 0)
            {
                throw new BadRequestException("Os elemento da lista de Stack deve ser uma string com no máximo 32 caracteres.");
            }
        }
    }
    public UsuarioModel PopulaUsuarioModel()
    {
        UsuarioModel usuario = new()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = Nome,
            Nascimento = Nascimento,
            Apelido = Apelido
        };

        if (Stack != null)
            usuario.Stack = string.Join( ", ", Stack);

        usuario.CampoSearch += $"{usuario.Nome},{usuario.Apelido},{usuario.Stack}";
        
        return usuario;
    }
}