# StockQuoteTracking
Este projeto tem como objetivo monitorar a cotação de ativos da B3 e enviar notificações por e-mail caso o valor de um ativo caia abaixo de um limite ou suba acima de outro. Para simular as cotações, devido à limitação de acesso às APIs de valores em tempo real, foi gerado um número aleatório entre 0 e 100 para representar os valores das ações. Além disso, o projeto demonstra o consumo da API da Alpha Vantage para obter dados históricos e gerar gráficos de velas (candlestick) simples no terminal.

Funcionalidades
---------------

-   **Monitoramento de Ações da B3:** O sistema monitora a cotação de ações brasileiras e envia e-mails quando as cotações caem abaixo de um valor mínimo ou sobem acima de um valor máximo.

-   **Simulação de Preço de Ações:** Como as APIs de valores em tempo real são pagas, o sistema gera números aleatórios entre 0 e 100 para simular os valores de ações.

-   **Gráfico Candlestick:** O projeto consome dados históricos de uma ação via a API da Alpha Vantage e exibe um gráfico simples de velas (candlestick) no terminal. Este processo também valida se a ação fornecida é da B3.

-   **Envio de E-mails:** Quando a cotação de um ativo atinge os limites pré-definidos, o sistema envia um e-mail de notificação utilizando o serviço de SMTP do SendGrid.

Tecnologias e Serviços Utilizados
---------------------------------

-   **API da Alpha Vantage:** Utilizada para consumir dados históricos das ações.
-   **SendGrid via SMTP:** Serviço utilizado para enviar e-mails de notificação.
-   **.NET (C#):** A aplicação foi desenvolvida como um projeto console utilizando o .NET Framework.
-   **Xunit, Moq e Bogus:** Bibliotecas utilizadas para criação de testes unitários, mocks e geração de dados fictícios, respectivamente.
-   **SkiaSharp:** Biblioteca para gerar gráfico para o corpo do email com os valores de cotações.

Estrutura do Projeto
--------------------

O projeto é uma aplicação console em C# que consome dados da API Alpha Vantage para gerar gráficos históricos e envia e-mails quando os preços das ações atingem os limites definidos.

### Diretórios principais:

-   `src/`: Contém o código fonte principal da aplicação.
-   `tests/`: Contém os testes unitários utilizando Xunit, Moq e Bogus.

Como Rodar o Projeto
--------------------

### Requisitos

-   .NET Core 3.1 ou superior
-   Conta no [SendGrid](https://sendgrid.com/) para envio de e-mails
-   Chave da API da [Alpha Vantage](https://www.alphavantage.co/)

### Passos

1.  Clone o repositório:

    ```
    git clone https://github.com/seuusuario/projeto-monitoramento-acoes.git
    cd projeto-monitoramento-acoes

    ```

2.  Instale as dependências:

    ```
    dotnet restore

    ```

3.  Defina as variáveis de ambiente para a chave da API da Alpha Vantage e as credenciais do SendGrid:

    -   `ALPHA_VANTAGE_API_KEY`
    -   `SENDGRID_API_KEY`
    -   `SENDGRID_FROM_EMAIL`
    -   `SENDGRID_TO_EMAIL`
4.  Para rodar a aplicação:

    ```
    dotnet run

    ```

5.  Para rodar os testes unitários:

    ```
    dotnet test

    ```

Exemplo de Uso
--------------

A aplicação, ao ser executada, realizará o monitoramento das cotações das ações e exibirá as notificações por e-mail caso os limites de valor sejam atingidos. O gráfico de candlestick será gerado no terminal ao consumir dados históricos de um ativo da B3.
