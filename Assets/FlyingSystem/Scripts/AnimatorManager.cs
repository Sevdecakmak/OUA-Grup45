using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    private CrowManager crowManager;
    private CrowLocomotion crowController;
    private int horizontal;
    private int vertical;
    private void Awake() 
    {

        animator = GetComponent<Animator>();
        crowManager = GetComponent<CrowManager>();
        crowController = GetComponent<CrowLocomotion>();
        // Yürüme animasyonlarını kontrol etmek için hash değerlerini alıyoruz.
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }
    public void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool useRootMotion = false)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.SetBool("isUsingRootMotion", useRootMotion);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    // Animasyonlar ayarlandığı zaman Animator.SetFloat() fonksiyonları açılacak.
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        // Animation Snapping
        float snappedHorizontal;
        float snappedVertical;
        #region Snapped Horizontal
        if(horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if(horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if(horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if(horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Snapped Vertical
        if(verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if(verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if(verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if(verticalMovement < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion
        

        if(isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }
        animator.SetFloat("Vertical", snappedVertical, 0.1f, Time.deltaTime);
        animator.SetFloat("Horizontal", snappedHorizontal, 0.1f, Time.deltaTime);
    }

    private void OnAnimatorMove() 
    {
        if(crowManager.isUsingRootMotion)
        {
            crowController.crowRigidBody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            crowController.crowRigidBody.velocity = velocity;
        }
    }
}
