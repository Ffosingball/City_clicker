using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementManager : MonoBehaviour
{
    public Camera camera;
    public float zoomSpeed = 0.1f;
    public float zoomSpeedPhone = 0.1f;
    public float minZoom = 10f;
    public float maxZoom = 60f;
    public float dragSpeed = 0.005f;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float movementDivider=6f;
    public float movementDividerPhone=6f;
    private Vector3 lastTouchPosition;
    [HideInInspector]
    public bool isDragging;

    private void Update()
    {

        if (Input.touchCount == 1 && !IsPointerOverUIObject())
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector3 currentTouchPosition = touch.position;
                Vector3 delta = (lastTouchPosition - currentTouchPosition)/(movementDividerPhone/(camera.orthographicSize/100));

                Vector3 newPosition = camera.transform.position + delta;

                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

                camera.transform.position = newPosition;

                lastTouchPosition = currentTouchPosition;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
        else if (Input.touchCount == 2 && !IsPointerOverUIObject())
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (camera.orthographic == false)
            {
                camera.fieldOfView += deltaMagnitudeDiff * zoomSpeedPhone;
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minZoom, maxZoom);
            }
            else
            {
                camera.orthographicSize += deltaMagnitudeDiff * zoomSpeedPhone;
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoom, maxZoom);
            }
        }
        else if (Input.mouseScrollDelta.y != 0)
        {
            camera.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed * 10f;
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoom, maxZoom);
        }


        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            lastTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentTouchPosition = Input.mousePosition;
            Vector3 delta = (lastTouchPosition - currentTouchPosition)/(movementDivider/(camera.orthographicSize/100));

            if (delta.magnitude > 1f) 
            {
                Vector3 newPosition = camera.transform.position + delta;
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

                camera.transform.position =  newPosition;

                lastTouchPosition = currentTouchPosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }


    private bool IsPointerOverUIObject()
    {
        // For mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        // For touch input
        if (Input.touchCount > 1)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            return EventSystem.current.IsPointerOverGameObject(touch1.fingerId) && EventSystem.current.IsPointerOverGameObject(touch2.fingerId);
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }

        return false;
    }
}
