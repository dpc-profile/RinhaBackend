# Cria as migrations
$ dotnet-ef migrations add MyMigration

# Aplica a migration no database
$ dotnet-ef database update

# Gerar relatorio que alimentara o reporgenerator
$ dotnet test ControleDeContatos.Tests \
--settings ControleDeContatos.Tests/coverlet.runsettings.xml

# Gerar relatório para HTML
$ reportgenerator \
-reports:"ControleDeContatos.Tests/TestResults/**/coverage.opencover.xml" \
-targetdir:"coveragereport" \
-reporttypes:Html

#Arquivo index.html estará na pasta 'coveragereport'

#Adiciona todos os csproj a solução ControleDeContatos.sln
$ dotnet sln api.sln add **/*.csproj --in-root

# Rodar o teste de mutação
$ dotnet Stryker