using UnityEngine;
using Cinemachine;

public class DoorPeekController : MonoBehaviour
{
    public Transform peekTarget;
    public Transform lookTarget;
    public float transitionSpeed = 5f;

    private CinemachineVirtualCamera activeVCam;
    private Transform originalFollow;
    private Transform originalLookAt;
    private bool isPeeking = false;

    void Update()
    {
        if (doorMechanic.instance.isDragging && !isPeeking)
        {
            StartPeek();
        }
        else if (!doorMechanic.instance.isDragging && isPeeking)
        {
            EndPeek();
        }
    }

    void StartPeek()
    {
        activeVCam = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;

        if (activeVCam == null || peekTarget == null)
            return;

        originalFollow = activeVCam.Follow;
        originalLookAt = activeVCam.LookAt;

        activeVCam.Follow = peekTarget;
        activeVCam.LookAt = lookTarget;

        isPeeking = true;
    }

    void EndPeek()
    {
        if (activeVCam == null)
            return;

        activeVCam.Follow = originalFollow;
        activeVCam.LookAt = originalLookAt;

        isPeeking = false;
    }
}
