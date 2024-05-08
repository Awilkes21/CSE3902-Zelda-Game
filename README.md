# CSE3902 Interactive Systems Project
##### Authors: Nick Johnson, Will Blanton, Nathan Rogers, Andrew Wilkes, Evan Skripac
##### Date: December 7, 2022

## Controls:
### Player controls
* W - move Link up, make Link face up
* Up arrow - move Link up, make Link face up
* A - move Link left, make Link face left
* Left arrow - move Link left, make Link face left
* S - move Link down, make Link face down
* Down arrow - move Link down, make Link face down
* D - move Link right, make Link face right
* Right arrow - move Link right, make Link face right
* Z - make Link use his primary attack
* N - make Link use his primary attack
* X - make Link use selected ability
* C - Change Link's primary attack method
* K - Move to previous room
* L - Move to next room
* Left Click - Move to previous room
* Right Click - Move to next room
* B - Cycle Link's ability in Inventory Screen
* Enter - Toggle Inventory screen
* Left Alt - Save Game
* Right Alt - Load Game

### Other controls
* G - spawn Bow to test item pickup
* Q - quit game
* R - reset the program to its initial state
* M - mute sound
* Escape - pause game

### Cheat Codes
* IVY - Health boost
* HUT - Extra bombs
* PIE - Extra rupees
* TOP - Unlocks the map
* UTE - Unlocks the compass

### GamePad Controls
* Left Thumbstick Up - move Link up, make Link face up
* Left Thumbstick Left - move Link left, make Link face left
* Left Thumbstick Down - move Link down, make Link face down
* Left Thumbstick Right - move Link right,make Link face right
* Y - Use Link’s secondary ability
* A - make Link attack with sword
* Right Shoulder - move to next room
* Left Shoudler - move to previous room
* Back- quit game
* Start - reset game

## Refactorings
* Implemented physicsManager class to reduce enemy fields (Will)
* Implemented healthManager class to reduce enemy fields (Will)
* Made CollidablesList in Collision manager static (Will)
* Removed game1 parameter from ICollidable Update method (Will)
* Made single static rand for enemies (Will)
* Split classes directory into multiple directories (e.g. managers, configs) (Will)
* Renamed Projectile class to physicsProjectile (Will)
* Implemented timer utility class (Will)
* Refactored abilities with multiple tiers into single class (Nathan)
* Changed inventory to use dictionary to keep track of ability unlocks (Nathan)

## Sprint 5 Features
* Procedural room generation - Enter the procedurally generated area through the bottom door in the spawn room.
* Cheat codes - Unlock by pressing the keys specified in the Cheat Codes section simultaneously

## Known Bugs
* HUD should be at top of screen (at least it is in the actual game)
* Player can shoot an arrow during room transition (fix in transition state refactor
* Player sometimes disappears when moving between rooms while damaged (or allow player to move around screen during transition)
* Room transitions sometimes spawn player in tile
* Boomerang should be able to fly over "water" tiles in certain rooms
* Boomerang should return to player, not just initial position
* Map in inventory screen does not show correct doorways
* HUD not synced with some inventory items
* Map currently set to always display on HUD instead of when item is found
* Vertical doors spawn you in front of door on opposite side of the room or push you through it.
* Player can move during transition screen (still need to refactor into gameState)
* Cannot currently distinguish between types of doors. Need to add classes for door types (e.g. locked door, bomb door)
* Enemies and player can walk through some walls in some rooms (primarily bottom right, but also sometimes top right)
* Enemies do not drop items
* Top doors in procedurally generated have hitbox moved up and to the left
* Room does not save/load correctly
* Wand projectile does not collide with tiles
