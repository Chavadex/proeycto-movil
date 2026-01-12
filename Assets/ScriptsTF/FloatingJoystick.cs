using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float radius = 100f;

    public Vector2 Direction { get; private set; }

    private Canvas canvas;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        background.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.position.x > Screen.width / 2) return;
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            canvas.worldCamera,
            out pos
        );

        pos = Vector2.ClampMagnitude(pos, radius);
        handle.anchoredPosition = pos;
        Direction = pos / radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        background.gameObject.SetActive(false);
    }
}
