# Project _NAME_

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: _Dominic Apgar_
-   Section: _202.05_

## Simulation Design

_An attack on humans by the symbiotes! Find cures to free infected humans, use loud sound to stun the symbiotes, and most of all, survive!_

### Controls

-   _List all of the actions the player can have in your simulation_
    -   _Include how to preform each action ( keyboard, mouse, UI Input )_
    -   _Include what impact an action has in the simulation ( if is could be unclear )_

## _Black Symbiote_

_The most common enemy, able to be frozen in place through loud noise._

### Hunting

**Objective:** _Attack and infect the remaining humans._

#### Steering Behaviors

- _Seek_
    - _Nearest Human_
- Obstacles - _Walls, Fire_
- Seperation - _None_
   
#### State Transitions

- _When in range of a human._

### _Searching_

**Objective:** _Searching for Humans to infect. (Wander)_

#### Steering Behaviors

- _Separate, Wander_
- Obstacles - _Walls, Fire_
- Seperation - _Symbiotes_
   
#### State Transitions

- _When out of range of a Human_
   
## _Human_

_Hopes to survive against the symbiotes._

### _Flee_

**Objective:** _To escape the symbiotes grasp._

#### Steering Behaviors

- _Flee_
    - _Nearest Symbiote_
- Obstacles - _Walls_
- Seperation - _None_
   
#### State Transitions

- _When in range/being Hunted by a symbiote._
   
### _Idle_

**Objective:** _A small break from the chaos._

#### Steering Behaviors

- _Separate, Wander_
- Obstacles - _Walls_
- Seperation - _Humans_
   
#### State Transitions

- _When out of range of a symbiote._

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_

## Make it Your Own

- _List out what you added to your game to make it different for you_
- _Player agent_
    - _Controllable agent_
    - _Collect's cures to use on symbiotes_
    - _Can be infected, then swaps teams(?)_
- _Time-based (Survive 2mins to win etc.)_

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

