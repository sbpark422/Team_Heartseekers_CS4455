Team: Heartseekers
Game: Spark

<Start scene file> 
CharacterCustomization

<How to play>
[BASIC] Customize your avatar using sliders (e.g., Skin Color, Hair style, Pants color etc.), then click “Create Character” when you are satisfied with your customizations. The user will navigate to the world where they can move around using WASD or arrow keys. In this world, users can meet other romance NPCs and try to talk to one of them. When users get closer to them, a conversational dialogue will appear. Use your mouse scroller to answer their questions. Based on the user’s choices it will lead the user to different challenges (new Unity Scenes, e.g., itemCollection, Tag, Hide & Seek, Puzzle). As the user successfully completes the challenge, a relationship strength (slider) with the NPC will increase. Users can press the “Backspace” key during challenges to go back to the world where they first meet their loved NPC. Users can reach out to other NPCs to start a conversation.

[Challenge 01: ITEM COLLECTION GAMEPLAY] 
When the user is loaded to the item collection challenge. You will see an inventory with items that the user needs to collect in the lower left of the screen. Start to find the items by moving around and colliding with the item. As the user collects the right objects, the inventory will be filled up indicating which ones the user has collected and which ones the user has yet to collect (grayed out). The relationship meter in the top right of the screen also fills up each time the right object is collected. When the user collects all the correct objects, the inventory is full and the relationship meter is maxed. 

[Challenge 02: TAG GAMEPLAY] 
When the user is loaded into the tag minigame, you will see a start button at the top center of the screen. After pressing start, the timer will count down and the button text changes to stop. If you press stop, the minigame pauses so the npc will stop moving, the timer will pause, and you will not be able to move.  The timer in the upper right hand corner indicates how much time is left. Run away from the npc for the entire duration of the timer to win the game. If the npc catches you before time is up, you lose. You can press the button after winning or losing to reset the minigame to play again. There is a speed buff (yellow sphere) in the upper left and lower right of the play field. Picking up the speed buff will boost your movement speed for a short period of time.

[Challenge 03: HIDE & SEEK GAMEPLAY] 
When the user is loaded into the hide and seek minigame, there is the same button and timer mechanic as tag. However, the win condition is to find the hiding npc before the timer runs out. The npc will be disguised as a prop (ie crate, barrel). If you are near a prop, a bubble will appear above the prop and you can interact with it by pressing F. If the prop is actually the npc, the npc will reveal itself and you win. If the prop is just a normal prop, then a message will display telling you it’s just a normal prop.

[Challenge 04: PUZZLE GAMEPLAY] 
Once the player is loaded into the tile-matching puzzle, the player must control the character to push the sphere around onto the tiles. There exists a “solution” path of correct tiles on which the ball must be rolled. Incorrect tiles are light up in red color, and correct tiles will light up from white to green color if the user manages to roll the sphere on the tiles. The win condition is to maneuver the ball through the puzzle in the correct order, memorizing the path taken.

<What parts to observe technology requirements>
Realtime character design with Animation: CharacterMovement
AI Interaction, path planning: tagDemo
Physics simulation with interactive objects: patternMatching
Interesting choices to make: CharacterMovement

<Individual Parts>
Dennis Crawford Contribution
Assets:
Customization Script
CharacterInput Script 
PlayerControl Script
CameraController Script
CharacterCustomization Scene
CharacterMovement Scene (minus NPCs)
Animations folder (Animator is custom and animations are from mixamo)
Distant Lands Pack folder (Asset Store)
Used mixamo animations and Distant Lands mesh pack from Asset Store to add player animations and movement as well as create a character customization script to allow for options such as gender, hair color, skin color, clothing color
Added camera controller to follow player around from top down perspective
Known Problems- Need to get animations working with root motion, 8 directional movement with root motion

Sahar Ali Contribution
Added a slider called relationshipslider
Added a heart image to the slider to signify that it is a relationship bar
Disables isInteractable so that the slider value cannot be changed by the user and is only updated from the logic
Added a script that updates the slider position based on a variable value called relationshipStrength
Relationshipstrength currently is not being updated since this was done before the implementation of the games
To increase strength when the player wins a game, we simply will need to call the function updaterelationshipstrength 

Seok Jin Hong Contribution
Assets:
PhysicsMaterial
Sphere
tileScript
Tiles prefab
Tiles
Red material
Green Material
patternMatching scene
Added new puzzleMatching Scene
Created tiles prefab for tile-matching puzzle
Created sphere to be pushed by player and rolled across tiles
Created physics material for ball, and adjusted friction to be more natural
Created color materials for tiles
Added script to control tile pattern recognition, adding “correct” tile path to HashSet and created logic to determine whether player is on correct tile upon collision
OnTriggerEnter detects whether player or ball is on tile, then verifies ball is on correct tile, and changes material color of tile to be either green or red
Note: Currently using HashSet and conditional statements to handle pattern recognition for puzzle, but plan on matching tiles to a 2-D array matrix to allow for randomization in future implementations

Nancy Zhang Contribution
Implemented tag minigame, and hide and seek minigame
tagDemo and hideNSeek scene
Scripts under Assets/Scripts/TagHideNSeek folder
GameButton, GameOverCanvas, HideAndSeekController, HideNPC, IGameMethods, PropDialogue, SeekManager, SetActiveBuffs, SpeedBuff, TagControl, TagNPC, Timer
Prefabs under Assets/Prefabs/TagHideNSeek folder
Medfan Barrels and Boxes (from Asset Store)

Soo Bin Park contribution
Assets:
itemCollection Script
NPC Script
DialogueManager Script
CameraForChallenge Script
A Portion of other scripts to incorporate all the team member scenes together
NPC related prefabs, Dialogue related contents (dialogue choices),  
3rd party unity assets for BGMs: CasualGameBGM05 (https://assetstore.unity.com/packages/audio/music/casual-game-bgm-5-135943 )
Implemented DialogueManager script to allow users to talk to other NPCs and choose answer choices. 
Edited scripts to load different scenes of challenges according to the chosen dialogue. Incorporated and connected all the scenes together as a working alpha system.
Created itemCollection challenge scene to allow users to pick up items using the inventory.
Incorporated RelationMeter to add interactivity to the itemCollection scene. (increase the value as the user collects the item)
Added temporal a hotkey cheat for alpha video (used backspace to go back to the world from Challenge scene)
Added audio source to use as a bgm for CharacterCusmization & Movement scene.


