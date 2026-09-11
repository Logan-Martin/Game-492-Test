using UnityEngine;
using UnityEngine.InputSystem;

public class PlrAttackSystem : MonoBehaviour
{
    Animator animator; //assuming i need this to change anims
                       // CharacterSkinController does ```animator.SetTrigger(str trigger);```
                       

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void InputTestFunction(InputAction.CallbackContext context) // event needs to be connected to EventSystem GameObj. -> PlayerInput
    {
        //context.phase = InputActionPhase.Performed; // Started, Waiting, Performed, Canceled, Disabled
        //context.duration = 0;
        //context.performed // shorthand for phase?
        //context.startTime;
        //context.time;
        //context.ReadValue();
        //context.started // shorthand for phase?
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
