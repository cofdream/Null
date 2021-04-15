using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace  NullNamespace
{
    public class ScrollItem : UIBehaviour, IPointerClickHandler
    {


        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
        }



    }
}