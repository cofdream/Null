using UnityEngine;
using UnityEngine.EventSystems;

namespace NullNamespace
{
    public class ScrollView : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
        }
    }
}