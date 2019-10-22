Team name: Out-Of-Bounds Exception
Team members: Michael Hayes, Shichao (Michael) Liang, Jiyoung Lim, Elmer Gonzalez, Jack McCormack
Game name: Toy souls

1.	Summary of game
	The goal of Toy Souls is to defeat an evil toy boss. At the beginning of the game, player starts with basic moving ability and attack skills. These are not enough to beat the final boss, so a player is to explore the map to enhance its ability. A player can upgrade their attack power by using collected souls and acquire new abilities by solving puzzles. 

2.	Player control
	* Character movement
		- Move forward: W	
		- Move left: A			
		- Move right: D		
		- Move backward: S	
		- Run forward: Shift + W or scroll wheel + W
		- Run left: Shift + A or scroll wheel + A
		- Run right: Shift + D or scroll wheel + D
		- Run backward: Shift + S or scroll wheel + S
		- Item usage: Q
	* Attack
		- Normal attack: left click
		- Thrust attack: right click
	* Special Ability (acquired later in the game by solving puzzles)
		- Dodge roll: C

	* Xbox Controller is also supported (recommended!)

	Tech requirements completed: Player control uses multi-layer animator with an override layer for attacks, item usage, etc.  Camera movement allows for locking on of targets, but this isn't added in the Alpha, as we have yet to figure out a way to know which enemy is closest on-screen to the player.
	Root motion for attacks, disabled for movement.
	Rolling animation was floaty using the blend tree, so an animation curve was made for the roll animation to follow.  Parameterized so that roll_speed effects the distance rolled.  If the player isn't movement, roll is a back-step.

3.	Enemies AI
	Enemies implemented for the Alpha are teddy bears (mobs) and Luigi (Boss). Enemies patrolling area will start chasing and attacking a player, when it gets close enough. A player can attack and defeat enemies and collect their souls. When the player is defeat by enemies, the game reset. 

4.	Puzzle
	A puzzle implemented for the Alpha is located at in front of flipped toy car. It consists of five rectangular platforms on the ground and is needed to be stepped in the right order to solve it (4,2,5,3,1 from the left). Completing it unlocks a new ability, dodge rolling. 

5.	Menu
	A player can pause and access to menu by pressing ESC. In menu, a player can resume, start a new game, and go back to main menu.  