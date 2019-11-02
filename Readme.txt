Instruction To Run:
- Goto  “AireLogicTechTest\Compiled Tech Test\AireLogicTechTest.UI.exe”
- Run
- Type artist name

Summary of project
There are a few assumptions/compromises I made while programming the technical test and I'd like to explain them here.
I tried to keep the code sensible.
1. API Response to Dynamics - While  didI think about creating a class to deserialise the raw JSON response into it didn’t seem worth it for a project of this size. I knew exactly which property I needed ArtistID and because there was only one I decided to just hold the JSON response in a dynamic.
2. Interfaces and DI - Again due to the size of the project and the fact that I knew this was a throw away it didn’t seem reasonable to use dependency injection. However, if needed to I would create interfaces for the ApiCaller, LyricsFormatter and ProgramRunner and call the interfaces in UI project instead of the concrete types.
3. Unit Tests - I just created one unit test project to show how I would write them. If this was more then a prototype I would create unit tests for the LyricsFormatter and ProgramRunner too.
