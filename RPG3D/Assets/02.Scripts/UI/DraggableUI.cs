using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 _originalLocalPointerPosition;
    private Vector3 _originalPanelLocalPosition;
    private RectTransform _panelRectTransform;
    private RectTransform _parentRectTransform;

    public void OnPointerDown(PointerEventData eventData)
    {
        _originalLocalPointerPosition = _panelRectTransform.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform,
                                                                eventData.position,
                                                                eventData.pressEventCamera,
                                                                out _originalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_panelRectTransform == null || _parentRectTransform == null)
            return;

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform,
                                                                    eventData.position,
                                                                    eventData.pressEventCamera,
                                                                    out localPointerPosition))
        {
            _panelRectTransform.localPosition = _originalPanelLocalPosition + (Vector3)(localPointerPosition - _originalLocalPointerPosition);
        }

        ClampWindow();
    }

    private void Awake()
    {
        _panelRectTransform = transform.parent as RectTransform;
        _parentRectTransform = _panelRectTransform.parent as RectTransform;
    }

    private void ClampWindow()
    {
        Vector3 pos = _panelRectTransform.localPosition;

        Vector3 min = _parentRectTransform.rect.min - _panelRectTransform.rect.min;
        Vector3 max = _parentRectTransform.rect.max - _panelRectTransform.rect.max;

        pos.x = Mathf.Clamp(_panelRectTransform.localEulerAngles.x, min.x, max.x);
        pos.y = Mathf.Clamp(_panelRectTransform.localEulerAngles.y, min.y, max.y);

        _panelRectTransform.localPosition = pos;
    }
}
