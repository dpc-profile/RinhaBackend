# Notas para consulta
## Migrations 
```sh
# Cria as migrations
$ dotnet-ef migrations add MyMigration

# Aplica a migration no database
$ dotnet-ef database update

# Desligar os containers e deletar suas imagens e containers
$ docker-compose down --rmi all
```

## Docker Compose (Ainda não implementado)
```sh
# Subir todos os container sem prender o terminal
docker-compose up -d

# Para todos os containers do docker compose
docker-compose down

# Para os containers e deletar as imagens e containers
$ docker-compose down --rmi all
```

## Bonus: Coverage
```sh
# Gerar relatorio que alimentara o reporgenerator
$ dotnet test Tests --settings Tests/coverlet.runsettings.xml

# Gerar relatório para HTML
$ reportgenerator \
-reports:"Tests/TestResults/**/coverage.opencover.xml" \
-targetdir:"coveragereport" \
-reporttypes:Html

#Arquivo index.html estará na pasta 'coveragereport'

#Adiciona todos os csproj a solução ControleDeContatos.sln
$ dotnet sln api.sln add **/*.csproj --in-root

# Rodar o teste de mutação
$ dotnet Stryker
```
