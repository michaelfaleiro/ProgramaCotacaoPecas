# README - Sistema de Cotações de Peças Automotivas

## Descrição do Projeto
Este projeto consiste em uma Web API desenvolvida em C# utilizando o MongoDB como banco de dados. A aplicação tem como objetivo gerenciar cotações de peças automotivas, permitindo a inclusão de produtos, associação de fornecedores e definição de preços.

## Funcionalidades Principais

### 1. Abertura de Cotação
   - Crie uma nova cotação informando detalhes como o carro e o chassi associado.

### 2. Adição de Produtos
   - Adicione produtos à cotação mesmo sem ter o código SKU.
   - Associe detalhes do produto, como nome, quantidade, e outros, conforme necessário.

### 3. Inclusão de Preços de Fornecedores
   - Inclua preços de diferentes fornecedores para um mesmo produto.
   - Registre informações como fornecedor, SKU, marca, preço de custo, quantidade atendida, entre outros.

### 4. Avaliação e Escolha de Melhores Condições
   - Analise os preços dos fornecedores e escolha a melhor condição de compra para cada produto.
   - Considere fatores como preço de custo, fabricante, preço de venda, entre outros.

## Tecnologias Utilizadas
- C#
- ASP.NET Core
- MongoDB

## Configuração do Ambiente de Desenvolvimento
1. **Pré-requisitos:**
   - [.NET Core SDK](https://dotnet.microsoft.com/download)
   - [MongoDB](https://www.mongodb.com/try/download/community)

2. **Configuração do MongoDB:**
   - Certifique-se de ter uma instância do MongoDB em execução.
   - Configure a conexão no arquivo de configuração do aplicativo.

3. **Execução do Projeto:**
   - Clone o repositório.
   - Navegue até o diretório do projeto.
   - Execute o comando `dotnet run` para iniciar a aplicação.

4. **Documentação da API:**
   - Acesse a documentação da API para obter detalhes sobre os endpoints disponíveis.

## Contribuição
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues para reportar bugs, propor novas funcionalidades ou melhorias.

## Contato
Para mais informações, entre em contato:
- [Email](mailto:michaelfaleiro@gmail.com)
- [LinkedIn](https://www.linkedin.com/in/michaelfaleiro/)

---

Esse README fornece uma visão geral do projeto, suas funcionalidades, requisitos, e instruções básicas para configuração e execução. Certifique-se de personalizá-lo conforme necessário para refletir precisamente os detalhes do seu projeto.
