using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowLocomotion : MonoBehaviour
{
    #region Variables
    private CrowManager crowManager;
    private AnimatorManager animatorManager;
    private InputManager inputManager;
    private GroundCheck groundCheck;

    private Vector3 moveDirection;
    private Transform cameraObject;
    public Rigidbody crowRigidBody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
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

    [Header("Dodge")]
    public float dodgeDistance = 5f;
    public float dodgeSpeed = 10f;
    public float dodgetimer = 5f;

    [Header("Combat")]
    public Transform target;
    public bool isFaceTarget;
    public bool isAttacking;
    public float stickyTargetDistance = 1;
    public float stickyTargetAmount = 1;
    public float combatCooldown = 2;
    private float currentCombatCooldown;

    #endregion
    private void Awake()
    {
        crowManager = GetComponent<CrowManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        crowRigidBody = GetComponent<Rigidbody>();

        groundCheck = GetComponentInChildren<GroundCheck>();

        cameraObject = Camera.main.transform;

    }
    public void HandleAllMovements()
    {
        isGrounded = groundCheck.isGrounded;
        HandleFallingAndLanding();
        if (crowManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();

        CalculateCombat();
        if(isAttacking)
            HandleAttacking();

        if (isFlying)
            HandleFlying();
    }

    #region Movement
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
    #endregion
    
    #region Rotation
    private void HandleRotation()
    {

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

        // Karganın ileriye bakmasını sağlamak için yazdım fakat karga kendi etrafında sürekli dönüyor.
        targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y + 90, targetRotation.eulerAngles.z);

        var crowRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = crowRotation;
    }
    #endregion

    #region Falling
    private void HandleFallingAndLanding()
    {
        if (!isGrounded)
        {
            inAirTimer = inAirTimer + Time.deltaTime;
            crowRigidBody.AddForce(transform.forward * leapingVelocity);
            crowRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }
        else
        {
            inAirTimer = 0;
        }

    }
    #endregion

    #region Jumping
    public void HandleJumping()
    {
        Debug.Log("Jumping");

        if (isGrounded && !isJumping)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 crowVelocity = moveDirection;
            crowVelocity.y = jumpingVelocity;
            crowRigidBody.velocity = crowVelocity;

        }
    }
    #endregion
    
    #region Dodge
    public void HandleDodge()
    {
        Debug.Log("Dodge");


        var dash = -moveDirection;
        Vector3 dodgeDirection = dash;

        crowRigidBody.AddForce(dodgeDirection * dodgeDistance * dodgeSpeed, ForceMode.Impulse);

        StartCoroutine(DodgeTimerCoroutine());

    }

    private IEnumerator DodgeTimerCoroutine()
    {
        yield return new WaitForSeconds(dodgetimer);
    }
    #endregion
    
    #region Flying
    public void HandleFlying()
    {
        Debug.Log("Flying: " + isFlying);

        animatorManager.animator.SetBool("isFlying", true);
        animatorManager.PlayTargetAnimation("Fly", false);

        Vector3 flyDirection = cameraObject.forward * inputManager.verticalInput;
        flyDirection += cameraObject.right * inputManager.horizontalInput;
        flyDirection.Normalize();
        flyDirection.y = 1; // Yukarı doğru uçma

        float currentFlyingSpeed = flyingSpeed;

        // Karganın uçma hareketi
        crowRigidBody.velocity = flyDirection * currentFlyingSpeed;

    }
    #endregion

    #region Combat
    public void HandleAttacking()
    {
        Debug.Log("Attacking");

        animatorManager.animator.SetBool("isAttacking", true);
        animatorManager.PlayTargetAnimation("Attack", false);

        if(Vector3.Distance(transform.position, target.transform.position) > stickyTargetDistance)
        {
            crowRigidBody.AddRelativeForce(Vector3.forward * stickyTargetAmount, ForceMode.Force);
        }


        currentCombatCooldown = combatCooldown;
    }

    

    private void CalculateCombat()
    {
        if (currentCombatCooldown < 0)
        {
            currentCombatCooldown = currentCombatCooldown - Time.deltaTime;
        }
    }
    #endregion
}
