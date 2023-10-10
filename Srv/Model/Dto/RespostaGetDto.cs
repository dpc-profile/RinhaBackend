namespace Api.Model.Dto;

public class RespostaGetDto
{
    public string? Id { get; set;}
    public string? Apelido { get; set;}
    public string? Nome { get; set;}
    public string? Nascimento { get; set;}
    public IEnumerable<string>? Stack { get; set; }
}