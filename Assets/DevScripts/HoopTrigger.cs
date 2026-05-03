using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        Top,
        Bottom
    }

    public TriggerType triggerType;
    public HoopScoreDetector scoreDetector;

    private void OnTriggerEnter(Collider other)
    {
        Basketball basketball = other.GetComponentInParent<Basketball>();

        if (basketball == null)
        {
            return;
        }

        if (scoreDetector == null)
        {
            return;
        }

        if (triggerType == TriggerType.Top)
        {
            scoreDetector.BallEnteredTop(basketball);
        }
        else
        {
            scoreDetector.BallEnteredBottom(basketball);
        }
    }
}