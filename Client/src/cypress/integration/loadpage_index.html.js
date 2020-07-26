
describe('Hacker Api Viewer Integration Test', () => {
  it('verifies page can load', () => {

    cy.server()
    // routes for testing
    cy.route('https://localhost:5001/api/items/getNewStories').as('getNewStories')
    cy.route('https://localhost:5001/api/items/getTopStories').as('getTopStories')
    cy.route('https://localhost:5001/api/items/getBestStories').as('getBestStories')
    
    cy.visit('http://localhost:4200')

    cy.wait('@getNewStories',{timeout: 80000}).then((value)=> {
      cy.contains('Select a author')
      cy.get('input').type('Micro')
      cy.wait(500)
      cy.get('input').type('{backspace}')

      cy.scrollTo(0,0);
      cy.get('.hamburger').click()
      

      cy.wait(500)
      cy.contains('Newest Stories').click()
      
      cy.contains('Top Stories').click()
      cy.wait('@getTopStories',{timeout: 50000})
      cy.contains('Best Stories').click()
      cy.wait('@getBestStories',{timeout: 50000})
      cy.get('.userBtn:first').click()
    })
   

    // Fails due to cors
    // cy.contains('Story Link').first().click()

  })
})
