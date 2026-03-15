//using UnityEngine;
//using UnityEngine.EventSystems;

//[RequireComponent(typeof(RectTransform))]
//public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
//{
//    [HideInInspector] public InventorySlot ParentSlot;

//    private RectTransform rect;
//    private Canvas canvas;
//    private CanvasGroup canvasGroup;

//    private Transform originalParent;

//    void Awake()
//    {
//        rect = GetComponent<RectTransform>();
//        canvas = GetComponentInParent<Canvas>();

//        canvasGroup = GetComponent<CanvasGroup>();
//        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
//    }

//    void Start()
//    {
//        ParentSlot = GetComponentInParent<InventorySlot>();
//        SetAnchoredPositionToZero();
//    }

//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        originalParent = transform.parent;

//        transform.SetParent(canvas.transform, true);

//        canvasGroup.blocksRaycasts = false;
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        if (transform.parent == canvas.transform)
//        {
//            transform.SetParent(originalParent, false);
//            SetAnchoredPositionToZero();
//        }

//        canvasGroup.blocksRaycasts = true;
//    }

//    public void SetAnchoredPositionToZero()
//    {
//        rect.anchorMin = new Vector2(0.5f, 0.5f);
//        rect.anchorMax = new Vector2(0.5f, 0.5f);
//        rect.pivot = new Vector2(0.5f, 0.5f);
//        rect.anchoredPosition = Vector2.zero;
//        rect.localRotation = Quaternion.identity;
//        rect.localScale = Vector3.one;
//    }
//}
