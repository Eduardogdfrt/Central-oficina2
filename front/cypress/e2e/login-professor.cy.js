describe('Teste de Login - Professor', () => {
  beforeEach(() => {
    // Visita a página de login
    cy.visit('http://localhost:3000/'); // Substitua pela URL correta
  });

  it('Deve Clicar para entrar e fazer login', () => {
    cy.get('span.rest-text').contains('ENTRAR').click();
    // Encontra o campo de e-mail e digita "23106789"
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

  it('Deve falhar ao tentar fazer login com senha incorreta', () => {
    cy.get('.rest-text').contains('ENTRAR').click();
    cy.get('input[placeholder="Digite seu e-mail"]').type('23106789');
    cy.get('input[placeholder="Digite sua senha"]').type('Teste1234');
    cy.get('button.button').contains('ENTRAR').click();
    cy.get('p[style="color: red;"]').should('contain', 'Erro: Invalid professor ID or password');
  });
  it('Deve falhar ao tentar fazer login com ID incorreto', () => {
    cy.get('.rest-text').contains('ENTRAR').click();
    cy.get('input[placeholder="Digite seu e-mail"]').type('23106788');
    cy.get('input[placeholder="Digite sua senha"]').type('Teste123');
    cy.get('button.button').contains('ENTRAR').click();
    cy.get('p[style="color: red;"]').should('contain', 'Erro: Invalid professor ID or password');
  });
  it('Deve falhar ao tentar fazer login com ID e senha incorretos', () => {
    cy.get('.rest-text').contains('ENTRAR').click();
    cy.get('input[placeholder="Digite seu e-mail"]').type('23106788');
    cy.get('input[placeholder="Digite sua senha"]').type('Teste1234');
    cy.get('button.button').contains('ENTRAR').click();
    cy.get('p[style="color: red;"]').should('contain', 'Erro: Invalid professor ID or password');
  });
  it('Deve falhar ao tentar fazer login com ID e senha vazios', () => {
    cy.get('.rest-text').contains('ENTRAR').click();
    cy.get('button.button').contains('ENTRAR').click();
    cy.get('p[style="color: red;"]').should('contain', 'Erro: Invalid professor ID or password');
  });
  it('Deve falhar ao tentar fazer login com ID vazio', () => {
    cy.get('.rest-text').contains('ENTRAR').click();
    cy.get('input[placeholder="Digite seu e-mail"]').type('23106788');
    cy.get('button.button').contains('ENTRAR').click();
    cy.get('p[style="color: red;"]').should('contain', 'Erro: Invalid professor ID or password');
  });
  it('Deve falhar ao tentar fazer login com senha vazia', () => {
    cy.get('.rest-text').contains('ENTRAR').click();
    cy.get('input[placeholder="Digite sua senha"]').type('Teste123');
    cy.get('button.button').contains('ENTRAR').click();
    cy.get('p[style="color: red;"]').should('contain', 'Erro: Invalid professor ID or password');
  });
  

});
