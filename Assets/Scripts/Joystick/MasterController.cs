using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MasterController : Joystick
{
    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joyType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;

    public float MoveThreshold
    {
        get { return moveThreshold; }
        set { moveThreshold = Mathf.Abs(value); }
    }
    public void SetMode(JoystickType joystickType)
    {
        this.joyType = joystickType;
        if (joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
        {
            background.gameObject.SetActive(false);

        }
    }
    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joyType);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (joyType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (joyType != JoystickType.Fixed)
        {
            background.gameObject.SetActive(false);
        }
        base.OnPointerUp(eventData);
    }
    protected virtual void HandleInout(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (joyType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}
public enum JoystickType
{
    Fixed,
    Floating,
    Dynamic
}
