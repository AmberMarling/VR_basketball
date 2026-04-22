using UnityEngine;
using TMPro;

public class ShotClockDisplay : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI[] shotClockTexts;

    void Update()
    {
        float value = gameManager.currentShotClock;

        string time = Mathf.Ceil(value).ToString();

        foreach (var t in shotClockTexts)
        {
            t.text = time;
        }
    }
}