# DeskFlow API — Gestão de Chamados e Helpdesk de TI 

##  Sobre o Projeto 
A **DeskFlow API** é uma Web API REST construída em .NET Core 10 utilizando Entity Framework Core e SQL Server. 
O sistema automatiza o gerenciamento de chamados de suporte técnico, histórico de interações e acompanhamento de status do atendimento. 

##  Tecnologias Utilizadas 
- .NET Core 10 / Web API 
- Entity Framework Core 10 
- SQL Server 
- Swagger / OpenAPI
- ASP.NET Core Identity

##  Como Executar a Aplicação 

### Pré-requisitos 
- .NET SDK 10 (ou superior) 
- SQL Server em execução (LocalDB, SQL Server Express ou Docker) 

### Passo a Passo 
1. Clone este repositório:  
````
git clone https://github.com/silvanaperich/SCTEC-.Net-Projeto-Final.git
````

2. Acesse a pasta do projeto: 
````
cd SCTEC-.Net-Projeto-Final
````

3. Configure a Connection String no arquivo `appsettings.json`, exemplo: 
````
"ConnectionStrings": { 
"DefaultConnection": "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Encrypt=False;Trust Server Certificate=True;Database=dbDeskFlow;" 
} 
````
Observação: se preferir, altere o nome do banco ou ajuste a string de conexão conforme a necessidade do seu ambiente.

4. Execute as Migrations para criar a estrutura no banco de dados: 
````
dotnet ef database update 
````

5. Execute a API: 
````
dotnet run 
````

6. Acesse a documentação do Swagger para testar os endpoints, adicionando `/swagger` no final da url, exemplo: 
````
http://localhost:5103/swagger 
````

## Autenticação

1. Efetue o registro email e senha para o login no endpoint:
````
/auth/register
````

2. Efetue o login no endpoint:
````
/auth/login
````

3. Copie o token do atributo `accessToken`;

4. Clique no botão `Authorize`, cole o token e clique em 'Authorize' para confirmar e feche a janela de autorização.

5. Pronto, agora poderá consumir os endpoints da API.

##  Ciclo de Vida do Chamado 
- **Aberto**: Chamado registrado pelo solicitante com a descrição do cenário para atendimento. 
- **EmAndamento**: Chamado em análise ou em processamento da ação necessária. 
- **Fechado**: Chamado encerrado com texto de solução e data de fechamento.

## Arquitetura em Camadas 
- **Controllers**: Recebem as requisições HTTP e definem os Status Codes, chamando a camada Services. 
- **Services**: Contêm as regras de negócio e validação dos status, chamando a camada Repositories. 
- **Repositories**: Executam comandos (Inserção, Alteração, Exclusão) e consultas de banco via EF Core. 
- **Middlewares**: Tratamento de erros globais da API.

##  Segurança e Autenticação
- **ASP.NET Core Identity**: gerenciamento de logins, geração e validação de Tokens. 