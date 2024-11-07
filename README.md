# Sistema de gerenciamento de presença ELLP
## Descrição do Projeto
Sistema de gerenciamento de presença dos alunos do projeto de extensão ELLP (Ensino Lúdico de Lógica de Programação), destinado ao controle de frequência nas oficinas de ensino de lógica e programação.
## Grupo
- Fabio Nascimento Dos Santos Junior
- Eduardo Gabriel De Freitas
- Jéssica Mota Pereira
## 1. Requisitos Funcionais

| ID | FUNCIONALIDADE |
| ----------- | ----------- |
| RF01 | O sistema deve permitir o cadastro de alunos voluntários (professores no projeto). |
| RF02 | O sistema deve permitir que alunos voluntários façam login. |
| RF03 | O sistema deve permitir o cadastro de workshops. |
| RF04 | O sistema deve permitir a inclusão de alunos voluntários como instrutores nos workshops. |
| RF05 | O sistema deve permitir o registro de presença dos alunos participantes em workshops usando QR Code. |
| RF06 | O sistema deve permitir a consulta de relatórios de workshops com alunos voluntários e participantes. |

## Cenários de uso e critérios de aceitação

### RF01: Cadastro de Alunos Voluntários (Professores)
**Cenário de uso:** Um usuário acessa a página de cadastro, preenche seus dados (nome, email, senha, etc.) e finaliza o cadastro. O sistema deve criar a conta do usuário e exibir uma mensagem de confirmação.

**Critérios de aceitação:** 
- O sistema deve validar que todos os campos obrigatórios estão preenchidos. 
- Se os dados estiverem corretos e completos, o cadastro deve ser bem-sucedido e o usuário deve ver uma mensagem de confirmação.
- Caso algum campo esteja incorreto ou incompleto, o sistema deve exibir mensagens de erro apropriadas.

### RF02: Login de Alunos Voluntários
**Cenário de uso:** Um aluno voluntário já cadastrado acessa a página de login, insere seu email e senha, e tenta entrar no sistema.

**Critérios de aceitação:** 
- Se as credenciais forem corretas, o usuário deve ser autenticado e redirecionado para o painel principal.
- Se as credenciais estiverem incorretas, o sistema deve exibir uma mensagem de erro específica.
- Após múltiplas tentativas incorretas, o sistema deve bloquear temporariamente o acesso.

### RF03: Cadastro de Workshops
**Cenário de uso:** O professor acessa o sistema, seleciona a opção de cadastro de workshops e adiciona um novo workshop com dados como nome, descrição, etc.

**Critérios de aceitação:** 
- O workshop deve ser criado com sucesso, e uma mensagem de confirmação deve ser exibida ao professor.
- Todos os campos obrigatórios para a criação de um workshop devem ser preenchidos.
- O sistema deve evitar duplicações de workshop com o mesmo nome e data.

### RF04:  Inclusão de Alunos Voluntários no Workshop
**Cenário de uso:** O criador do workshop associa um aluno voluntário como instrutor para organizar as aulas.
**Critérios de aceitação:** 
- O aluno voluntário deve ser adicionado corretamente ao workshop selecionado.
- Caso o aluno já esteja associado, o sistema deve notificar o professor.


### RF05: Registro de Presença dos Alunos Participantes com QR Code
**Cenário de uso:** Durante o workshop, o professor solicita a geração de um QR code para que os alunos participantes possam registrar a presença.

**Critérios de aceitação:** 
- O QR code deve ser gerado e exibido ao professor com um período de validade específico.
- O QR code deve ser único para cada sessão de workshop.
- Após o período de validade, o QR code deve expirar e não ser mais utilizável

### RF06: Consulta de Relatórios de Workshops
**Cenário de uso:** O professor acessa o sistema para consultar o relatório de um workshop específico, visualizando a lista de alunos voluntários e participantes.

**Critérios de aceitação:** 
- O sistema deve exibir a lista de presença corretamente, mostrando os alunos voluntários e os participantes, incluindo datas e horários de presença.
- O professor deve poder visualizar o relatório por data, aluno voluntário e workshop.

## 2. Arquitetura em Alto Nível do Sistema
Clean Architecture foi a arquitetura escolhida para ser utilizada no backend.

A Clean Architecture, proposta por Robert C. Martin (também conhecido como Uncle Bob), é uma abordagem de design de software que promove a separação de responsabilidades e a independência dos componentes do sistema. Seu objetivo principal é criar sistemas que sejam fáceis de entender, testar, manter e evoluir ao longo do tempo.

A Clean Architecture é frequentemente representada como uma série de camadas concêntricas, onde cada camada tem uma responsabilidade específica e depende apenas das camadas mais internas
Aqui estão as principais camadas:
- **Entidades (Entities):** São objetos de negócio que encapsulam as regras críticas do domínio. Essas entidades são independentes de qualquer framework, interface de usuário ou banco de dados.
- **Casos de Uso (Use Cases):** Contêm a lógica de aplicação específica para coordenar o fluxo de dados entre entidades. Eles definem as operações que podem ser realizadas no sistema e implementam as regras de negócio.
- **Interface de Controle (Controllers):** Esta camada adapta dados de e para a camada de casos de uso. Inclui gateways (ou reposítórios) que convertem dados para um formato que a camada de casos de uso entende e vice-versa. Também inclui controladores e apresentadores que convertem dados da interface de usuário.
- **Ui, Frameworks e etc:** Contêm detalhes específicos de frameworks, bancos de dados, interfaces de usuário e outros mecanismos externos. Essa camada é o que a maioria dos frameworks web fornece: detalhes de implementação e bibliotecas específicas.
Alem disso é importante salientar que a Clean Architecture é fortemente baseada nos princípios SOLID, aproveitando esses princípios para criar uma estrutura de software robusta, modular e fácil de manter.

Pensando em um sistema que o usuario tera login e senha para realizar o visto por questão de segurança. O sistema do back-end será feito para ser um sistema que terá a segurança para isso com uma rota de put pra confirmar e uma de get quando o status da api for 200.

### Diagrama da Arquitetura
![image](https://github.com/user-attachments/assets/8525ca8b-ba9f-4acd-94c0-5a9bbe1f00bb)


## 3. Estratégia de Automação de Testes
### 1. Objetivos da Automação 
- Cobertura: Garantir a qualidade das funcionalidades essenciais do sistema por meio de testes end-to-end que cobrem as principais interações dos usuários.
- Eficiência: Reduzir o tempo de execução dos testes manuais repetitivos, garantindo que o sistema funcione corretamente após cada atualização.
- Consistência: Padronizar os testes para obter resultados confiáveis e consistentes em cada execução, minimizando erros humanos.

### 2. Escopo dos Testes Automatizados 
- Testes End-to-End: Automatização dos cenários de teste cobrindo fluxos completos de usuário, como cadastro, login, registro de presença e geração de QR code, garantindo que cada interação funcione conforme o esperado.
- Testes Funcionais nas Rotas da API: Realização de testes para verificar se as rotas da API estão respondendo corretamente e se os dados esperados estão sendo retornados. Isso inclui testes de retorno para operações como cadastro de alunos voluntários e login de usuários.

### 3. Ferramentes para Automação 
- Cypress: Ferramenta principal para execução de testes end-to-end, com suporte para validações em tempo real de interfaces web.
- Robot Framework: Alternativa ao Cypress, especialmente para cenários mais complexos ou casos em que o Cypress encontre limitações.

###  4. Ambiente de Execução dos Testes
- Ambiente de Desenvolvimento: Os testes serão inicialmente executados neste ambiente para validar as funcionalidades conforme forem implementadas e revisadas.

### 5. Critérios de Aceitação para a Automação 
- Cobertura de Teste: Alcançar um mínimo de 80% de cobertura dos fluxos principais e interações essenciais do sistema.
- Tempo de Execução: Limite máximo de 15 minutos para a execução completa dos testes end-to-end, garantindo eficiência no ciclo de desenvolvimento.
- Manutenção Contínua: Revisão e atualização dos scripts conforme o projeto avança, incluindo novos cenários e ajustes conforme as funcionalidades mudam.

### 6. Relatórios e Monitoramento 
- Relatórios Automatizados: Geração de relatórios após cada execução dos testes para documentar o sucesso e falhas das execuções, permitindo que a equipe acompanhe o progresso e os problemas encontrados.

## 4. Tecnologias Utilizadas
### Principais Tecnologias
- Linguagens de Programação: [.NET(C#), JavaScript]
- Frameworks: [React, Specflow, coverage]
- Banco de Dados: [MYSQL, Entity Framework)]
- Outras Ferramentas: [Docker, GitHub Actions]

