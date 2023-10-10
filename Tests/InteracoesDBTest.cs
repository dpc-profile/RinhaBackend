using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Model;
using Api.Model.Data;
using Api.Repository;

using Xunit;

namespace TesteAPI;

public class InteracoesDBTest
{
    private readonly BancoContexto _bancoContexto;

    public InteracoesDBTest()
    {
        _bancoContexto = ContextGenerator.Generate();
    }

    [Fact]
    public async Task AposGravaUsuario_DeveTerUmUsuarioGravado()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        UsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();

        await repository.GravarUsuarioAsync(usuario);

        Assert.Single(_bancoContexto.Usuarios);
    }

    [Fact]
    public async Task AposConsultaUsuarioPorUUID_NaoDeveSerNulo()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        UsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        UsuarioModel? resposta = await repository.ConsultaUsuarioPorUUIDAsync(usuario.Id!);

        Assert.NotNull(resposta);
    }

    [Theory]
    [InlineData("java")]
    [InlineData("teste")]
    [InlineData("360")]
    public async Task AposConsultaUsuarioPorTermo_NaoDeveSerListaVazia(string termo)
    {
        UsuarioRepository? repository = new(_bancoContexto);
        UsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        List<UsuarioModel>? resposta = await repository.ConsultarUsuarioPorTermoAsync(termo);

        Assert.NotEmpty(resposta);
    }

    [Theory]
    [InlineData("David")]
    [InlineData("Armando")]
    [InlineData("Python")]
    public async Task AposConsultaUsuarioPorTermo_DeveSerListaVazia(string termo)
    {
        UsuarioRepository? repository = new(_bancoContexto);
        UsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        List<UsuarioModel>? resposta = await repository.ConsultarUsuarioPorTermoAsync(termo);

        Assert.Empty(resposta);
    }

    [Fact]
    public async Task AposConsultaUsuarioPorTermo_QuantidadeDeveSerZero()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        UsuarioModel? usuario = UsuarioDbGenerator.Generate_StackNull();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        string termo = "Java";

        List<UsuarioModel>? resposta = await repository.ConsultarUsuarioPorTermoAsync(termo);

        int quantUsuarios = resposta.Count;

        Assert.Equal(expected: 0, actual: quantUsuarios);
    }

    [Fact]
    public async Task AposConsultaUsuarioPorApelido_DeveRetornarUmUsuario()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        UsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        var resposta = await repository.ConsultarUsuarioPorApelidoAsync(usuario.Apelido);

        Assert.NotNull(resposta);
    }

    [Fact]
    public async Task AposConsultaUsuarioPorApelido_DeveRetornarNulo()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        UsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        var resposta = await repository.ConsultarUsuarioPorApelidoAsync("Zezinho");

        Assert.Null(resposta);
    }

    [Fact]
    public async Task AposContaUsuarios_DeveSerUm()
    {
        UsuarioRepository? repository = new(_bancoContexto);

        UsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        int resposta = await repository.CountUsuariosCadastradosAsync();

        Assert.Equal(expected: 1, actual: resposta);
    }
}