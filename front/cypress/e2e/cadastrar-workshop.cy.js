describe('Cadastro de Workshop', () => {
    beforeEach(() => {
        // Visita a página de cadastro
        cy.visit('http://localhost:3000/login');// Substitua pela URL correta
        cy.get('input[placeholder="Digite seu e-mail"]').type('23106789');

    // Encontra o campo de senha e digita "Teste123"
    cy.get('input[placeholder="Digite sua senha"]')
      .should('be.visible') // Verifica se o campo está visível
      .type('Teste123');

    // Encontra o botão "ENTRAR" e clica
    cy.get('button.button').contains('ENTRAR').click();

    // Valida que a navegação ou feedback esperado ocorreu
    // Substitua '/professor/home' pela URL de destino correta
    cy.url().should('include', '/workshops');
    });

    it('Deve Clicar para cadastrar e fazer cadastro', () => {
        cy.get('p.text-card')
        .contains('Adicionar') // Verifica o texto antes de clicar
        .click(); 
      cy.url().should('include', '/workshop-cadastro');
      cy.get('input[placeholder="Digite o nome"]')
      .should('be.visible') // Verifica se o campo está visível
      .type('Teste');
      cy.get('input[type="date"]')
      .should('be.visible') // Verifica se o campo está visível
      .type('2024-12-25');
      cy.get('button.button')
      .should('be.visible') // Verifica se o botão está visível
      .contains('CADASTRAR');
    });

    it('Deve dar erro no cadastro', () => {
        cy.get('p.text-card')
        .contains('Adicionar') // Verifica o texto antes de clicar
        .click(); 
      cy.url().should('include', '/workshop-cadastro');
      cy.get('input[placeholder="Digite o nome"]')
      .should('be.visible') // Verifica se o campo está visível
      .type('Teste');
      cy.get('input[type="date"]')
      .should('be.visible') // Verifica se o campo está visível
      .type('222222-12-25');
      cy.get('button.button')
      .should('be.visible') // Verifica se o botão está visível
      .contains('CADASTRAR');
    });
});