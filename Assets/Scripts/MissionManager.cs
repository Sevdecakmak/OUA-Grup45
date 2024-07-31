using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public Mission[] missions; // Sorular

    public TMP_Text missionTextPrefab; // Önceden hazýrlanmýþ TMP_Text bileþeni
    public Vector2 startPosition; // TMP_Text bileþeni için pozisyon ayarý
    public Transform parentTransform; // TMP_Text bileþenlerinin ekleneceði parent Transform
    int yVal = 35;

    [System.Serializable]
    public struct Mission
    {
		
        public string mission_name;
        public bool is_completed;

        public Sprite crossImageTik;
        public Sprite crossImageCarpi;
    }

    public void Setup(Mission[] missions)
    {
        int missionNum = 0;

        foreach (var mission in missions)
        {
            // Yeni bir TMP_Text bileþeni oluþtur
            TMP_Text newMissionText = Instantiate(missionTextPrefab, parentTransform);

            // Görevin adýný ayarla
            newMissionText.text = mission.mission_name;

            // Pozisyonu ayarla
            RectTransform rectTransform = newMissionText.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(startPosition.x, startPosition.y - (yVal * missionNum), 0);

            Image childImage = newMissionText.GetComponentInChildren<Image>();
            if (childImage != null)
            {
                // Image bileþenini gizle
                childImage.gameObject.SetActive(false);
            }

            missionNum++;
        }
    }

    private void Start()
    {
        Setup(missions);
    }
}