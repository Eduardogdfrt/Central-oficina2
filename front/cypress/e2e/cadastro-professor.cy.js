describe('Cadastro de Professor', () => {
    beforeEach(() => {
        // Visita a página de cadastro
        cy.visit('http://localhost:3000/cadastro'); // Substitua pela URL correta
    });

    it('Deve Clicar para cadastrar e fazer cadastro', () => {
        cy.get('input[placeholder="Digite seu nome"]')
            .should('be.visible') // Verifica se o campo está visível
            .type('Professor Teste');
        cy.get('input[placeholder="Digite seu e-mail"]')
            .should('be.visible') // Verifica se o campo está visível
            .type('teste@teste.com'); // Preenche o campo com o e-mail "teste@exemplo.com"
        cy.get('input[placeholder="Digite sua senha"]')
            .should('be.visible') // Verifica se o campo está visível
            .type('SenhaSecreta123'); // Preenche o campo com a senha "SenhaSecreta123"
        cy.get('input[placeholder="Digite sua especialidade"]')
            .should('be.visible') // Verifica se o campo está visível
            .type('Teste');
        cy.get('button.button')
            .should('be.visible') // Verifica se o botão está visível
            .contains('CADASTRAR') // Verifica que o texto no botão é "CADASTRAR"
            .click(); // Clica no botão "CADASTRAR"
    });

    it('Deve falhar ao tentar fazer cadastro com e-mail já cadastrado', () => {
        cy.get('input[placeholder="Digite seu nome"]').type('Professor Teste');
        cy.get('input[placeholder="Digite seu e-mail"]').type('teste@teste.com');
    cy.get('input[placeholder="Digite sua senha"]').type('SenhaSecreta123');
    cy.get('input[placeholder="Digite sua especialidade"]').type('Teste');
    cy.get('button.button').contains('CADASTRAR').click();});

    it('Deve falhar ao tentar fazer cadastro com e-mail inválido', () => {
        cy.get('input[placeholder="Digite seu nome"]').type('Professor Teste');
        cy.get('input[placeholder="Digite seu e-mail"]').type('teste');
        cy.get('input[placeholder="Digite sua senha"]').type('SenhaSecreta123');
        cy.get('input[placeholder="Digite sua especialidade"]').type('Teste');
        cy.get('button.button').contains('CADASTRAR').click();
    });

    it('Deve falhar ao tentar fazer cadastro com senha inválida', () => {
        cy.get('input[placeholder="Digite seu nome"]').type('Professor Teste');
        cy.get('input[placeholder="Digite seu e-mail"]').type('00000000');
        cy.get('input[placeholder="Digite sua senha"]').type('senha');
        cy.get('input[placeholder="Digite sua especialidade"]').type('Teste');
        cy.get('button.button').contains('CADASTRAR').click();
    });

    it('Deve falhar ao tentar fazer cadastro com especialidade inválida', () => {
        cy.get('input[placeholder="Digite seu nome"]').type('Professor Teste');
        cy.get('input[placeholder="Digite seu e-mail"]').type('00000000');
        cy.get('input[placeholder="Digite sua senha"]').type('SenhaSecreta123');
        cy.get('input[placeholder="Digite sua especialidade"]').type('Teste');
        cy.get('button.button').contains('CADASTRAR').click();
    });

    it('Deve falhar ao tentar fazer cadastro com todos os campos vazios', () => {
        cy.get('button.button').contains('CADASTRAR').click();
    });

    it('Deve falhar ao tentar fazer cadastro com nome vazio', () => {
        cy.get('input[placeholder="Digite seu nome"]').type('Professor Teste');
        cy.get('button.button').contains('CADASTRAR').click();
    });

    it('Deve falhar ao tentar fazer cadastro com e-mail vazio', () => {
        cy.get('input[placeholder="Digite seu e-mail"]').type('00000000');
        cy.get('button.button').contains('CADASTRAR').click();
    });

    it('Deve falhar ao tentar fazer cadastro com senha vazia', () => {
        cy.get('input[placeholder="Digite sua senha"]').type('SenhaSecreta123');
        cy.get('button.button').contains('CADASTRAR').click();
    });
    it('Deve falhar ao tentar fazer cadastro com especialidade vazia', () => {
        cy.get('input[placeholder="Digite sua especialidade"]').type('Teste');
        cy.get('button.button').contains('CADASTRAR').click();
    });
    
});

