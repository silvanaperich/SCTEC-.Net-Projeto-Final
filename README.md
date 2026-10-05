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
- Ferramenta do EF Core (se ainda não tiver):
```
dotnet tool install --global dotnet-ef
```

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

## Endpoints

### Categorias
| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/categorias` | Lista todas as categorias |
| GET | `/api/categorias/{id}` | Busca uma categoria |
| POST | `/api/categorias` | Cadastra uma categoria |
| PUT | `/api/categorias/{id}` | Altera o nome |
| DELETE | `/api/categorias/{id}` | Exclui (bloqueado se houver chamados vinculados) |

### Chamados
| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/chamados` | Abre um chamado (status `Aberto` e data de abertura automáticos) |
| GET | `/api/chamados` | Lista com filtros opcionais: `status`, `prioridade`, `categoriaId` |
| GET | `/api/chamados/{id}` | Detalhes, com categoria e interações |
| POST | `/api/chamados/{id}/iniciar` | `Aberto` → `EmAndamento` |
| POST | `/api/chamados/{id}/encerrar` | `EmAndamento` → `Fechado` (exige `solucao`) |
| POST | `/api/chamados/{id}/interacoes` | Adiciona comentário (não aceita chamado `Fechado`) |
| PUT | `/api/chamados/{id}` | Altera os dados do chamado (não aceita chamado `Fechado`) |
| DELETE | `/api/chamados/{id}` | Exclui o chamado |

### Interações
| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/interacoes` | Lista todas |
| GET | `/api/interacoes/{id}` | Busca uma interação |
| POST | `/api/interacoes` | Cadastra informando o `chamadoId` (não aceita chamado `Fechado`) |
| PUT | `/api/interacoes/{id}` | Altera autor e mensagem |
| DELETE | `/api/interacoes/{id}` | Exclui |

**Valores aceitos:**
- `prioridade`: `Baixa`, `Media`, `Alta`
- `status`: `Aberto`, `EmAndamento`, `Fechado`

**Códigos de sucesso:**
- `GET`: 200 com o conteúdo
- `POST` de criação: 201, com `Location` e o item criado no corpo
- `POST /iniciar`, `POST /encerrar`, `PUT` e `DELETE`: 204 (sem corpo)

Todos os endpoints `/api/...` exigem autenticação (ver **Exemplos de Utilização**, passo 1).

## Exemplos de Utilização

Fluxo completo de um atendimento. Os exemplos podem ser executados pelo Swagger ou por qualquer cliente HTTP (a URL base em desenvolvimento é `http://localhost:5103`).
Ajuste os ids conforme o retorno de cada passo.

### 1. Criar usuário e obter o token
`POST /auth/register`
```json
{ "email": "suporte@empresa.com", "password": "Senha@123" }
```
A senha precisa ter ao menos 6 caracteres, com maiúscula, minúscula, número e símbolo.

`POST /auth/login`
```json
{ "email": "suporte@empresa.com", "password": "Senha@123" }
```
Copie o valor de `accessToken` da resposta e informe-o no botão **Authorize** do Swagger.
Em clientes HTTP, envie o cabeçalho `Authorization: Bearer {token}`.

### 2. Cadastrar uma categoria
`POST /api/categorias`
```json
{ "nome": "Hardware" }
```

### 3. Abrir um chamado
`POST /api/chamados`
```json
{
  "titulo": "Computador não liga",
  "descricao": "Ao pressionar o botão, nenhum LED acende.",
  "prioridade": "Alta",
  "solicitanteNome": "Maria Souza",
  "categoriaId": 1
}
```
O chamado é criado com status `Aberto` e a data de abertura preenchida automaticamente.  

### 4. Iniciar o atendimento
`POST /api/chamados/1/iniciar` (sem corpo)

O status passa para `EmAndamento`.

### 5. Registrar uma interação
`POST /api/chamados/1/interacoes`
```json
{ "autor": "Técnico João", "mensagem": "Fonte testada, defeito confirmado." }
```

### 6. Encerrar o chamado
`POST /api/chamados/1/encerrar`
```json
{ "solucao": "Fonte de alimentação substituída." }
```
O status passa para `Fechado` e a data de fechamento é gravada.

### 7. Consultar
- Detalhes, com categoria e interações: `GET /api/chamados/1`
- Chamados abertos de prioridade alta: `GET /api/chamados?status=Aberto&prioridade=Alta`
- Filtro por categoria: `GET /api/chamados?categoriaId=1`
- Filtros podem ser combinados.

### Exemplos de erro
As falhas retornam um JSON padronizado, sem stack trace:

```json
{ "message": "Chamado está fechado, não é possível adicionar interações." }
```

| Situação | Resposta |
|---|---|
| Recurso inexistente | 404 |
| Regra de negócio violada (transição inválida, solução vazia, categoria com chamados) | 400 |
| Erro inesperado | 500 |
| Sem token ou token inválido | 401 |

##  Ciclo de Vida do Chamado 
- **Aberto**: Chamado registrado pelo solicitante com a descrição do cenário para atendimento. 
- **EmAndamento**: Chamado em análise ou em processamento da ação necessária. 
- **Fechado**: Chamado encerrado com texto de solução e data de fechamento.

Fluxo do status do chamado é obrigatório; após fechar o chamado não pode ser reaberto.

## Estrutura de Pastas
```
DeskFlow.API/
├── Controllers/        --> Rotas HTTP e status codes
├── Services/           --> Regras de negócio e validações
│   └── Interfaces/
├── Repositories/       --> Acesso a dados (EF Core)
│   └── Interfaces/
├── Models/
│   ├── Entities/       --> Categoria, Chamado e Interacao
│   └── Enums/          --> StatusChamado e PrioridadeChamado
├── DTOs/               --> Objetos de entrada e saída da API
├── Middlewares/        --> ExceptionHandlingMiddleware (erros globais)
├── Exceptions/         --> RegrasException (violações de regra de negócio)
├── Data/               --> AppDbContext e Migrations
└── Program.cs          --> Configuração da aplicação e injeção de dependência
```

## Arquitetura em Camadas 
- **Controllers**: Recebem as requisições HTTP e definem os Status Codes, chamando a camada Services. 
- **Services**: Contêm as regras de negócio e validação dos status, chamando a camada Repositories. 
- **Repositories**: Executam comandos (Inserção, Alteração, Exclusão) e consultas de banco via EF Core. 
- **Middlewares**: Tratamento de erros globais da API.

##  Segurança e Autenticação
- **ASP.NET Core Identity**: gerenciamento de logins, geração e validação de Tokens. 

## Vídeo de Apresentação 
[Clique aqui para assistir ao vídeo de demonstração do projeto](https://drive.google.com/file/d/15KBd1OAZ0GJM1KEcBCRvlyfmfPnBjha9/view?usp=sharing) 