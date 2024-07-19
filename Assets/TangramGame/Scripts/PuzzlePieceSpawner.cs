using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceSpawner : MonoBehaviour
{
    private GameObject[] puzzlePieces; // 7 tane puzzle parçasının prefab'leri burada olmalı
    public Vector2 xRange = new Vector2(-8f, -1f); // X ekseni aralığı
    public Vector2 yRange = new Vector2(-4f, 4f); // Y ekseni aralığı

    void Start()
    {
        puzzlePieces = GameObject.FindGameObjectsWithTag("Draggable"); // Tag'i Draggable olan tüm objeleri alır
        DistributePuzzlePieces();
    }

    void DistributePuzzlePieces()
    {
        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(xRange.x, xRange.y),
                Random.Range(yRange.x, yRange.y),
                0f
            );
            puzzlePiece.transform.position = randomPosition;
        }
    }
}
