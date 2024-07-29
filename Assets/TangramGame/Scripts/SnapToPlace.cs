using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPlace : MonoBehaviour
{
    public Transform correctPosition;
    //public GameObject correctPiece;
    public Sprite completeSprite;
    private bool isPlaced = false;
    public float snapThreshold = 0.2f; // Eşik değeri
    private Draggable draggable;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        draggable = GetComponent<Draggable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Update() 
    {
        Debug.Log($"Correct {correctPosition.name} Position: {correctPosition.position}");
        Debug.Log($"Piece {gameObject.name} Position: {transform.position}");
        if(!isPlaced)
        {
            float distance = Vector3.Distance(transform.position, correctPosition.position);
            Debug.Log(distance);

            if(distance < snapThreshold )
            {
              
                Debug.Log(correctPosition);

                transform.position = correctPosition.position;

                draggable.enabled = false;
                spriteRenderer.sprite = completeSprite;
                isPlaced = true;
            }
        }

    }

}
