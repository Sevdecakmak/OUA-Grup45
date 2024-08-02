using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowManager : MonoBehaviour
{
    private Animator animator;
    private Animation animation;
    private InputManager inputManager;
    private CameraManager cameraManager;
    private CrowLocomotion crowController;

    public bool isInteracting;
    public bool isUsingRootMotion;
    private void Awake() 
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        crowController = GetComponent<CrowLocomotion>();
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate() 
    {
        crowController.HandleAllMovements();
    }

    private void LateUpdate() 
    {
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");
        crowController.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", crowController.isGrounded);
    }
}
