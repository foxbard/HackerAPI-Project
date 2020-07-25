
describe('Hacker Api Viewer Integration Test', () => {
  it('verifies page can load', () => {
    cy.visit('http://localhost:4200')
    // expect(true).to.equal(false)
    cy.wait(3000)
    cy.contains('Select a author')
    cy.get('input')
      .type('Microsoft')
    cy.wait(200)
    cy.get('input')
      .type('zzzzzz')

    cy.get('.hamburger').click()
    cy.wait(500)
    cy.contains('Newest Stories').click()
    cy.wait(500)
    cy.contains('Top Stories').click()
    cy.wait(500)
    cy.contains('Best Stories').click()
    cy.wait(500)
    cy.get('.userBtn:first').click()

    // Fails due to cors
    // cy.contains('Story Link').first().click()

  })
})
