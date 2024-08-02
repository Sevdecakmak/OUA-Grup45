using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private CrowControls crowControls;
    private AnimatorManager animatorManager;
    private CrowLocomotion crowController;
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool b_Input;
    public bool x_Input;
    public bool jump_Input;
    public bool fly_Input;
    public bool attack_Input;


    public delegate void MouseRightClickAction();
    public static event MouseRightClickAction OnMouseRightClick;

    public delegate void EscapeAction();
    public static event EscapeAction OnEscape;
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        crowController = GetComponent<CrowLocomotion>();
    }

    private void OnEnable()
    {
        if (crowControls == null)
        {
            crowControls = new CrowControls();

            // Buradaki Movement ve Camera aksiyonları pass through olduğu için değerlerini alabiliyoruz.
            crowControls.CrowMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            crowControls.CrowMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            // Buradaki Click ve Sprint aksiyonları ise bir Button.
            crowControls.CrowMovement.Click.performed += i => OnClickPerformed(i);
            crowControls.CrowMovement.Click.canceled += i => OnClickCanceled(i);

            // Crow Controls inputlarından CrowActions içerisindeki sprint inputuna basıldığında b_Input true olacak.
            crowControls.CrowActions.Sprint.performed += i => b_Input = true;
            crowControls.CrowActions.Sprint.canceled += i => b_Input = false;

            // Crow Controls inputlarından CrowActions içerisindeki jump inputuna basıldığında jump_Input true olacak.
            crowControls.CrowActions.Jump.performed += i => jump_Input = true;

            // Dodgeleme geri sıçrama hareketi için x_Input true olacak.
            crowControls.CrowActions.X.performed += i => x_Input = true;

            // Uçma işlemi için fly_Input true olacak.
            crowControls.CrowActions.Fly.started += i => OnFlyStarted(i);
            crowControls.CrowActions.Fly.performed += i => OnFlyPerformed(i);
            crowControls.CrowActions.Fly.canceled += i => OnFlyCanceled(i);

            crowControls.CrowActions.Attack.performed += i => attack_Input = true;
            crowControls.CrowActions.Attack.canceled += i => attack_Input = false;
        }
        crowControls.Enable();
    }

    private void OnDisable()
    {
        crowControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpingInput();
        HandleDodgeInput();
        HandleFlyingInput();
        HandleAttackingInput();
    }
    private void HandleMovementInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, crowController.isSprinting);
    }
    private void HandleSprintingInput()
    {
        if (b_Input && moveAmount > 0.5f)
        {
            crowController.isSprinting = true;
        }
        else
        {
            crowController.isSprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            crowController.HandleJumping();
        }
    }
    private void HandleDodgeInput()
    {
        if (x_Input)
        {
            x_Input = false;
            crowController.HandleDodge();
        }
    }

    private void HandleFlyingInput()
    {
        if(fly_Input)
        {
            crowController.isFlying = true;
        }
        else
        {
            crowController.isFlying = false;
        }
    }

    private void HandleAttackingInput()
    {
        if (attack_Input)
        {
            crowController.isAttacking = true;
        }
        else
        {
            crowController.isAttacking = false;
        }
    }
    private void OnFlyStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Fly started");
        fly_Input = true;
    }
    private void OnFlyPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Fly performed");
        fly_Input = true;
    }
    private void OnFlyCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Fly canceled");
        fly_Input = false;
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (context.control.name == "rightButton")
        {
            OnMouseRightClick?.Invoke();
        }
    }
    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        if (context.control.name == "escape")
        {
            OnEscape?.Invoke();
        }
    }
}
