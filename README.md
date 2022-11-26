# Project _Symbiote Invasion_

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: _Dominic Apgar_
-   Section: _202.05_

## Simulation Design

_An attack on humans by the symbiotes! Find cures to free infected humans, use loud sound to stun the symbiotes, and most of all, survive!_

### Controls

-   _Movement_
    -   _WASD keys / Arrow keys_
-   _Use Antidote_
    -   _Left Click_
    -   _Cures symbiotes, returning them to humans_

## _Symbiote_

_The most common enemy, luckily they cannot harm you if you are in a hazmat suit._

### Infected

**Objective:** _Attack and infect the remaining humans._

#### Steering Behaviors

- _Seek_
    - _Nearest Human_
- Obstacles - _Walls, Obstacles_
- Seperation - _Other Symbiotes_
   
#### State Transitions

- _After transitioning to a symbiote._

### _Transitioning_

**Objective:** _Transitioning to an infected (Frozen for x time)_

#### Steering Behaviors

- Separate
- Obstacles - _Walls, Obstacles_
- Separation - _Symbiotes, Humans_
   
#### State Transitions

- _When first infected by a symbiote._
   
### _Human_

_Hopes to survive against the symbiotes._

**Objective:** _To flee away and survive the symbiotes._

#### Steering Behaviors

- _Flee_
    - _Nearest Symbiote_
- Obstacles - _Walls_
- Separation - _Humans_
   
#### State Transitions

- _When either cured or by default._
   
### _Curing_

**Objective:** _Similar to transitioning, but returns symbiote into humans._

#### Steering Behaviors

- _Separate_
- Obstacles - _Walls, Obstacles_
- Separation - _Humans, Symbiotes_
   
#### State Transitions

- _When hit with an antidote._

## Sources

-   _Assets custom made by myself, Dominic Apgar_

## Make it Your Own

- _List out what you added to your game to make it different for you_
- _Player agent_
    - _Controllable agent_
    - _Collect's cures to use on symbiotes_
- _Time-based (Survive 2mins to win etc.)_
- _Custom assets/art_
- _Twist on zombie-like survival_
- _Custom steering behaviors_

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

