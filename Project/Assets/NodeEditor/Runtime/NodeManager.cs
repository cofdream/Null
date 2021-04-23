using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DA.Node
{
    public class NodeManager : MonoBehaviour
    {
        public static NodeManager Instance { get; private set; }

        [SerializeField] private EventTrigger eventTrigger;

        private void Awake()
        {
            Instance = this;

            eventTrigger.triggers.Add(new EventTrigger.Entry() { eventID = EventTriggerType.PointerDown, callback = new EventTrigger.TriggerEvent(), });
        }

        private void OnValidate()
        {
            eventTrigger = GetComponent<EventTrigger>();
        }

    }

}