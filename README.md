# HackerApiViewerProject
## Content
### HackerApiViewer (Angular 10.0.4 Client Front End)
  * cd into Client folder
  * npm install (installs dependencies including cypress)
  * ng serve to run client
  * Unit Testing via Jasmine/Karma (ng test)
  * Integration Testing via https:cypress.io (npx cypress open)
### HackerApiClient (Api Client Backend)
  * run /HackerAPI-Project/hackerAPIServer/hackerAPI.Client.sln in Visual Studio (compiled in vs2019)
  * Unit Testing via xUnit (no Moq on this release)
#### Features
  * https://localhost:5001/swagger => Swagger API
  * redis for backend caching (referd to readme under Redis directory)
  
## What it does:
* Uses the awesome HackerNews API https://github.com/HackerNews/API to pull newest, best or top stories and angular material to make it look good :D!
* Caches using redis on backend. 
* Stories can be filtered via title, and selected.
* A user can be selected and their info viewed
* A users list of submitted story items can be viewed and the value clicked to redirect to link in a new tab.
* Sorting of items via table headers
* Progress Bars (needed for intial caching :D)
* Pagination 
* Selection of 3 possible categories of stories via hamburger menu.
* Error messaging when clicking on link that does not have a url in user submitted stories.
* All endpoints can be also tested via swagger :D

## Known Issues
* When Integration testing, caching may cause first test to fail.
* Initial caching may take a bit, sometimes as long as 33s. After that though data will load from cache.
* Sometimes server will return first value as null from HackerAPI.

## Notes
* I kept the testing small, as I just wanted to show that I can do integrated and unit testing. In a real world situation this would be much more extensive, especially for endpoints
* All tests passed on my local (famous last words lol)
* Was also going to add that a stories title would appear when viewing a users submitted stories. Not really liking that dropdown :(
* wanted to add a lot of things like making sure observables are unsubcribed, as well as managing handling of incoming data in a different way. decided to wrap it up though.
