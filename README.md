na appsettings.json, na string de conexão o TrustServerCertificate=True, desativa a criptografia SSL 

na appsettings.json, 

dotnet ef database update // Atualiza o banco de dados com as migrações

dotnet ef migrations add CriandoTabelaContatos --context BancoContext // Cria uma migração para o contexto BancoContext

dotnet tool install --global dotnet-ef // Instala a ferramenta de linha de comando do Entity Framework Core

// Instalação dos pacotes necessários
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.1

