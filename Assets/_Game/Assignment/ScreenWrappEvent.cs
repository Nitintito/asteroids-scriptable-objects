using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace.GameEvents;

public class ScreenWrappEvent : MonoBehaviour
{
    [SerializeField] private GameEvent _onScreenExitEvent;
    [SerializeField] Renderer[] ShipRenderers;

    Camera mainCam;
    Vector3 shipPosition;
    bool wrappingX = false;
    bool wrappingY = false;

    private void Start()
    {
        mainCam = Camera.main;
        ShipRenderers = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (!InsideCamera())
        {
            _onScreenExitEvent.Raise();
        }
    }

    public void ScreenWrap()
    { 
        if (wrappingX && wrappingY)
        {
            return;
        }

        shipPosition = mainCam.WorldToViewportPoint(transform.position);
        Vector3 position = transform.position;

        if (!wrappingX && (shipPosition.x > 1 || shipPosition.x < 0))
        {
            position.x = -position.x;
            wrappingX = true;
        }
        if (!wrappingY && (shipPosition.y > 1 || shipPosition.y < 0))
        {
            position.y = -position.y;
            wrappingY = true;
        }

        transform.position = position;
    }

    bool InsideCamera()
    {
        foreach (Renderer renderer in ShipRenderers)
        {
            if (renderer.isVisible)
            {
                wrappingX = false;
                wrappingY = false;
                return true;
            }
        }
        return false;
    }
}
