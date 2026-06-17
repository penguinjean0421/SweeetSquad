using UnityEngine;
using Unity.Cinemachine; // 유니티 6의 시네머신 네임스페이스

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public bool isCameraLocked = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isCameraLocked = !isCameraLocked;
        }

        if (isCameraLocked)
        {
            cinemachineCamera.enabled = false;
        }
        else
        {
            cinemachineCamera.enabled = true;
        }
    }
}