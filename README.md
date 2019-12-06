# Austin Anderson and Tim Trimble VR Final Project
This is our final project repository for the Fall 2019 CS 5963 class.

Our game, Explosions in the Sky, is a game developed for the Oculus Rift with Touch Controllers. It's a simple RTS developed with the idea in mind of creating a new perspective to witness RTS like games from, one that evokes reality and awe rather than a perspective purpsoed exclusively for strategy.

### Running the game. ###

1. This game was developed using OVR (Open VR inputs) on an oculus rift with touch controllers. We have tested it on the valve index's present in the VR Classroom and found that the knuckle controllers don't seem to be responseive to OVR input. So, to guarantee this game's functionality, please use with an oculus device with touch controllers.

2. Not required, but the game is designed to be played sitting in a reclining chair or laying down on a beanbag chair or some sort. This is *purposeful*. The comfort of someone playing the game sitting straight up is not something we developed for, and so it should be taken into account that any discomfort experienced while playing this way is due to playing in a non-reccomended fashion.

3. Note: It seems that, due to something with Steam VR input overides, the A and X buttons on oculus touch controllers are sometimes unreliable in their functionality, whiich will cause issues with filtering controls in the game. We have tried to program around this, but please try to run the game without running Steam VR if possible. 

###Playing this game.###

####Controls:####

To select units, point at the sky with the controller, pull the index trigger, and drag across any blue (friendly) units that you want to select. After that, release the trigger, and those units are now selected.

To direct units to move to a given location, simply point at that location AFTER selecting units, and pull the hand trigger (the lower trigger).

To filter out units during selection, use the A/X and B/Y face buttons. Pressing any of these face buttons will toggle on/off the unit filtering, according to the following guid:

Default: When no filters are selected, a click and drag selection will select all units that the box covers. This box will be purple, and is the default. 

A/X: When you press A/X (without other filters toggled on), it will toggle the small unit selection. When your selection box is green, that means that the only units you will select when click-drag selecting are small type units. 

B/Y: When you press B/Y (without other filters toggled on), it will toggle the medium unit selection. When your selection box is yellow, that means that the only units you will select when click-drag selecting are medium type units. 

A+B/X+Y: If you toggle both filters on by pressing A and then B or X and then Y, it will toggle the large unit selection. When your selection box is blue, that means that the only units you will select when click-drag selecting are large type units. 

### Project Goals###

Our project had the following as it's stated goals in the project proposal:

To create an RTS game experimenting with a new perspective on RTS gameplay

To create a new control method for RTS games in VR, focused on evoking awe through controls instead of being the most tactically sound control methods.

To do so through implementing a simple RTS game revolving around a low-orbit space battle taking place above the player.

To create a fun experience focused around the joy of controls over the nitty gritty tactics most often found in RTS games. 

####Granular project goals:####

Click and drag control Scheme:
This control scheme, as described in our project proposal, revolves around being able to click and drag to select units (like clicking and dragging with a mouse on a desktop). The purpose of this was to create more natural, free flowing controls for directing units.

Unit filtering controls:
The unit filtering controls were a simple way to provide the player with the ability to select unit types while doing a click and drag selection.

Unit Movement:
We made the goal of having collision aware movement through the space for all combat units. 

Simple AI: 
We had the goal of creating a simple but effective AI to create challenge for the player.

VR ground persepctive: 
Another goal was to create a different perspective for viewing the battlefield. Instead of a godlike view (e.g. Starcraft 2), provide a more realistic, humanity oriented view of what an epic battle might look like. 

Fun RTS: 
We wanted to make the game at least somewhat enjoyable. It's a game. 

###Status of implementation:###

Click and drag control Scheme:
We succesfully fully implemented our click and drag control scheme into the game. It functions by holding down the index finger trigger, dragging across the sky over units that you've selected, and then releasing. After that, you use the hand trigger to point and click at where you want the units to go. The units use an attack move behavior, which means they move towwards the destination they've been given and attack anything along the way, without stopping. You can find this implementation in the InputManager script.

Unit Filtering Controls: 
We succesfullly implemented filtering controls on the face buttons of the touch controllers. The system works smoothly and reliably. This functionality can be found in the input manager scripts. 

Unit Movement:
Through using a Nav mesh, we were succesfully able to make smooth unit navigation that accounts for collisions with other units and avoids them. Just check the Navmaesh if you need any clarification in implementation. 

Simple AI: 
We created a simple flocking AI to compete against the player. While certainly nowhere near a high-caliber quality AI (e.g. Starcraft 2), the AI functions well enough to create meaningful challenge for the player and require them to actively consider their actions. Check the game manager for details. 

VR Ground Perspective: 
We did fully impelement this (by placing the camera on the ground). We're also going to be presenting the demo with a beanbag chair for player's to sit in, so they can comfortably look upwards. The game is reccomended to be played in this manner.

Fun RTS:
This is obviously subjective, and the game has a lot of things left we could iterate on to make it a better RTS game, but the core experience is something that we (Austin and Tim) find fun and engaging. We definitely see a future for this game if we choose to continue developing it. 

