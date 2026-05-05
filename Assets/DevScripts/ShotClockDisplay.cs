using UnityEngine;
using TMPro;

public class ShotClockDisplay : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI[] shotClockTexts;

    void Update()
    {
        float value = gameManager.currentShotClock;
        int score = gameManager.score;

        string time = Mathf.Ceil(value).ToString();

        bool isScore = true;
        foreach (var t in shotClockTexts)
        {
            t.text = $"{(isScore?time:score)}";
            isScore = !isScore;
        }
    }
}