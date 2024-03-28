# Binding of Issac Remake

<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![LinkedIn][linkedin-shield]][linkedin-url]




<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/PaulKokhanov1/BindingofIssacRemake">
    <img src="https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/ba8440ae-6b1a-46b4-ab17-50452ff0718b" alt="Logo" width="280" height="200">
  </a>

<h3 align="center">Binding of Issac Remake</h3>

  <p align="center">
    Being that a huge goal of mine is to pursue game development, whether professionally or personally, I need to establish a strong fundamental basis. Thus, I made a goal to re-create one of my all-time favourite games "The Binding of Issac".
    <br />
    <a href="https://store.steampowered.com/app/113200/The_Binding_of_Isaac/"><strong>See the Game »</strong></a>
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#history">History</a></li>
    <li><a href="#about-my-implementation">About My Implementation</a></li>
    <li><a href="#obstacles-and-observations">Obstacles and Observations</a></li>
    <li><a href="#next-steps">Next Steps</a></li>
    <li><a href="#bugs">Bugs</a></li>
    <li><a href="#conclusion">Conclusion</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

## Important Note
Due to my barely passable drawing skills alot of the sprites for the re-creation were taken from the original game "The Binding of Issac: Rebirth". I used an online website that posted these extracted sprites, hence all creditation goes to those wonderful individuals 
who took the time to extract the various arts pieces of the game. https://www.spriters-resource.com/pc_computer/bindingofisaacrebirth/ 

<!-- ABOUT THE PROJECT -->
## About The Project
<div align="center">
  <a href="https://github.com/PaulKokhanov1/LunarLanderRL">
    

https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/954386dc-9e36-4cc6-b870-c5261d8ef890


  </a>
</div>

My desire was to evolve my understanding of game development and coding using C# by building from scratch the popular dungeon crawler game "The Binding of Issac"

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* [![CSharp][CSharp.com]][CSharp-url]
* [![Unity][Unity.com]][Unity-url]
* [![Gimp][Gimp.com]][Gimp-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- HISTORY -->
## History

To begin, lets briefly describe the game and the history of its creation. Binding of Issac is a roguelike (a style of game that traditionally involves a player going through procedurally generated levels and usually has permanent death of the player) video game. It is a top-down shooter that has the player going through various rooms within a level, defeating enemies and obtaining items and proceeding to the next level upon eliminating the boss of the level. 

The game originally created by Edmund McMillen (designer and story-creation) & Florian Himsl (programmer) was the results of a week-long game jam. Edmund is actually known for another title he designed called “Super Meat Boy”. The goal of the game was intended to dive into “touchy” subjects in regards to religion, hence McMillen had expected the game to fail. 

McMillien wanted to incorporate the procedural generation inspired by “The Legend of Zelda” game’s dungeon structure. 


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ABOUT MY IMPLEMENTATION -->
## About My Implementation

As this was my first take on a "large" project past a few week-long games and a couple game-jams, I intended to make use of clean coding architecture and proper organization to mimic similar workflows as I imagine would be found in the industry. Furthermore, as this was a remake I ensured to follow many of the key features of the game, this made for easier planning but many obstacles. 

### Scenes

Firstly, lets talk about the scenes found in the game. In my case, I used two scenes, the title screen and the main gameplay loop. The title screen allows the player to start a new game, edit a few options and quit the application. The main gameplay loop is where the player is allowed to play the game with the option of pausing using the “ESC” key to select between: Resuming the game, returning to the main menu or quitting the application.

### Key Features

Moving on to a few key features. To begin, I knew I needed to incorporate the procedural dungeon generation. I had explored various options and within the scripts you can find 3 iterations of algorithms I tried to generate the dungeons. I first attempted to use a backtracking algorithm called “Depth First Search”, this algorithm uses a grid to explore a path by choosing random “neighbors” at each point,

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/f7036aa5-41b6-47ea-9b33-c5ccec5d981e)

It continues to explore until it hits a point where all its neighbors have already been visited or cannot be visited (i.e a wall),

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/a3803a37-b7ed-4f7a-a00c-1e2dd069736e)

 and then starts to “backtrack” along its created path until it find a point that has un-visited neighbors. 

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/2d2f2b01-e4e2-49e3-b086-30d60e898991)

This continues until the algorithm eventually backtracks to its starting position. This did indeed create a maze like dungeon, however it did not capture the essence of how the binding of issac’s rooms are laid out. Instead of a singular path with a few dead ends, BOI (Binding of Issac) tends to have various branches that lead to dead ends in various directions. Hence, I thought to try and use a Breath First Search-esk approach. Breath First search ultimately explores outward instead of following one path until a dead end. Thus, I took this same concept and tried to apply it to the dungeon generation. Essentially, I had the algorithm begin in the center of the grid and explore a finite amount of steps in one direction until backtracking a finite amount of steps and exploring a different direction. Once again, this still did not feel correct as per the original game, so lost in though and scrolling through the internet I stumbled upon Florian’s personal explanation of the games logic https://www.youtube.com/playlist?list=PLm_aCwRGMyAdCkytm9sp2-X7jfEU9ccXm and Boristhebrave https://www.boristhebrave.com/2020/09/12/dungeon-generation-in-binding-of-isaac/ . Boris goes in depth talking about the method Florian used to implement the procedural generation of the dungeon. 

As a brief summary, the game is generated on a 9x8 grid, I adjusted to make it 10x10. The number of rooms is determined by the following formula:

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/0c6c55b0-4052-4c91-bbde-9a1f118c6749)

Assuming the 9x8 grid is labeled 0 -> 79, the starting room is always cell 35. The alogrithm creates a queue inputting cell 35 and then continously popping the left most element and looping over the 4 possible directions and it does the following:

•	Determine the neighbour cell by adding +10/-10/+1/-1 to the currency cell.

•	If the neighbour cell is already occupied, give up

•	If the neighbour cell itself has more than one filled neighbour, give up.

•	If we already have enough rooms, give up

•	Random 50% chance, give up

•	Otherwise, mark the neighbour cell as having a room in it, and add it to the queue.

If a cell does not add to its neighbours, it is added to a list of “dead ends” which are later used for special rooms, such as the boss room, treasure room, item shop etc...

For example, this is one of the outputs of the algorithm:

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/8c2bdb37-ddd1-4a29-833b-37ee0380dd65)

So as we can see this is very similar to a breadth first search approach, but different than my own original implementation.

In my case, as mentioned earlier I used a 10x10 grid and started our player at cell 45. I only incorporated the Boss and treasure room simply due to the fact that creating more layouts of rooms was not my goal in this project. I was more interested in learning to develop a game from scratch and the inner working of how this game was created.



### Camera Movement

The camera itself does not maintain the player in the center of its FOV at all times. Rather in the game it moves between rooms whenever the player enters that specific room. Thus, thinking about how to implement this logic, I considered two options:

1.	Create a BoxCollider2D on each door to initiate the movement of the camera
   
2.	Move the camera by constantly tracking the players position.
   
As you may have guessed option 1 is less memory intensive and overall an easier implementation. So obviously… I chose option 2. Why? I really don’t know. But regardless, I decided to normalize the players position to the respective cell number of each room and then whenever the player’s normalized position changed, so did the camera. I don’t think this is the best strategy as it requires me to continously observe the players position and compute a small calculation to determine which cell they are in, but it was an interesting approach rather than simply using an onTriggerEnter2D event.

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/620df8dd-ce73-4af9-beec-c858ff261dc8)



### Animation

Sigh… I really did not understand how tedious animation can be, and how time consuming. I limited my scope of creating characters and bosses due to how long the animations could take and then to tie them to the necessary logic of the player of enemy. I explored different options to create the animations. For example, the use of StateMachineBehavior classes in conjunction with the Animator. Such an implementation was used for my boss character since there were key states and state transitions that needed to happen in order to bring to life the boss. I could have simply used an Enum class to describe each of the states, but instead I found this method to work quite well for the case of my “boss battle”. For example, here is the simple Animator tree for my boss:

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/187d05f6-c329-4dbd-8d78-e56154be712c)

In the case of enemies, I instead created a base Enemy Controller class and created a public Enum class in order to handle state transitions, and for animation transitions I also used the animator tree however I did not create separate classes for each state.

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/308b296d-b2b6-4b39-8f19-94241a2675ea)

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/a7f0e5ef-2c79-4afd-9df4-e8f176caa917)




### Enemy AI

In the original game, I believe there is no “wandering” mechanic for the enemies. Though curious, I wanted to try out different states that our enemies could take on. Hence In my EnemyController class, the main states of a Melee Enemy were: Idle, Wander, Follow, Attack and Die

-	The Idle state is used when the player is not in the same rooms as the enemy, preventing them from moving
  
-	The Wander state is activated if a player is out of the enemies “viewing” range and thus the enemy chooses random directions to walk in
  
-	The Follow state actives once the player is within the enemies “viewing” range and they track the player
  
-	The Attack state is activated once the player is within attacking range of the enemy and this causes the enemy to deal damage to the player
  
-	The Die state is activated once the player deals enough damage to the enemy and defeats them, causing their game Object to be destroyed.

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/bd8bec40-d514-4afa-9c8d-4ad8d1ce8bfb)



### Enemy Spawning

In the original game, there were many different types of room layouts specifically designed by McMillen to his liking. Being limited on time I instead opted for random enemy and object spawning. To do this I created a grid for each room type that I had (Normal Room, Treasure Room and Boss Room) and then using scriptable objects for each enemy type I instantiated them within each room by randomly selecting the amount of this type of enemy to spawn between a pre-determined min and max amount. After instantiating one of the enemies, that cell, within the grid becomes “unavailable” so that no more enemies can be spawn in that location. This made for easier integration of enemy and object spawning using the same grid so that no enemies would get spawned and stuck on the same cell as an object like a rock.

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/c54c6122-4ac0-4c08-9771-f0f1ed1cf061)

![image](https://github.com/PaulKokhanov1/BindingofIssacRemake/assets/69466838/034c0f56-2c5c-4ca3-b4ce-2a460600666e)


### Project Architecture

Not knowing how the creators decided to organize their project (probably pretty well considering their knowledge of game development) I really wanted to ensure I did everything to follow my own established rules within the organization of the project. To begin, I organized my hierarchy to various subsets of possibly Game Objects, specifically, Managers, Setup, Environment, Canvases & Systems

-	Managers,
    -	Scene specific classes, i.e flow of the game
 	
-	Setup
    -	Camera, Lights, post-processing volume, Event System
-	Environment
    -	Spawning terrain, trees, tiles
-	Canvases
    -	Where you put Canvases, canvases are where all the UI goes
-	Systems
    -	Objects in Scene that use “Don’t Destroy on Load”
    -	Audio System
    -	Resource System
        -	Used for references to scriptable objects


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- OBSTACLES AND OBSERVATIONS -->
## Obstacles and Observations



### Enemies phasing through Walls


### Item Animations


### Less than minimum number of Rooms Created



<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- NEXT STEPS -->
## Next Steps


### The Minimap

### More Items, Objects and Enemies

### Rooms


<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- BUGS -->
## Bugs

### Player getting stuck between rooms

### Tears immediately being destroyed when shooting when player is at the top wall


<!-- CONCLUSION -->
## Conclusion


<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

Paul Kokhanov - [Website](https://paulkokhanov.com/) - paul.kokhanov@gmail.com

Project Link: [https://github.com/PaulKokhanov1/BindingofIssacRemake](https://github.com/PaulKokhanov1/BindingofIssacRemake)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Spriters Resource](https://www.spriters-resource.com/pc_computer/bindingofisaacrebirth/)
* [The Binding of Issac](https://store.steampowered.com/app/113200/The_Binding_of_Isaac/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/github_username/repo_name.svg?style=for-the-badge
[contributors-url]: https://github.com/github_username/repo_name/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/github_username/repo_name.svg?style=for-the-badge
[forks-url]: https://github.com/github_username/repo_name/network/members
[stars-shield]: https://img.shields.io/github/stars/github_username/repo_name.svg?style=for-the-badge
[stars-url]: https://github.com/github_username/repo_name/stargazers
[issues-shield]: https://img.shields.io/github/issues/github_username/repo_name.svg?style=for-the-badge
[issues-url]: https://github.com/github_username/repo_name/issues
[license-shield]: https://img.shields.io/github/license/github_username/repo_name.svg?style=for-the-badge
[license-url]: https://github.com/github_username/repo_name/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/paulkokhanov
[product-screenshot]: https://github.com/PaulKokhanov1/LunarLanderRL/assets/69466838/6ed735d2-09d7-42f3-bfe3-01473ff121c9
[product-screenshot-original]:https://github.com/PaulKokhanov1/LunarLanderRL/assets/69466838/efdba43a-cc5b-4a62-b447-c9cd2fac6ba2

[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[CSharp.com]: https://img.shields.io/badge/C%23-%23512BD4?style=for-the-badge&logo=csharp&logoColor=white
[CSharp-url]: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit
[Unity.com]: https://img.shields.io/badge/unity-0769AD?style=for-the-badge&logo=unity&logoColor=white
[Unity-url]: https://unity.com/
[Gimp.com]: https://img.shields.io/badge/GIMP-FFE01B?style=for-the-badge&logo=gimp&logoColor=%235C5543&color=FFE01B
[Gimp-url]: https://www.gimp.org/
