# SearchChallenge2020
A project to implement a search contest between search engines to return results of searching for a programming language's name.

# ProgLangContest: 
Solution implemented for .Net Core. It consist of the client SearchFight console application that works making calls to the SearchEngines.Api. 
The Api exposes a single endpoint that accepts the search term (prog language name) and return the results by search engine. 
The internals of the search implementation for each search engine are managed by the SearchEnginesService service. There is a set of available search engine to be
loaded dinamically, each search engine can implement the search on its own and using its 3rd party search api.    
 
To review the solution:
1 - Open the solution in Visual Studio 2019
2 - Start a new debugging instance of the project: SearchEngines.Api; In the browser, the 'is alive' starting point to try is: http://localhost:50938/api/search
3 - Start a new debugging session of the project : SearchFight (Console) , or
4 - Go to the path: SearchFight\bin\Debug\netcoreapp3.1 :
 4.1 - Open Cmd window
 4.2 - Type a line to execute , for example: SearchFight.exe .net java "java script" scala python
   
# SearchFight.NetFramework: 
Solution with the initial implementation for .Net framework. For compatibility and initial verification purposes.   
It also can be reviewed using the included client console application. 
