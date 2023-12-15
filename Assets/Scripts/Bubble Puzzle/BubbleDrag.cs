using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BubbleDrag : MonoBehaviour, IDragAndDrop
{
    [SerializeField]
    private BubbleShoot _bubbleShoot;

    [SerializeField]
    private Transform _firePosition;

    private EventTrigger _EventTrigger;

    private event Action<BaseEventData> _BeginDragEvent;
    private event Action<BaseEventData> _OnDragEvent;
    private event Action<BaseEventData> _EndDragEvent;

    private Vector2 _direction;


    private void Awake()
    {
        // GetComponent
        _EventTrigger = GetComponent<EventTrigger>();

        // Add Event
        _BeginDragEvent += BeginDrag;
        _OnDragEvent += OnDrag;
        _EndDragEvent += EndDrag;

        // Add or replace the existing OnBeginDrag listener
        AddEventTriggerListener(EventTriggerType.BeginDrag, BeginDrag);
        AddEventTriggerListener(EventTriggerType.Drag, OnDrag);
        AddEventTriggerListener(EventTriggerType.EndDrag, EndDrag);
    }

    public void BeginDrag(BaseEventData baseEventData)
    {
        _bubbleShoot.LineRenderSetActive(true);
    }

    public void OnDrag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);

        _direction = (mousePosition - _firePosition.position).normalized;
        _bubbleShoot.DrawLine(_firePosition.position, _direction);
    }

    public void EndDrag(BaseEventData baseEventData)
    {
        _bubbleShoot.LineRenderSetActive(false);
        _bubbleShoot.InstantiateBall(_direction);
    }

    private void AddEventTriggerListener(EventTriggerType eventTriggerType, UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback.AddListener(callback);
        _EventTrigger.triggers.Add(entry);
    }
}
