using System;

using Api.Model.Data;

using Microsoft.EntityFrameworkCore;

namespace TesteAPI;

public static class ContextGenerator
{
    public static BancoContexto Generate()
    {
        DbContextOptionsBuilder<BancoContexto>? optionsBuilder = new DbContextOptionsBuilder<BancoContexto>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

        return new BancoContexto(optionsBuilder.Options);
    }
}
