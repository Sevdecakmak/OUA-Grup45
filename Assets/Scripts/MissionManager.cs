using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public Mission[] missions; // Sorular

    public TMP_Text missionTextPrefab; // �nceden haz�rlanm�� TMP_Text bile�eni
    public Vector2 startPosition; // TMP_Text bile�eni i�in pozisyon ayar�
    public Transform parentTransform; // TMP_Text bile�enlerinin eklenece�i parent Transform
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
            // Yeni bir TMP_Text bile�eni olu�tur
            TMP_Text newMissionText = Instantiate(missionTextPrefab, parentTransform);

            // G�revin ad�n� ayarla
            newMissionText.text = mission.mission_name;

            // Pozisyonu ayarla
            RectTransform rectTransform = newMissionText.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(startPosition.x, startPosition.y - (yVal * missionNum), 0);

            Image childImage = newMissionText.GetComponentInChildren<Image>();
            if (childImage != null)
            {
                // Image bile�enini gizle
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