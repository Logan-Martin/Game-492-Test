
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class PlrMovementInput : MonoBehaviour {
    [SerializeField] private MovementInput movementInputScript;
	public Vector2 playerInput_MovementVector;
	private PlayerInput playerInput; // For NewInputSystem
    // 00000000000000 //

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

	
    public void UpdateInput_PlayerMovementVector2(InputAction.CallbackContext context)
    {
        playerInput_MovementVector = context.ReadValue<Vector2>(); // read from context as a Vector2, as is the type
    }


    // Update is called once per frame
    void Update () {
        movementInputScript.InputX = playerInput_MovementVector.x;
        movementInputScript.InputZ = playerInput_MovementVector.y;
        print("" + playerInput_MovementVector.x + " | " + playerInput_MovementVector.y);
    }

    // !!event needs to be connected to EventSystem GameObj. -> PlayerInput
    public void InputFunc_Attack(InputAction.CallbackContext context) 
    {
        if (!context.performed) { return; } // so below then if it's perf code below will run
        movementInputScript.AttackFunc();
    }
}
