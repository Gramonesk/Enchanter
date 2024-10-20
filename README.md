<h1>Enchanter</h1>

![0709](https://github.com/Gramonesk/Enchanter/assets/154248035/41b20f66-24fe-476e-885b-ddab74e6b318)

## 🔴 About Project
  This was my personal project that was oriented to a Gacha Deck-Building Strategy type of genre. The game idea initially was made due to my superimposed imagination seemingly describing a fantasy story. The whole mechanic of this game was made as a mix of several games mechanics i played prior to this game. This project is still in further development for optimization and ideation.

## 📋 Project Info 
* Editor Version : Unity 2022.3.29f1

* My Contribution : Overall (Ideation, Design, Story, Mechanics, Art [Not Applied Yet] )
<br>

## 🕹️ About Game
Enchanter is a strategic card-based deck-building game where players take turns attacking opponents based on the speed of their characters. Each decision impacts the flow of battle, creating a dynamic and engaging gameplay experience.

## 📜 Scripts and Features

| Location |  Script       | Description                                                  |
|-----| ------- | ------------------------------------------------------------ |
|DataPersistence| `DataManager.cs` | Manages data storage and data distribution towards the interfaces. |
|DataPersistence| `DataHandler.cs` | Handles the save and loading system for the game. |
|Gameplay| `Inventory.cs` | Stores picture data and its detail for further uses during gameplay. |
|Manager| `UIManager.cs`  | Manages pausing and various UI element functions. |
|Underwater| `ScreenshotHandler.cs`  | Handles screenshot and also album creating. |
| | `etc`  | |
<br>

<details>
  <summary>More Details</summary>
  
1. **Data Persistence**
   - using JSON, filestream and furthermore using generics and interfaces to make it modular and appliable for all my other projects, this mechanics allows me to save data ex: string datas, pictures and more
   - using inventory system that retrieves data i saved either by singleton referencing or straight from loading the game so that the photo data can be used to sell and display what was taken before 
3. **Screen snapping and game resolution**
    - used for taking photos of the sea turtles and saving it, this also scales with the game resolution so that it wont break the game
4. **Design Patterns**
    - using an Invoker so gameplay feels robust especially when interacting with the pause menu or UI
5. **Navigation mesh**
    - using a navmesh to make the npc move and interact with the environment to make the gameplay feel more filled.
6. **URP POST-PROCESSING**
    -  Implimentation of post-processing effects in unity
    -  Lights 2D used for improved visual
7. **Object pooling**
   - using an object pooling to reduce memory buffer and also a large performance boost on the game
8. **State Machine Pattern**
   - using statemachine to control states pattern and reduce potential bug threats on the game.
</details>


<details>
  <summary>What i learned</summary>

- As the game was more data-structure oriented and also was the game made with personal interest, the development process took way longer which taught me a lot in terms of Game Designing and Program designing. In terms of program designing skills, i learned to use or utilize making a class diagram first before making the mechanic, this is to make the code more modular and open.
- Through the process, i learned to identify the base mechanics of the game. As an example, a notable game "Yu-Gi-OH" have effects mechanics such that they have a trigger effect like [On Draw] [On Discard] effects. These effects are modifications subsequently to the main effect of the base mechanics of the card. Even as a game programmer, knowing this would save me alot of times from disambiguation and long-term thinking. This is why most programmers tend to ask the designers regarding such mechanics in which to reassure the mechanics given to him. 
- From this knowledge i also learned that making a design document and managing tasks are the most important aspect of game developing because it points out what was necessary at the current time. Even then when making these mechanics, i would still make the codes simple; open for modification and future uses.
<br>
</details>

## 📂Files description

```
├── Aquarencia                       # Folder containing all the Unity project files, to be opened by a Unity Editor
   ├── ...
   ├── Assets                        # Folder containing all code, assets, scenes, etc used for development. This was not automatically created by Unity
      ├── ...
      ├── Scenes                     # Folder containing several scenes that you can open and play the game via Unity
      ├── Script                     # Folder containing all the scripts related to making the game
      ├── ....
   ├── ...
      
```
<br>

## 🕹️ Controls
| Function | KeyCode |
|:---:|:---:|
| LMB | interact |
| Esc | Menu |
