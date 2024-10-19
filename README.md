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
  <br>
Overall i developed all my code structuring skills
I learned a lot about profiling, optimizing and handling memory when it comes to making this project which was a personal interest for me. Throughout the process of making this project, i spend a lot of effort and gained experience in understanding on how to make my code a lot more flexible and enabled me to modify, extend it easily with new features needed.
</br></br>
 However, i also learned to adopt a more practical approach on making code that is necessary and refactor it later when needed to increase my efficiency on my making process
</details>

<br>

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
<table width ="100%">
  <td> 
    
![Aquarencia Controls1](https://github.com/user-attachments/assets/63c78467-7f67-418b-8088-4257111cdd26)
    
  </td>
  <td> 
    
![Aquarencia Controls2](https://github.com/user-attachments/assets/e24c1d35-0a8d-4f6e-84d4-890c7d6552f7)
    
  </td>
</table>
