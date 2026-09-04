using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSkinController : MonoBehaviour
{
    Animator animator;
    Renderer[] characterMaterials;

    public Texture2D[] albedoList;
    [ColorUsage(true,true)]
    public Color[] eyeColors;
    public enum EyePosition { normal, happy, angry, dead}
    public EyePosition eyeState;

    // -------- //
    // Variables for NewInputSystem
    // Note: This Script on changes the face of the player character, not movement
    public InputSystem_Actions playerControls; 
            // auto under Assets folder by Unity when using new input system. Named InputSystem_Actions

    // public for ease of click-and-drag refrence

    private InputAction EyePositionAction_Normal;
    private InputAction EyePositionAction_Angry;
    private InputAction EyePositionAction_Happy;
    private InputAction EyePositionAction_Dead;
        // Note: ever InputAction needs to be:
            // 1. set up w/ the type itself as a variable 
            // 2. assigned/connected to an event under the OnEnabled() event w/ the following code:
                    // variable = variable.Category.NameOfAction;
                    // variable.performed += RelatedFunction;
                            // !Note: Each RelatedFunction must start w/ an 'InputAction.CallbackContext' argument type

        // Note: For individual InputActions to work, I think there always needs to be the overall InputSystem_Actions
        // That setup requires the following: [In only one? script or anytime used in a script?]
            // 1. variable [of InputActionAsset type] assigned on Awake() event:
                    // Ex: playerControls = new InputActionAsset();
            // 2. same needs to be connected to OnDisable() event:
                    // Ex: playerControls.Disable();

    // 000000000 //


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        
    }

    private void Awake()
    {
        playerControls = new InputActionAsset();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        EyePositionAction_Normal = playerControls
        EyePositionAction_Angry
        EyePositionAction_Happy
        EyePositionAction_Dead
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //ChangeMaterialSettings(0);
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //ChangeMaterialSettings(1);
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //ChangeMaterialSettings(2);
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //ChangeMaterialSettings(3);
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
        }
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ChangeMaterialSettings(int index)
    {
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetColor("_EmissionColor", eyeColors[index]);
            else
                characterMaterials[i].material.SetTexture("_MainTex",albedoList[index]);
        }
    }

    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_MainTex", offset);
        }
    }
}
