using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class Basketball : MonoBehaviour
{
    [Header("Reset Settings")]
    public Transform spawnPoint;
    public float outOfBoundsY = -10f;
    public float resetDelayAfterOutOfBounds = 1.0f;

    [Header("Miss Detection")]
    public float missDelayAfterFloorHit = 2.0f;
    public string floorTag = "Floor";

    [Header("References")]
    public GameManager gameManager;

    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private bool hasBeenThrown;
    private bool scoredThisShot;
    private bool resetScheduled;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void Update()
    {
        if (transform.position.y < outOfBoundsY && !resetScheduled)
        {
            ResetBallAfterDelay(resetDelayAfterOutOfBounds);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        hasBeenThrown = false;
        scoredThisShot = false;
        resetScheduled = false;

        StopAllCoroutines();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        hasBeenThrown = true;
        scoredThisShot = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenThrown || scoredThisShot || resetScheduled)
        {
            return;
        }

        if (collision.gameObject.CompareTag(floorTag))
        {
            StartCoroutine(MissAfterDelay());
        }
    }

    private IEnumerator MissAfterDelay()
    {
        resetScheduled = true;

        yield return new WaitForSeconds(missDelayAfterFloorHit);

        if (!scoredThisShot)
        {
            if (gameManager != null)
            {
                gameManager.RegisterMiss();
            }
            else
            {
                ResetBallNow();
            }
        }
    }

    public void MarkScored()
    {
        scoredThisShot = true;
        hasBeenThrown = false;
        resetScheduled = true;
    }

    public bool CanScore()
    {
        return hasBeenThrown && !scoredThisShot;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public void ResetBallAfterDelay(float delay)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (!resetScheduled)
        {
            StartCoroutine(ResetAfterDelay(delay));
        }
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        resetScheduled = true;

        yield return new WaitForSeconds(delay);

        ResetBallNow();
    }

    public void ResetBallNow()
    {
        StopAllCoroutines();

        hasBeenThrown = false;
        scoredThisShot = false;
        resetScheduled = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }
}