using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // LINQ kütüphanesini ekleyin

public class MissionManager : MonoBehaviour
{
    public Mission[] missions; // Görevler

    public TMP_Text missionTextPrefab; // Önceden hazırlanmış TMP_Text bileşeni
    public Vector2 startPosition; // TMP_Text bileşeni için pozisyon ayarı
    public Transform parentTransform; // TMP_Text bileşenlerinin ekleneceği parent Transform
    int yVal = 35;

    [System.Serializable]
    public struct Mission
    {
        public string mission_name;
        public bool is_completed;

        public Sprite crossImageTik;
        public Sprite crossImageCarpi;
    }

    // Görevlerin tamamlanıp tamamlanmadığını kontrol eden method
    public bool AreAllMissionsCompleted()
    {
        return missions.All(m => m.is_completed);
    }

    // Görevlerin tamamlanma durumunu izleyen bir event oluştur
    public delegate void OnAllMissionsCompletedHandler();
    public event OnAllMissionsCompletedHandler OnAllMissionsCompleted;

    public void Setup(Mission[] missions)
    {
        int missionNum = 0;

        foreach (var mission in missions)
        {
            // Yeni bir TMP_Text bileşeni oluştur
            TMP_Text newMissionText = Instantiate(missionTextPrefab, parentTransform);

            // Görevin adını ayarla
            newMissionText.text = mission.mission_name;

            // Pozisyonu ayarla
            RectTransform rectTransform = newMissionText.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(startPosition.x, startPosition.y - (yVal * missionNum), 0);

            Image childImage = newMissionText.GetComponentInChildren<Image>();
            if (childImage != null)
            {
                // Image bileşenini gizle
                childImage.gameObject.SetActive(false);
            }

            missionNum++;
        }
    }

    private void Start()
    {
        Setup(missions);
    }

    private void Update()
    {
        if (AreAllMissionsCompleted())
        {
            OnAllMissionsCompleted?.Invoke();
        }
    }
}
