using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public int Horizontal => (snapX) ? SnapFloat((int)input.x, AxisOptions.Horizontal) : (int)input.x;

    public float HandleRange
    {
        set { handleRange = Mathf.Abs(value); }
    }

    public float DeadZone
    {
        set { deadZone = Mathf.Abs(value); }
    }
    
    private Vector2 input = Vector2.zero;
    private RectTransform baseRect;
    private Canvas canvas;
    private Camera cam;
    private float deadZone;
    private float handleRange = 1;
    private float screenWidth;
    private bool snapX = false;

    public enum AxisOptions { Horizontal }
    public AxisOptions axisOptions = AxisOptions.Horizontal;
    
    public InputHandler inputHandler;
    public RectTransform background;
    public RectTransform handle;

    private void Awake()
    {
        screenWidth = Screen.width / 2.0f;
    }

    private void Start()
    {
        ShowJoystick();
        
        HandleRange = handleRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
            {
                input = normalised;
            }
        }
        else
        {
            input = Vector2.zero;
        }
    }

    private void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal)
        {
            input = new Vector2(input.x, 0f);
        }
    }

    private int SnapFloat(int value, AxisOptions snapAxis)
    {
        if (value == 0)
        {
            return value;
        }

        float angle = Vector2.Angle(input, Vector2.up);
        if (snapAxis == AxisOptions.Horizontal)
        {
            if (angle < 22.5f || angle > 157.5f)
            {
                return 0;
            }
        }
        
        return value;
    }

    private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }

        return Vector2.zero;
    }
    
    private void ShowJoystick()
    {
        background.gameObject.SetActive(true);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        HideJoystick();
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShowJoystick();
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;

            if (touch.phase == TouchPhase.Moved)
            {
                OnDrag(eventData); 
            }
            else if (touch.phase == TouchPhase.Began)
            {
                if (touchPosition.x >= screenWidth)
                {
                    inputHandler.MoveRight();
                } 
                else if (touchPosition.x < screenWidth)
                {
                    inputHandler.MoveLeft();          
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            cam = canvas.worldCamera;
        }

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        
        handle.anchoredPosition = input * radius * handleRange;
    }

    public void HideJoystick()
    {
        background.gameObject.SetActive(false);
    }
}