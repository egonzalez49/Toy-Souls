Team Name: Out-of-Bounds Exception

Team Members:
	Elmer Gonzalez, egonzalez49@gatech.edu, egonzalez49
	Shichao Liang, sliang76@gatech.edu, sliang76
	Jiyoung Lim, jlim335@gatech.edu, jlim335
	James McCormack, jmccormack30@gatech.edu, jmccormack30
	Michael Hayes, mhayes64@gatech.edu, mhayes64

Installation Requirements:
	Unpack “OOBException_ToysSouls.7z”.  Navigate to “Build” folder, and run ToysSouls.exe

Gameplay Instructions:
	The goal of Toy Souls is to defeat an evil toy boss. At the beginning of the game,
	player starts with basic moving ability and attack skills. These are not enough to
	beat the final boss, so a player is to explore the map to enhance its ability.
	A player can upgrade their attack power by using collected souls and acquire new
	abilities by solving puzzles.   In addition, there are items hidden throughout the map. 

	To play, upon game load, hit “New Game”.

	Controls are listed below:

	Xbox Controller Preferred: 

	- Camera Controls - Right Stick
	- Player Movement - Left Stick
	- Run/Sprint - Click Left Stick
	- RB - 1H/2H Attack 1
	- RT - 1/H/2H Attack 2
	- LB - 1H/2H Attack 3
	- Y - Swap to 1H/2H Stance
	- B - Dodge/Roll
	- X - Use Healing Potion
	- A - Interact
	- Start - Pause Menu
	- Options - Button Mappings

Rubric Requirements:

For the following section, for each rubric item, please see instructions
on how to see evidence of rubric row.

<<3D Game Feel>>
- Clearly defined, achievable goal:
	See story screen at the beginning of the game.
- Communication of success/failure: 
	Die to an enemy or boss. There will be a failure screen. 
	Defeat the boss. There will be a success screen.
- Start menu: 
	Load the game to see start menu with options. 
- Able to reset upon success/failure: 
	Die or win to access options menu.. Select “New Game”.
- Not an FPS: 
	Our game is a third-person action rpg (not a shooter)

<<Fun Precursors>>
-  Goals/Subgoals communicated to player:
	- To see main goal, read story splash on the main screen. 
	- To see upgrade subgoals, go to pig NPC. 
	- To see dodge/roll upgrade objectives, go to the sheep NPC. 
- Provide interesting choices to player: In game, you can choose: 
	- Grinding time to get more powerful.
	- How much resources to devote to getting health potions versus upgrade
	- When to challenge the boss.
- Must have consequences:
	- To see death consequence, die to see that you lose all souls and must restart. 
	- To see resource management consequences, spend their souls on potions if they take too much damage or spend souls on upgrades.
	- To see aggression consequence, hit the boss 4 times.  You will trigger an end-game fight that is inescapable.
-  Player choices engage with the game: 
	If the player decides to fight the boss, they trigger the final boss sequence. 
	If they decide to instead level up, they can buy sword upgrades and potions for when they deal with the final boss. 
	Choosing to explore also grants them possibility of finding chests with loot.
- Avoid Hollow/Obvious/Uninformed decisions: 
	One of the player’s main decisions is whether they are prepared to take on the final boss. 
	This choice comes down to the player determining if they are ready and confident in their abilities and will require the player to be well informed on their abilities.
-  Avoid Fun Killers: 
	Nowhere in the game is the player killed by a seemingly invisible object (in the final boss scene, a player can jump off and die, but that is due to their own mistake).
	No one-shot mechanics. 
	Enemy respawns: Limiting player farming capability is anti-fun.  Notice that in our game, NPCs will continuously spawn.
- Achieve balance or resources, strategies, etc.: 
	Everyone finds fun in different elements. According to various psychographic profiles.
	1. Spectacle - This player likes the spectacle, so getting a bigger sword will appeal to this player.
	2. Creative - This player wants to find creative ways to play the game. Puzzles appeal to this player.
	3. Competitive - This player wants to defeat the boss as quickly and efficiently as possible. For this player, finding the right balance of killing enemies to upgrade vs. time investment is key.
- Reward successes and threat of punishment for failure :Explore the map: 
	-Find the bucket to enable the dodge/roll puzzle.
	-Find a reward by exploring the map. 
	-Hit multiple enemies at a time to more efficiently farm souls.  However, this runs the risk of getting swarmed.  
	-If the player wants to fight the boss, he can do so but will need to dodge his attacks that are difficult to do so without acquiring the roll ability.
- In-game learning/training opportunities: 
	Upon level load, you start on a bed, where you can try out the buttons in relative safety.  
	However, too much time here will spawn more enemies below.  
	This is a Souls-like game, so tutorials are very sparse.  
	The goal is to find out how the world works.
- Appropriate progression of difficulty: 
	The game is a set level of difficulty at the beginning, but you experience a downward progression of difficulty as you get more powerful, as that’s the power-fantasy inherent to most action RPG games.  
	There is a spike in difficulty as the player transitions to the boss arena.
- Avoid opportunities for player to trivialize game: 
	There is a cap on the number of sword upgrades so that the player can not become “too strong”.

<<3D Character>> 
- Character control is predominant part of game: 
	Your main objective is to use the player.  
	You do not control anything else, with the exception of menus.
- No Unity tutorial characters: 
	No tutorial characters used.
- Configure animation control and input processing: 
	Use an Xbox controller for the game.  
- Utilize a character with engaging animations: 
	There are a variety of animations
	3 one-handed attacks
	2 two-handed attacks
	A drinking potion animation
	Dodge/roll animations.  
	Walking and running are blended for different states.
	Try these out according to game controls, which can be accessed with the left Options button on the Xbox Controller.
	You cannot animation cancel out of an attack, so attacks have consequences.
	Try out the difference in times between one-handed and two-handed attacks.  
	Try out running.
	Try out dodge/roll when standing.
	Try out dodge/roll when walking/running.
- Has analog control of character majority of the time: 
	Uses Xbox controller.
- No difficult button mappings:
	Xbox mappings are taken from other Souls-like games, and are shown in-game.
- Fluid animation: 
	Try out the animations.  Turning is very Dark Souls-like, so it’s very fast.
- Humanoid players don’t slide or moonwalk: 
	Root motion is enabled for all attack and stationary animations.
- Low latency responsiveness: 
	Test out pressing attack button and the animation.
- Camera is smooth, always shows player: 
	3rd person camera always follows the player, and there is physics interpolation to move towards the player.
- Camera has limited passing through walls.: 
	Arena walls become transparent to allow the player to see appropriately.  To test, go to a wall, and then turn the player camera around.
- Auditory feedback on character state and actions: 
	Player swings, drinking potion, and getting hit by ememes makes corresponding sounds.
- Coupling with physics simulations via animation curves, callback events, IK adjustments to animations: 
	Player animations use blending and finite state machines.
	Player dodge roll uses custom anim curve to prevent sliding motion at the end of the animation.

<<3D World>>
- Synthesized a new environment: 
	New environment is a bedroom.
- Graphically and auditorily represent physical interactions: 
	There is a swinging door with sounds.
	Swing at destructible boxes with sounds.
	Swing at enemies.
	Pickup the bucket.
	Walk over puzzle elements.
- Graphics aligned with physics representations (no clipping): 
	There is no clipping of objects that we can see.
- Appropriate boundaries: 
	Walls prevent movement out of arena.
	In final arena, falling off encounters a death zone.
- Variety of environmental physical interactions (Scripted object interactions, Physics rigid body objects, Animated objects using Mecanim, State changing or destroyable objects): 
	Acquire the bucket, the bucket should disappear. 
	Acquire the bucket, and then talk to the Sheep NPC, fences disappear.
	Inside the fenced off area, run over the puzzles to see them change color. 
	Find the gate, and run through to see it swing. 
	There are destructible crates throughout the level, including a wall of crates.  Hit them with swords to break them.  
	Otherwise, kite the boss towards them, and the enemies will break them too. Find a chest and interact with it to open them. 
- 3D simulation with six degrees of freedom movement: 
	There are 3 degrees of translational movement, and 3 for rotation.  
	Red Ted Mage Bears will turn towards player and shoot fireballs.
- Interactive environment: 
	A puzzle is enclosed by a fence. A player is required to complete a quest to remove the fence and access to the puzzle. 
	A bucket can be collected by a player to complete the quest. 
	Breakable boxes wall off areas.  Can be broken by player or 
- Consistent spatial simulation: 
	No obvious glitches in physics that we can see.

<<NPC/AI>>
- Multiple AI states of behavior: 
	Teddy Bear enemies display a number of different states: wandering, idle, chasing the player, and attack.
	Red Ted enemies have additional state of a ranged attack, but will aggro on low range.
	The boss Boss has a chase player state and an attacking state.
-Smooth/steering locomotion: 
	Unity’s NavMesh provides a lot of the functionality for this piece, but we additionally programmed some steering to make improve the AI polish. 
	For example, there is some additional spherical interpolation applied during attacking moves.
	Additionally, the Teddy Bear enemy agents “wander” by calculating a random point somewhere in a large area of the player; that way the Teddy Bear agents somewhat path towards the player, but not on a direct path towards the player. 
- Root motion:
	Root motions is applied to all enemy AI animations. To see, kite enemies at different ranges to see animations.
- Effective and believable AI: 
	Animation polish combined with proper/reasonable condition testing that dictates transitions between states makes the AI behavior effective and fun.  
	Some examples of this “condition testing” is: we’ve programmed an attack range (which is the distance between the agent and the player) tailored to the size of the AI enemy agent before the agent changes to an attacking state, and the agent must also be facing the correct direction before starting an attack (this is computed using the dot product between the forward vector of the agent and the distance vector between the agent and the player)
- Fluid animation: 
	Animations were individually created for each enemy using Mixamo, and in some cases are blended together to create a smooth transition between different animations. 
	As previously mentioned, some animations also use some programmatic interpolation to ameliorate any unrealistic movements.
- Sensory feedback of AI state(animation) :
	Each state for both the Teddy Bear enemies and the Boss has an individual animation attached to it, so visually distinguishing between the different states of the AI enemies is obvious.
- Difficulty of engagement is appropriate: 
	Engagement with Teddy Bear enemies is a good difficulty.
	Attacks are telegraphed.
	With strategic movement and timing the player can defeat the enemies more easily. 
	Try timing your attacks correctly during the down time after the enemy attacks. 
- AI interacts with and takes advantage of the environment: 
	During the final boss scene, the boss will have a state where he grows big and stomps out the outer edges of the map shrinking the total play area.  Try to bait out that attack.

<<Polish>>
- Start menu GUI: 
	Start menu GUI up on game load.
- Pause Menu:
	Pressing the Start Button on the controller.
- Feel like a game.: 
	It’s playable. Just play the game.
- No debug visible: 
	Debug console is toggled off in the final build
- Exit at anytime: 
	Player can pause the game and quit at any time
- No developer cheat modes: 
	No cheat modes in the game.
- GUI in right styles: 
	GUI style fits the aesthetic of the game.
- Transitions between scenes done aesthetically: 
	See the transition to the final stage. Involves cross-fade.
- Should include many of the following: Scripted aesthetic animations (swaying trees, grass, scurrying bugs under moved rocks, etc.), Proximity-based events (alien plants that retract if you get near, picture frame falls off wall, etc.), Surface effects, such as texture changes or decals (e.g. dentable surface, bullet holes, etc.), Particle effects (e.g. dust or splashes around footsteps), Auditory representation of every observable game event, Physics event-based feedback such as particle effects and one-shot audio for collisions of different types, Sonifications that map physical properties to audio effects. Example: Wheeled robot gear whine generated via volume attenuation and frequency shift of looped audio as a function of robot’s speed: 
	Whenever you hit an enemy or take damage, there are particles resembling cotton coming out of the stuffed animals or the player.
	Sound effects upon weapon swings and hits.  
	Grunts and other sounds upon damage.
- Unified art-style: 
	Every message box shares the same style
- Shading and lighting style:
	Every scene shares two directional lights.  One acts as a key light, the other as fill to dark shadows.
- Unified color palette: 
	The color palette inside the room is consistent with that of a normal bedroom. 
	No color is blatantly out of place, such as a pink table. 
	The UIs all have a unified background and colors for text. 
- Sound Theme: 
	The game has different sounds depending on the state of the game. 
	Once the player engages in the final boss scene, the music intensifies to signify the increased tension of the moment.
- No glitches: 
- No escaping confines of game: 
	The player is unable to escape the room they start in as their are four walls, a ceiling, and a floor that act as boundaries. 
	Once in the final boss scene, if the player leaves the area, they fall off and die.
- No obvious edge of the game world unless part of intended gameplay: 
	Since our game takes place in a bedroom, there is an observable edge in the world, mainly the walls of the room.
- No getting player stuck: 
	Player can not get stuck anywhere in the environment. 
	The player is not able to get anywhere they are not supposed to, or if they are on the chair in the scene for example, they can just jump down.
- Stable: The game runs stable and does not crash

Deficiencies or Known Bugs:
	- Rolling can cause clipping through small barriers.
	- Red Ted fireballs can sometimes disappear before they are meant to expire
	- Transition to final boss arena through knocking the player into the air might
	  not clip through properly.

External Resources:
	*ENVIRONMENTAL ASSETS*
	**Unity Store Assets**
		- Door Free Pack Aferar by Andrey Ferar
		- Low Poly Pack - Environment Lite by Solum Night
		- Interior Props Pack Asset by Reach the End
		- Realistic Furniture and Interior Props Pack by Sevastian Marevoy
		- HDRP Furniture Pack by Tridify
		- Bed and Bath Furniture Pack by Pepperjack
		- Wooden Floor Materials by Casual2d
		- Starfield Skybox by Pulsar Bytes
		- Western Props Pack by DevAssets
		- Wooden Fence Destruction by Inspectorj Sound Effect

	*NPC AND CHARACTER ASSETS*
	**Free3D.com Assets**
		- boxMan  by Athos
		- Morion Helmet V1 by Printable_Models
		- Breastplate Armor VerC V1 by Printable_Models
		- Greaves Armor verC by Printable_Models
		- Ted Bear by William_DSP
	**Turbosquid.com Assets**
		- Luigi 3d model by Giimann
	**Unity Store Assets**
		- RPG Swords! by Alex Lusth
		- 6 x 3D Cute Toy Models by Psionic Games
	**Adobe Assets**
		- Agent animations  by Adobe Mixamo

	*UI ASSETS*
	**Unity Store Assets**
		- Fantasy UI Elements by Ravenmore
	**Dafont.com Assets**
		- Optimum Precepts font by Manfred Klein

	*SOUND/MUSIC ASSETS*
	**Incompetch.com Assets**
		- Music by Kevin MacLeod
			"Come Play with Me"
			"Beauty Flow"
			"Prelude and Action"
			"Bassa Island Game Loop"
			"Glitter Blast"
			"Life of Riley"
			License: CC BY (http://creativecommons.org/licenses/by/4.0/)
	**Themushroomkingdom.net Assets**
		- “Crying #3”
		- “Power-up”
		- “Pipe travel / power-down”
	**Freesound.org Assets**
		- “Explosion_01” by tommccann
		- “Man getting hit” by Under7dude
		- “RussianMeteorite_SFX” by jongrubbs
		- “Booms” by studiomandragore

Work Breakdown:
	Game UI, Environments, and NPCs:
		- Elmer Gonzalez
	Enemy Design, AI, and Animations:
		- Michael C. Hayes
	Character Design and Animations:
		- Shichao Liang
	Environments:
		- Jiyoung Lim
	Puzzle Design:
		- James McCormack

What Scenes to Open in Unity:
	-TitleScreenScene





