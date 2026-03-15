//using UnityEngine;
//using UnityEngine.EventSystems;

//public class InventorySlot : MonoBehaviour, IDropHandler
//{
//    public InventoryItem CurrentItem
//    {
//        get
//        {
//            return GetComponentInChildren<InventoryItem>();
//        }
//    }

//    public void OnDrop(PointerEventData eventData)
//    {
//        var dragged = eventData.pointerDrag;
//        if (dragged == null) return;

//        var item = dragged.GetComponent<InventoryItem>();
//        if (item == null) return;

//        if (item.ParentSlot == this) return;

//        InventoryItem other = CurrentItem;

//        if (other != null)
//        {
//            var fromSlot = item.ParentSlot;

//            other.transform.SetParent(fromSlot.transform, false);
//            other.SetAnchoredPositionToZero();
//            other.ParentSlot = fromSlot;
//        }

//        item.transform.SetParent(transform, false);
//        item.SetAnchoredPositionToZero();
//        item.ParentSlot = this;
//    }
//}
