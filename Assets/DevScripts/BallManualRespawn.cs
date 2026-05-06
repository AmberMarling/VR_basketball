using UnityEngine;

using UnityEngine;
using UnityEngine.XR;

public class BallManualRespawn : MonoBehaviour
{
    [Header("Ball Reference")]
    public Basketball basketball;

    [Header("Controller Settings")]
    public bool allowRightControllerB = true;
    public bool allowLeftControllerY = true;

    private InputDevice rightController;
    private InputDevice leftController;

    private bool previousRightSecondaryButtonState;
    private bool previousLeftSecondaryButtonState;

    private void Start()
    {
        FindControllers();
    }

    private void Update()
    {
        if (!rightController.isValid || !leftController.isValid)
        {
            FindControllers();
        }

        CheckRightController();
        CheckLeftController();
    }

    private void FindControllers()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    private void CheckRightController()
    {
        if (!allowRightControllerB)
        {
            return;
        }

        if (!rightController.isValid)
        {
            return;
        }

        bool isPressed;

        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressed))
        {
            if (isPressed && !previousRightSecondaryButtonState)
            {
                RespawnBall();
            }

            previousRightSecondaryButtonState = isPressed;
        }
    }

    private void CheckLeftController()
    {
        if (!allowLeftControllerY)
        {
            return;
        }

        if (!leftController.isValid)
        {
            return;
        }

        bool isPressed;

        if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressed))
        {
            if (isPressed && !previousLeftSecondaryButtonState)
            {
                RespawnBall();
            }

            previousLeftSecondaryButtonState = isPressed;
        }
    }

    private void RespawnBall()
    {
        if (basketball == null)
        {
            return;
        }

        basketball.ResetBallNow();
    }
}
