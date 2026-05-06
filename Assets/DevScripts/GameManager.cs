using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //[Header("Shot Clock")]
    //public float shotClockLength = 24f;
    //public float currentShotClock;
    //public bool clockRunning = true;

    [Header("Score")]
    public int score = 0;
    public int pointsPerBasket = 2;
    public TMP_Text[] scoreTexts;

    [Header("Ball")]
    public Basketball basketball;

    private void Start()
    {
        //ResetShotClock();
        UpdateScoreUI();
    }

    private void Update()
    {
        // if (!clockRunning)
        // {
        //     return;
        // }

        // currentShotClock -= Time.deltaTime;

        // if (currentShotClock <= 0f)
        // {
        //     currentShotClock = 0f;
        //     ShotClockExpired();
        // }
    }

    // public void ResetShotClock()
    // {
    //     currentShotClock = shotClockLength;
    //     clockRunning = true;
    // }

    public void AddMadeBasket()
    {
        score += pointsPerBasket;
        UpdateScoreUI();

        //ResetShotClock();

        // if (basketball != null)
        // {
        //     basketball.ResetBallAfterDelay(1.0f);
        // }
    }

    // public void RegisterMiss()
    // {
    //     ResetShotClock();

    //     if (basketball != null)
    //     {
    //         basketball.ResetBallAfterDelay(1.5f);
    //     }
    // }

    // private void ShotClockExpired()
    // {
    //     clockRunning = false;

    //     if (basketball != null)
    //     {
    //         basketball.ResetBallAfterDelay(1.0f);
    //     }

    //     Invoke(nameof(ResetShotClock), 1.0f);
    // }

    private void UpdateScoreUI()
    {
        foreach (TMP_Text text in scoreTexts)
        {
            if (text != null)
            {
                text.text = score.ToString();
            }
        }
    }
}