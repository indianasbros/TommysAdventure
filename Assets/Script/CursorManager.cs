using UnityEngine;
using Cinemachine;

public class CursorManager : MonoBehaviour
{
    public CinemachineVirtualCamera mainGameplayCam;
    public CinemachineBrain brain;

    void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
        mainGameplayCam.Priority = 100; // fuerza que esta sea la primera
        ForceCursorUpdate();
    }

    void Update()
    {
        ForceCursorUpdate();
    }
     void ForceCursorUpdate()
    {
        if (mainGameplayCam == null || brain == null)
        {
            Debug.LogWarning("Main Gameplay Camera or Cinemachine Brain is not assigned.");
            return;
        }

        ICinemachineCamera activeICam = brain.ActiveVirtualCamera;

        if (activeICam == null)
        {
            Debug.Log("No active virtual camera.");
            return;
        }

        // Si es una StateDrivenCamera, accedemos a su cámara activa real
        if (activeICam is CinemachineStateDrivenCamera stateCam)
        {
            var child = stateCam.LiveChild as CinemachineVirtualCamera;

            if (child != null && child == mainGameplayCam)
            {
                LockCursor(true);
            }
            else
            {
                LockCursor(false);
            }
        }
        else if (activeICam is CinemachineVirtualCamera virtualCam)
        {
            if (virtualCam == mainGameplayCam)
            {
                LockCursor(true);
            }
            else
            {
                LockCursor(false);
            }
        }
    }
    void LockCursor(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}