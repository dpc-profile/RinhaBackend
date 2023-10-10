using System;
using Api.Model;

namespace TesteAPI;

public static class UsuarioDbGenerator
{
    public static UsuarioModel Generate_Full()
    {
        UsuarioModel usuario = new ()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = "Teste1",
            Nascimento = "2023-11-23",
            Apelido = "XxTest360xX",
            Stack = "C#, Java, NodeJS",
            
        };

        usuario.CampoSearch += $"{usuario.Nome},{usuario.Apelido},{usuario.Stack}";

        return usuario;

    }

    public static UsuarioModel Generate_StackNull()
    {
        UsuarioModel usuario = new ()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = "Teste2",
            Nascimento = "2023-11-23",
            Apelido = "XxTest360xX",
        };

        usuario.CampoSearch += $"{usuario.Nome},{usuario.Apelido},{usuario.Stack}";

        return usuario;

    }
}
