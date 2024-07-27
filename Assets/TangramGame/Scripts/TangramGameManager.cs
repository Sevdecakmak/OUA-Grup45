using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangramGameManager : MonoBehaviour
{
    public GameObject[] tangramPieces;
    public Transform[] randomPositions;
    public GameObject silhouette;

    private void Start()
    {
        ShufflePieces();
        StartCoroutine(ToggleSilhouette());
    }

    private void ShufflePieces()
    {
        foreach (GameObject piece in tangramPieces)
        {
            int randomIndex = Random.Range(0, randomPositions.Length);
            piece.transform.position = randomPositions[randomIndex].position;
        }
    }

    private IEnumerator ToggleSilhouette()
    {
        while (true)
        {
            silhouette.SetActive(!silhouette.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
