using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float shotClockDuration = 24f;
    public float currentShotClock;

    void Start()
    {
        currentShotClock = shotClockDuration;
    }

    void Update()
    {
        currentShotClock -= Time.deltaTime;

        if (currentShotClock < 0)
        {
            currentShotClock = 0;
        }
    }
}