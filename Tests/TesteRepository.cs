using System;

using Api.Model;
using Api.Model.Data;
using Api.Model.Dto;
using Api.Repository;
using Api.Services;

using Microsoft.EntityFrameworkCore;

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
    public void Teste_Repo_Gravar()
    {
        UsuarioRepository? repository = new UsuarioRepository(_bancoContexto);
 
        DBUsuarioModel usuario = new ()
        {
            Nome = "Test1",
            Nascimento = "2023-11-23",
            Apelido = "Testenildo",
            Stack = "C#, Java, NodeJS",
            
        };

        usuario.CampoSearch += $"{usuario.Nome},{usuario.Apelido},{usuario.Stack}";

        _ = repository.GravarUsuarioAsync(usuario);

        Assert.Single(_bancoContexto.Usuarios);
    }
}