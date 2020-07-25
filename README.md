# HackerApiViewerProject
## Content
### HackerApiViewer (Angular 10.0.4 Client Front End)
  * npm install (installs dependencies including cypress)
  * ng serve to run client
  * Unit Testing via Jasmine/Karma (ng test)
  * Integration Testing via https:cypress.io (npx cypress open)
### HackerApiClient (Api Client Backend)
  * dotnet restore
  * dotnet build
  * dotnet run
  * (Or launch via Visual Studio)
  * Unit Testing via xUnit (no Moq on this release)
#### Features
  * https://localhost:5001/swagger => Swagger API
  
## What it does:
* Uses the awesome HackerNews API https://github.com/HackerNews/API to pull newest, best or top stories and angular material to make it look good :D!
* Stories can be filtered via title, and selected.
* A user can be selected and their info viewed
* A users list of submitted story items can be viewed and the value clicked to redirect to link in a new tab.
* Sorting of items via table headers
* Pagination 
* Selection of 3 possible categories of stories via hamburger menu.
* Error messaging when clicking on link that does not have a url in user submitted stories.
* All endpoints can be also tested via swagger :D


## Notes
* I kept the testing small, as I just wanted to show that I can do integrated and unit testing. In a real world situation this would be much more extensive, especially for endpoints
* All tests passed on my local (famous last words lol)
* Was also going to add that a stories title would appear when viewing users submitted stories. Not really liking that dropdown :(
* wanted to add a lot of things like making sure observables are unsubcribed, as well as managing handling of incoming data in a different way. Also wanted to add so that caching is used on backend to handle data, api data being sent to client, decided to wrap it up though.
