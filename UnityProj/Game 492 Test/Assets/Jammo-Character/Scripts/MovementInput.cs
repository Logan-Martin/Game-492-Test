
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

    public float Velocity;
    [Space]

	public float InputX;
	public float InputZ;
	public Vector2 playerInput_MovementVector;
    //
    public AttackSystem attackSystem_Ref;
    //


	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
    private bool allowPlayerMovement = true;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

	private PlayerInput playerInput; // For NewInputSystem
    // 00000000000000 //

    public void TogglePlayerMovement(bool toggle)
    {
        allowPlayerMovement = toggle;
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Use this for initialization
    void Start () {
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();
	}
	
    public void UpdateInput_PlayerMovementVector2(InputAction.CallbackContext context)
    {
        playerInput_MovementVector = context.ReadValue<Vector2>(); // read from context as a Vector2, as is the type
    }


	// Update is called once per frame
	void Update () {
		InputMagnitude ();

        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel -= 0;
        }
        else
        {
            verticalVel -= 1;
        }
        moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        controller.Move(moveVector);


    }

    void PlayerMoveAndRotation() {
		//InputX = Input.GetAxis ("Horizontal");
		//InputZ = Input.GetAxis ("Vertical");
		InputX = playerInput_MovementVector.x;
		InputZ = playerInput_MovementVector.y;



        var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputZ + right * InputX;

		if (blockRotationPlayer == false) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
		}
	}

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

	void InputMagnitude() {
        //Calculate Input Vectors
        //InputX = Input.GetAxis ("Horizontal");
        //InputZ = Input.GetAxis ("Vertical");
        InputX = playerInput_MovementVector.x;
        InputZ = playerInput_MovementVector.y;

        //anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        //anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player
        if (allowPlayerMovement)
        {
            if (Speed > allowPlayerRotation)
            {
                anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
                PlayerMoveAndRotation();
            }
            else if (Speed < allowPlayerRotation)
            {
                anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
            }
        }
	}

    public void InputFunc_Attack(InputAction.CallbackContext context) // event needs to be connected to EventSystem GameObj. -> PlayerInput
    {
        if (!context.performed) { return; } // so below then if it's perf code below will run
        if (attackSystem_Ref.lastAnimPhase == CustomAnimationPhaseType.LeavingPhase || attackSystem_Ref.lastAnimPhase == CustomAnimationPhaseType.None) // not in anim commit window
        {
            // Check for Normal or Another Type of Attack Needed
            //print(Time.time - tSinceLastPerfAttack);
            if (!((Time.time - attackSystem_Ref.tSinceLastPerfAttack) < attackSystem_Ref.tWindowToAllowNxtAttack) || (attackSystem_Ref.lastAttackType == AttackType.None))
            {
                // if NOT [within time window to allow next attack] OR [lastAttack was set to None meaning a chain alr happened or time passed]
                // -so do normal attack work
                attackSystem_Ref.DoNormalAttack();
                return;
            }
            // 0000 //

            // Here on means non-normal attack:
            if (attackSystem_Ref.lastAttackType == AttackType.None) // None -> Base
            {
                attackSystem_Ref.DoNormalAttack();
            }
            else if (attackSystem_Ref.lastAttackType == AttackType.Base) // Base -> 2nd
            {
                attackSystem_Ref.Do2ndAttack();
            }
            else if (attackSystem_Ref.lastAttackType == AttackType.Attack2) // 2nd -> 3rd
            {
                attackSystem_Ref.Do3rdAttack();
            }
            else if (attackSystem_Ref.lastAttackType == AttackType.Attack3)
            {
                // in middle of perf attack3
                return;
            }
            else
            {
                print("Wrong string given!");
                // wrong str given
            }
        }


        //context.phase = InputActionPhase.Performed; // Started, Waiting, Performed, Canceled, Disabled
        //context.duration = 0;
        //context.performed // shorthand for phase?
        //context.startTime;
        //context.time;
        //context.ReadValue();
        //context.started // shorthand for phase?
    }
}
