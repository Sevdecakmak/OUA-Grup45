using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowLocomotion : MonoBehaviour
{
    private CrowManager crowManager;
    private AnimatorManager animatorManager;
    private InputManager inputManager;

    private Vector3 moveDirection;
    private Transform cameraObject;
    public Rigidbody crowRigidBody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5f;
    public float sprintSpeed = 8f;
    public float rotationSpeed = 15f;

    [Header("Jump Speeds")]
    public float jumpHeight = 3f;
    public float gravityIntensity = -15f;

    [Header("Flying")]
    public bool isFlying;
    public float flyingSpeed = 5f;
    public float flyingLiftForce = 10f;
    private void Awake()
    {
        crowManager = GetComponent<CrowManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        crowRigidBody = GetComponent<Rigidbody>();

        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovements()
    {
        HandleFallingAndLanding();
        if (crowManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isJumping)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            Debug.Log("Sprinting");
            moveDirection = moveDirection * sprintSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;

            }
        }


        var movementVelocity = moveDirection;
        crowRigidBody.velocity = movementVelocity;

    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        var targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        var targetRotation = Quaternion.LookRotation(targetDirection);
        var crowRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = crowRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if (!isGrounded)
        {
            if (!crowManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            animatorManager.animator.SetBool("isUsingRootMotion", false);
            inAirTimer = inAirTimer + Time.deltaTime;
            crowRigidBody.AddForce(transform.forward * leapingVelocity);
            crowRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !crowManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }

            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


    }
    public void HandleJumping()
    {
        Debug.Log("Jumping");

        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 crowVelocity = moveDirection;
            crowVelocity.y = jumpingVelocity;
            crowRigidBody.velocity = crowVelocity;
        }
    }
    public void HandleDodge()
    {
        Debug.Log("Dodge");
        if (crowManager.isInteracting)
            return;

        animatorManager.PlayTargetAnimation("Dodge", true, true);

    }
    public void HandleFlying()
    {
        Debug.Log("Flying");

        // animatorManager.animator.SetBool("isFlying", true);

        if (inputManager.fly_Input && !isFlying && isGrounded)
        {
            isFlying = true;
            isGrounded = false;

            animatorManager.PlayTargetAnimation("Fly", true);
        }

        if (isFlying)
        {
            Vector3 flyDirection = cameraObject.forward * inputManager.verticalInput;
            flyDirection += cameraObject.right * inputManager.horizontalInput;
            flyDirection.Normalize();

            // Yükseklik kontrolü
            if (inputManager.fly_Input)
            {
                flyDirection.y = 1; // Yukarı doğru uçma
            }
            else
            {
                flyDirection.y = 0; // Y ekseni hareketi durdur
            }

            float currentFlyingSpeed = flyingSpeed; // Bu değeri istediğiniz gibi ayarlayabilirsiniz

            // Karganın uçma hareketi
            crowRigidBody.velocity = flyDirection * currentFlyingSpeed;
        }

        if (!inputManager.fly_Input && isFlying)
        {
            // Karga uçmayı durduruyor
            isFlying = false;
            // Karganın yere düşmesini sağlar
            crowRigidBody.velocity = new Vector3(crowRigidBody.velocity.x, -fallingVelocity, crowRigidBody.velocity.z);
        }

    }
}
