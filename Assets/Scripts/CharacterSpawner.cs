using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject cameraManagerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);

        var existingCameraManager = FindObjectOfType<CameraManager>();

        if(existingCameraManager == null)
            Instantiate(cameraManagerPrefab, spawnPoint.position , spawnPoint.rotation);
    }
}
