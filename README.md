# Desafio para candidatos vaga de desenvolvedor Engenhos
-	Precisamos que seja construído um console application capaz de se integrar a API do Azure DevOps para consultar todos os workitems de um projeto e salvá-los em um banco de dados
-	Construção de um site (Angular ou React e com visual em Bootstrap padrão) para exibição em formato dos itens retornados com opção de ordenação por data e filtro por tipo de Workitem

## Instruções do Projeto LoadWorkItems
1. Criar um banco de dados com o nome WorkItems.
2. Mudar o valor da variavel connectionString na classe Program.
3. No banco de dados execulta a Query abaixo.

    CREATE TABLE [dbo].[Item] (
        [Id]    INT            NOT NULL,
        [Title] NVARCHAR (200) NULL,
        [Type]  NVARCHAR (100) NULL,
        [Data]  DATETIME       NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
  
4. Rode o projeto, na opção "Deseja deixa as configurações padrões?(S/N)", caso digite sim sera consumido uma api padrão do meu repositório,
  caso digite não sera solicitado a	URL do Azure DevOps e o	Nome do projeto onde estão os workitems.
  OBS: O Projeto do Azure deve estar como Público.
  
## Instruções do Projeto LoadWorkItemsWebApiService
1. Este Projeto deve estar sendo execultado para o Projeto Angular se conectar.
1. Para verificar o funcionamento da api, com o projeto em execução, basta acessar a url: http://localhost:porta/api/item?&pageNumber=1&type , nela vai conter o xml gerado. 

## Instruções do Projeto LoadWorkItemsAngular
1. Após subir o servidor, abrir a url http://localhost:4200/, nesta página vai listar os work items.
2. Para ordena por data, basta clicar no icone ao lado da label Data e para filtrar pelo tipo é só selecionar uma das opções do dropdown.
    
    
  
