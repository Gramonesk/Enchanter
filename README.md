<h1>Enchanter</h1>

![0709](https://github.com/Gramonesk/Enchanter/assets/154248035/41b20f66-24fe-476e-885b-ddab74e6b318)

## 🔴 About Project
  This was my personal project that was oriented to a Gacha Deck-Building Strategy type of genre. The game idea initially was made due to my superimposed imagination seemingly describing a fantasy story. The whole mechanic of this game was made as a mix of several games mechanics i played prior to this game. This project is still in further development for optimization and ideation.

## 📋 Project Info 
* Editor Version : Unity 2022.3.29f1

* My Contribution : Overall (Ideation, Design, Story, Mechanics, Art [Not Applied Yet] )

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
  
1. **Post Processing**
   - using shader graph and blooming to create the glow effect exclusively on the fire orbs
   - using color grading and white balance to make the visual look more engaging

3. **Object Oriented Programming**
    - using object oriented programming on the card datas, allowing open modifications for effects
    
4. **Event System**
    - using events, like delegates or scriptable objects to trigger events that would activate an effect
    
5. **Scriptable Objects**
    - using scriptable objects, as the main data to store the cards data and also their Effect Functionality improving overall memory management
    
6. **Particle System**
   - using a customly made particle system to create the visuals for the cards.

8. **AI / Enemy behaviour**
   - currently using a simple longest sequence searching with a DFS like approach by using recursion using the enemy's hand as data.
   - Future Plans:
       - using a procedural approach to determine cards that should be played as data
       - using pointing system to determine which targets should this card be used onto


<details>
  <summary>What i learned</summary>

- As the game was more data-structure oriented and also was the game made with personal interest, the development process took way longer which taught me a lot in terms of Game Designing and Program designing. In terms of program designing skills, i learned to utilize class diagram first beforehand; allowing a good code management and easier code execution as i don't need to think much further during the development process.
- Through the process, i learned to identify the base mechanics of the game. As an example, a notable game "Yu-Gi-OH" have effects mechanics such that they have a trigger effect like [On Draw] [On Discard] effects. These effects are modifications subsequently to the main effect of the base mechanics of the card.

| Detailed Explanation |
|----|
| As in the following problem, we would be thinking more and more into the implication of this type of unique implimentation. Assuming that we had started class designing and determining the base class, all of these couldve been done with using OOP (Object Oriented Programming) and event system implimentation. Even as a game programmer, knowing this would save me alot of times from disambiguation and deep consideration. |
 
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
