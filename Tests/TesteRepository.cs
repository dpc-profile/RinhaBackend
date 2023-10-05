using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Api.Model;
using Api.Model.Data;
using Api.Model.Dto;
using Api.Repository;

using Xunit;

namespace TesteAPI;

public class TesteRepository
{
    private readonly BancoContexto _bancoContexto;

    public TesteRepository()
    {
        _bancoContexto = ContextGenerator.Generate();
    }

    [Fact]
    public async Task Testar_Gravar()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        DBUsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();

        await repository.GravarUsuarioAsync(usuario);

        Assert.Single(_bancoContexto.Usuarios);
    }

    [Fact]
    public async Task Testar_ConsultarPorUuid()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        DBUsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        DBUsuarioModel? resposta = await repository.ConsultaUsuarioPorUUIDAsync(usuario.Id!);

        Assert.NotNull(resposta);
    }

    [Theory]
    [InlineData("Java")]
    [InlineData("Teste")]
    [InlineData("360")]
    public async Task Testar_ConsultarPorTermo(string termo)
    {
        UsuarioRepository? repository = new(_bancoContexto);
        DBUsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        List<DBUsuarioModel>? resposta = await repository.ConsultarUsuarioPorTermoAsync(termo);

        Assert.NotEmpty(resposta);
    }

    [Theory]
    [InlineData("David")]
    [InlineData("Armando")]
    [InlineData("Python")]
    public async Task Testar_ConsultarPorTermo_Empty(string termo)
    {
        UsuarioRepository? repository = new(_bancoContexto);
        DBUsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        List<DBUsuarioModel>? resposta = await repository.ConsultarUsuarioPorTermoAsync(termo);

        Assert.Empty(resposta);
    }

    [Fact]
    public async Task Testar_ConsultarPorTermo_StackNull()
    {
        UsuarioRepository? repository = new(_bancoContexto);
        DBUsuarioModel? usuario = UsuarioDbGenerator.Generate_StackNull();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        string termo = "Java";

        List<DBUsuarioModel>? resposta = await repository.ConsultarUsuarioPorTermoAsync(termo);

        int quantUsuarios = resposta.Count;

        Assert.Equal(expected: 0, actual: quantUsuarios);
    }

    [Fact]
    public async Task Testar_CountUsuarios()
    {
        UsuarioRepository? repository = new(_bancoContexto);

        DBUsuarioModel? usuario = UsuarioDbGenerator.Generate_Full();
        
        _bancoContexto.Usuarios.Add(usuario);
        _bancoContexto.SaveChanges();

        int resposta = await repository.CountUsuariosCadastradosAsync();

        Assert.Equal(expected: 1, actual: resposta);
    }
}