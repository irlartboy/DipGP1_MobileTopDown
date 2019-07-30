using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, 
    IPointerDownHandler,IDragHandler, IPointerUpHandler

{
    #region SerilizeFields
    [SerializeField]
    public float handlerRange = 1;
    [SerializeField]
    private float deadZone = 0;
    [SerializeField]
    private AxisOptions aixsOps = AxisOptions.Both;
    [SerializeField]
    private bool snapX = false;
    [SerializeField]
    private bool snapY = false;
    [SerializeField]
    private RectTransform background = null;
    [SerializeField]
    private RectTransform handle = null;
    #endregion
    #region Refs
    private RectTransform baseRect = null;
    private Canvas canvas;
    private Camera cam;
    public Vector2 input = Vector2.zero;
    #endregion
    #region Properties
    public float Horizontal
    {
        get { return (snapX) ? SnapFloat(input.x) : input.x; }
    }
    #endregion
    #region Functions

    #endregion
    #region Interface
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
        HandleInput(input.magnitude, input.normalized, radius,cam);
        handle.anchoredPosition = input * radius * handlerRange;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
    #endregion
}
public enum AxisOptions
{
    Both,
    Horizontal,
    Vertical
}
