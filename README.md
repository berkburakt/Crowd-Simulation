# Crowd-Simulation
  
## Instructions  
  
+ Press **Space** to stop crowd's walking  
+ Press **S** to start crowd's walking  
+ Press **Up-Arrow** to rotate crowd towards you  
+ Press **Down-Arrow** to rotate crowd opposite of you  
+ Press **Right-Arrow** to increase crowd's height  
+ Press **Left-Arrow** to decrease crowd's height  
+ Prees **W** to increase crowd's weight  
+ Press **Q** to decrease crowd's weight

## How to conduct the experiment

### 1. Main Menu
  + Enter details
    1. Participant's height
    2. Participant's gender
    3. Assign participant to a group
    4. Confirm details and click OK
### 2.	Training Room
### 3.	Experiment Stage 1: Pre-test
  +	Participant is asked to modify the avatar to its own body shape with the use of the controller – 10 times 
  + Participant is asked to modify the avatar to their dream body shape with the use of the controller – 10 times 
### 4.	Experiment Stage 2: Manipulation stage
  +	According to the participant’s group (1 or 2), all the other avatars in the environment will have a type of body shape (skinny or bulky) respectively.
  + Find the apple game: Engagement
    1.	The participant is asked to find the avatar in the environment which has an apple on his/her head
    2.	The participant can navigate in the environment using the teleportation capabilities performed by the use of the controller (HTC Vive)
    3.	Participant has to touch the body of the corresponding avatar (with apple on its head) to score a point
    4.	The participant can see his/her score on the top right corner of the VR headset’s field of view
    5.	After some time (approx. 10-15 mins), the manipulation stage ends
### 5.	Experiment Stage 1: Post-test
  +	Participant is again asked to modify the avatar to its own body shape with the use of the controller – 10 times 
  +	Participant is again asked to modify the avatar to their dream body shape with the use of the controller – 10 times
### 6.	The experiment ends (Proceed with other formalities)

## Technical details (Unity Development)

### 1.	Main menu: Detail entries
  +	The scene is called ‘Menu’
  +	All the visual elements are under Canvas object
  +	The menu elements are under Canvas->Menu object
  +	Menu object (under Canvas) has Main Menu script attached to it
  +	Main Menu script
    1.	Takes input regarding participant and saves to ‘save.txt’ file in the application folder
    2.	Apart from the menu entries, other data stored is:
        +	Score (How many apples[points] does the participant collect during the manipulation stage)
        +	Teleportation count (How many times the participant teleports during the manipulation stage)
        +	Distance (How much distance the participant travels in the manipulation stage)
        +	‘pretestBodyValues’ array (The body values selected by the participant as representation of their own body during stage 1: Pre-test)
        +	‘pretestIdealValues’ array (The body values selected by the participant as representation of their dream body during stage 1: Pre-test)
        +	‘postBodyValues’ array (The body values selected by the participant as representation of their own body during stage 3: Post-test)
        +	‘postIdealValues’ array (The body values selected by the participant as representation of their dream body during stage 3: Post-test)
    3.	The save.txt file contains log/information data in JSON format
    4.	JSONHelper is used to convert string data into JSON format
    5.	Function Load()
        +	Checks if already a ‘save.txt’ file exists
        +	If yes, then loads that file else creates a new file during call of function Save()
    6.	Function Save()
        +	Called on clicking the ‘OK’ button in the main menu
        +	Writes all the new participant’s data in the ‘save.txt’ file by converting into JSON format
        +	‘Playerprefs’ static unity datatype which stores player preferences. Score, Triggercount, and distance is stored in these so as to avoid high I/O costs. No need to write/read data from ‘save.txt’ file


### 2.	Character Room Scene
  +	Instructions are stored in the Canvas object under ‘instructionsText’
  +	Messages are also manipulated here in ‘text’ object
  +	Manager object
    1.	Only purpose is to call Character_Menu_Manager script
  +	Character_Menu_Manager script
    1.	2 gameobjects: bodyMale and bodyFemale
    2.	Instructions are manipulated using ‘instrText’ object
    3.	Function Start()
        +	Character object stores the participant info given in main menu here by reading the ‘save.txt’ file
        +	‘index’ is the last participant entry in the json file array, which is indeed the most recent participant whose entry was made in the main menu
        +	Checks gender of the participant and shows the corresponding avatar
        +	Position of the avatar is at (0,0,0). The player’s camera position is hardcoded to (0,0,-2)
        +	Take height from the JSON file and decrease it by 1.7f
        +	Give this height to blendShape00 function which helps to convert the height of the participant to corresponding unity scene values for the avatar
        +	Reposition the avatar. Because on manipulating the height, the legs of the avatar go below point of origin
    4.	Function blendShape00()
        +	If and else conditions sets a flag value so as to decide which skinned mesh renderer blend shape value to change
        +	Blnd1.Shape000_pos and Blnd1.Shape000_neg are manipulated to change the height of the avatar
    5.	Function randomBodyShape()
        +	Every time the participant confirms a body shape this function creates random values for a new body shape to be shown
    6.	Function update()
        +	Checks if the count and shows corresponding instruction text
        +	SteamVR_Actions GrabPinch looks for a press trigger on the touchpad
        +	The if conditions check where the touchpad is being pressed: left, right, up, down
        +	When the trigger button is pressed, write the values to the JSON file by first storing in the character object (Check if-else conditions)
    7.	Function blendShape01()
        +	Changes the bulkiness of the avatar character
        +	Boolean ‘pos’ determines whether to decrease or increase the width of the avatar
        +	False: decreases and True
    
### 3.	SMPL_mecanim scene
  +	Spawn body object contains Spawn body script and 2 audio sources (Walking and environment sounds)
  +	SpawnBody script
    1.	Spawns random avatars in the scene during runtime
    2.	Function Start()
        +	spawnNumberM and spawnNumberF decides the number of male and female clones to spawn respectively
        +	spawnNumber can be changed from unity inspector
        +	Lots of textures for male and female are available
        +	2 different texture arrays for male and female avatars
        +	Audios array is for successful score sound (i.e. when the participant collects an apple)
    3.	Function spawnBody()
        +	Create a clone and put it in the scene at a random position
        +	Attach texture to the clone
        +	Set speed of clone animation
        +	Add the clone to the bodyCloneArray
        +	Increase the ‘count’ variable
    4.	Function selectRandom()
        +	Randomly puts an apple on a male/female avatar’s head
        +	bodyCloneArray is used here to randomly select an avatar already existing in the scene (previously mentioned in spawnBody function)
    5.	Function selectRandom()
        +	Check participant’s input and triggers ‘UpdateTrigger’ function which is in ‘updateScore’ script attached to ‘ScoreSound’ object
  +	Score sound object contains UpdateScore script and also has a score count audio attached to it
  +	UpdateScore script
    1.	Retrieves JSON file again so as to update participant’s score, teleportCount, and distance
    2.	Function Start()
        +	Retrieves Playerprefs: distance, triggercount, and score
    3.	Function UpdateScore()
        +	Increase score count
        +	Update it in playerprefs score variable
        +	Change score text on the participant’s field of view
        +	Update and write the score value of the participant in the ‘save.txt’ file
    4.	Function UpdateTrigger()
        +	Increases triggercount corresponding to number of teleports made
        +	Update it in playerprefs triggercount variable
        +	Update and write the triggercount value of the participant in the ‘save.txt’ file
    5.	Function UpdateDistance()
        +	Called everytime a participant teleports
        +	The new distance travelled is passed to the function
        +	Total distance is calculated and stored here

  +	userScript script
    1.	Attached to player game object
    2.	Function Start()
        +	Retrieves old position of the participant in the virtual environment
    3.	Function Update()
        +	If the participant travels to a new position then calculate the distance travelled 
        +	Call the UpdateDistance function and pass this distance travelled to it as a ‘newDistance’ parameter
        +	Update the ‘oldPosition’ variable value by the new position
