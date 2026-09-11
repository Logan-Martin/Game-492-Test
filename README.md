# Game-492-Project

Github Repo for my GAME 492 project.

---

### Updates:

9/10/2026:
- Added PlrAttackSystem script w/ time-sensitive chain outputs
   - Key thing to work on next: Animation phases w/ animation events. Enum started w/ phases. 
- Cleaned up previous day's formatting.
- Looked at the params of context from ```InputAction.CallbackContext```
	- Unity Docs: [https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/api/UnityEngine.InputSystem.InputAction.CallbackContext.html](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/api/UnityEngine.InputSystem.InputAction.CallbackContext.html)

9/9/2026:
- Notes:
   - Use the same button for punch, cross, and hurricane kick. Change in attack based on presses in succession.
   - (!) It's possible to bake movement into animation but can shouldn't for ease of changing that
   - (!) Have a way to define when animation attack is Active [where the hitbox is] Active frame window.
	 - (!) put cube hitbox on Armature -> fist/arm 
   - (!) When in attack animation, no movement
   - idea of combo. Within a certain amount of time allow combo.
- Goal: Implement attack that changes animation based on presses of the same button in succession 

9/8/2026:
- Goals:
   - [Done] (!!) Convert Jammo input to use NewInputSystem (Currently uses Old System [Non-GUI-Based])
      - Gamepad support automatic* if done w/ New InputAction system
   - [Watched a video on it] (!) Look into Cinemachine [dependency for Jammo]
   - [Found & Downloaded 3 anims. Not Sure how to Implement] (!) Collect animations for character actions [3x attack animations] [Mixamo link: [https://www.mixamo.com/#/?genres=Combat](https://www.mixamo.com/#/?genres=Combat)]
   - Later Goal: Adding combo attacks
- Notes:
   - The "NewInputSystem" is actually more like 5 years old w/ a variety of implementation techniques. Global custom script, use of Global InputAction system, use of shorthand Classes like Mouse/Keyboard, a generated C# class [most known to me atm], and likely more I'm unaware of. Additionally, there's the system before the "New" one, "Old", as well as a GUI-Based input system. Likely other systems as well.
        - [Using] "New" InputAction-Based System covered here: [https://www.youtube.com/watch?v=Yjee_e4fICc](https://www.youtube.com/watch?v=Yjee_e4fICc)
        - [Not Using] "New" Shorthand Class conversion from Old system to New covered here: [https://www.youtube.com/watch?v=Q7NFzES5GMU](https://www.youtube.com/watch?v=Q7NFzES5GMU)
    - New InputAction Implementation Notes:
      - Config. Or Create New Input Action: [Project Settings --> Input System Package]
         - Press + button beside Action. Binding must be made per ControlScheme [Keyboard, Touch, Gamepad, etc.].    
      - Horizontal & Vertical Movement Vectors: [in JammoChar/Scripts/MovementInput.cs]
          - Old Code:
              - ```InputX = Input.GetAxis ("Horizontal");```
              - ```InputZ = Input.GetAxis ("Vertical");```
          - New Code:
              - ```
                public Vector2 playerInput_MovementVector;
                // and then assignment where needed within existing code.
                // InputX = playerInput_MovementVector.x;
                // InputZ = playerInput_MovementVector.y;
                
                public void UpdateInput_PlayerMovementVector2(InputAction.CallbackContext context)
                {
                   playerInput_MovementVector = context.ReadValue<Vector2>(); // read from context as a Vector2, as is the type
                }
                ```
      - Eye Position Switching [4 total] [in JammoChar/Scripts/CharacterSkinController.cs]
          - Old Code: ```Input.GetKeyDown(KeyCode.Alpha1)``` [x4 w/ switch per direct Keycode]
          - New Code:
             - ```
               // [x4 w/ switch per InputAction - Normal, Happy, Angry, Dead states *in order. Now using 1-4 w/ Keyboard. D-Pad (Up, Left, Right, Down *in that order) w/ Gamepad]
               public void ChangeEyesOnInput_Normal(InputAction.CallbackContext context) // event connected to EventSystem GameObj. -> PlayerInput
               {
                  if (context.performed) { // if statement to stop x3 [start, performed, end] firing
                     //ChangeMaterialSettings(0);
                     ChangeEyeOffset(EyePosition.normal);
                     ChangeAnimatorIdle("normal");
                  }
               }
               ```    
