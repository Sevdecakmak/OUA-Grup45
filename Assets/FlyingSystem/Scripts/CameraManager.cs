using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    private InputManager inputManager;
    public Transform targetTransform;       // Kameranın takip edeceği hedef
    public Transform cameraPivot;           // Kameranın pivotu
    public Transform cameraTransform;       // Kameranın transformu
    public LayerMask collisionLayers;       // Kameranın çarpışma layerları
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero; // Kameranın takip edeceği hedefin hızı
    private Vector3 cameraVectorPosition; 

    public float cameraCollisionOffSet = 0.2f; // Kameranın çarpışma ofseti
    public float minimumCollisionOffSet = 0.2f; // Kameranın minimum çarpışma ofseti
    public float cameraCollisionRadius = 2f; // Kameranın çarpışma yarıçapı
    public float cameraFollowSpeed = 0.2f; // Kameranın takip hızı
    public float cameraLookSpeed = 2f;     // Kameranın bakış hızı
    public float cameraPivotSpeed = 2f;    // Kameranın pivot hızı

    public float lookAngle;             // Kameranın dikey bakış açısı (Yukarı, Aşağı)
    public float pivotAngle;            // Kameranın yatay bakış açısı (Sağa, Sola)

    public float minPivotAngle = -35f;  // Kameranın en aşağı bakış açısı
    public float maxPivotAngle = 35f;   // Kameranın en yukarı bakış açısı

    private void Awake() 
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<CrowManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
        
        InputManager.OnMouseLeftClick += CursorLock;
        InputManager.OnEscape += CursorUnlock;

    }
    private void OnDestroy() 
    {
        InputManager.OnMouseLeftClick -= CursorLock;
        InputManager.OnEscape -= CursorUnlock;
    }
    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }
    private void CursorLock()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }
    private void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;

    }    
    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    public void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = targetPosition -(distance - cameraCollisionOffSet);
        }
        if(Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition = targetPosition - minimumCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
