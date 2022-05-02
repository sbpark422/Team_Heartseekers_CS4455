# Team_Heartseekers_CS4455/CS6457
## Team: Heartseekers
### Game: Spark

Start scene file: CharacterCustomization <br/><br/>

[How to play]  <br/><br/>
[BASIC] Customize your avatar using sliders (e.g., Skin Color, Hair style, Pants color etc.), then click “Create Character” when you are satisfied with your customizations. The user will navigate to the world where they can move around using WASD or arrow keys. In this world, users can meet other romance NPCs and try to talk to one of them. When users get closer to them, a conversational dialogue will appear. Use your mouse scroller to answer their questions. Based on the user’s choices it will lead the user to different challenges (new Unity Scenes, e.g., itemCollection, Tag, Hide & Seek, Puzzle). As the user successfully completes the challenge, a relationship strength (slider) with the NPC will increase. Users can press the “Backspace” key during challenges to go back to the world where they first meet their loved NPC. Users can reach out to other NPCs to start a conversation.  <br/>

[Challenge 01: ITEM COLLECTION GAMEPLAY] <br/><br/>
When the user is loaded to the item collection challenge. You will see an inventory with items that the user needs to collect in the lower left of the screen. Start to find the items by moving around and colliding with the item. As the user collects the right objects, the inventory will be filled up indicating which ones the user has collected and which ones the user has yet to collect (grayed out). The relationship meter in the top right of the screen also fills up each time the right object is collected. When the user collects all the correct objects, the inventory is full and the relationship meter is maxed. <br/>

[Challenge 02: TAG GAMEPLAY] <br/><br/>
When the user is loaded into the tag minigame, you will see a start button at the top center of the screen. After pressing start, the timer will count down and the button text changes to stop. If you press stop, the minigame pauses so the npc will stop moving, the timer will pause, and you will not be able to move.  The timer in the upper right hand corner indicates how much time is left. Run away from the npc for the entire duration of the timer to win the game. If the npc catches you before time is up, you lose. You can press the button after winning or losing to reset the minigame to play again. There is a speed buff (yellow sphere) in the upper left and lower right of the play field. Picking up the speed buff will boost your movement speed for a short period of time.

[Challenge 03: HIDE & SEEK GAMEPLAY] <br/><br/>
When the user is loaded into the hide and seek minigame, there is the same button and timer mechanic as tag. However, the win condition is to find the hiding npc before the timer runs out. The npc will be disguised as a prop (ie crate, barrel). If you are near a prop, a bubble will appear above the prop and you can interact with it by pressing F. If the prop is actually the npc, the npc will reveal itself and you win. If the prop is just a normal prop, then a message will display telling you it’s just a normal prop.<br/>

[Challenge 04: PUZZLE GAMEPLAY] <br/><br/>
Once the player is loaded into the tile-matching puzzle, the player must control the character to push the sphere around onto the tiles. There exists a “solution” path of correct tiles on which the ball must be rolled. Incorrect tiles are light up in red color, and correct tiles will light up from white to green color if the user manages to roll the sphere on the tiles. The win condition is to maneuver the ball through the puzzle in the correct order, memorizing the path taken.<br/>

<What parts to observe technology requirements> <br/>
Realtime character design with Animation: CharacterMovement <br/>
AI Interaction, path planning: tagDemo <br/>
Physics simulation with interactive objects: patternMatching <br/>
Interesting choices to make: CharacterMovement



