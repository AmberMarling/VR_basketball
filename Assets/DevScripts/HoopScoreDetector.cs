using System.Collections.Generic;
using UnityEngine;

public class HoopScoreDetector : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;

    [Header("Scoring Rules")]
    public float maxTimeBetweenTriggers = 1.0f;
    public float requiredDownwardVelocity = -0.2f;

    private Dictionary<Basketball, float> ballsThatEnteredTop = new Dictionary<Basketball, float>();

    public void BallEnteredTop(Basketball basketball)
    {
        if (basketball == null)
        {
            return;
        }

        if (!basketball.CanScore())
        {
            return;
        }

        Rigidbody rb = basketball.GetRigidbody();

        if (rb == null)
        {
            return;
        }

        // The ball should be moving downward when it enters the top of the hoop.
        if (rb.linearVelocity.y > requiredDownwardVelocity)
        {
            return;
        }

        ballsThatEnteredTop[basketball] = Time.time;
    }

    public void BallEnteredBottom(Basketball basketball)
    {
        if (basketball == null)
        {
            return;
        }

        if (!basketball.CanScore())
        {
            return;
        }

        if (!ballsThatEnteredTop.ContainsKey(basketball))
        {
            return;
        }

        float timeSinceTopEntry = Time.time - ballsThatEnteredTop[basketball];

        if (timeSinceTopEntry > maxTimeBetweenTriggers)
        {
            ballsThatEnteredTop.Remove(basketball);
            return;
        }

        Rigidbody rb = basketball.GetRigidbody();

        if (rb == null)
        {
            return;
        }

        // Still require the ball to be moving downward through the net.
        if (rb.linearVelocity.y > requiredDownwardVelocity)
        {
            return;
        }

        ballsThatEnteredTop.Remove(basketball);

        basketball.MarkScored();

        if (gameManager != null)
        {
            gameManager.AddMadeBasket();
        }
    }
}