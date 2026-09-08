# Game-492-Project

Github Repo for my GAME 492 project.

---

### Updates:

9/8/2026:
- Goals:
   - (!!) Convert Jammo input to use NewInputSystem (Currently uses Old System [Non-GUI-Based])
      - Gamepad support automatic* if done w/ New InputAction system
   - (!) Look into Cinemachine [dependency for Jammo]
   - (!) Collect animations for character actions [3x attack animations]
- Notes:
   - The "NewInputSystem" is actually more like 5 years old w/ a variety of implementation techniques. Global custom script, use of Global InputAction system, use of shorthand Classes like Mouse/Keyboard, a generated C# class [most known to me atm], and likely more I'm unaware of. Additionally, there's the system before the "New" one, "Old", as well as a GUI-Based input system. Likely other systems as well.
        - [Using] "New" InputAction-Based System covered here: [https://www.youtube.com/watch?v=Yjee_e4fICc](https://www.youtube.com/watch?v=Yjee_e4fICc)
        - [Not Using] "New" Shorthand Class conversion from Old system to New covered here: [https://www.youtube.com/watch?v=Q7NFzES5GMU](https://www.youtube.com/watch?v=Q7NFzES5GMU)
    - New InputAction Implementation Notes:
      - Horizontal & Vertical Movement Vectors: [in JammoChar/Scripts/MovementInput.cs]
          - Old Code:
              - ```InputX = Input.GetAxis ("Horizontal");```
              - ```InputZ = Input.GetAxis ("Vertical");```
          - New Code:
              - ``` ```
              - ``` ```
      - Eye Position Switching [4 total] [in JammoChar/Scripts/CharacterSkinController.cs]
          - Old Code: ```Input.GetKeyDown(KeyCode.Alpha1)``` [x4 w/ switch per direct Keycode]
          - New Code: ``` ``` [x4 w/ switch per InputAction]
    - 
