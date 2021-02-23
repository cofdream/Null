using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class FSM
    {
        public delegate void TranslationAction();

        public class State
        {
            public ushort StateId;
            public Dictionary<ushort, FSMTranslation> FSMTranslationDic = new Dictionary<ushort, FSMTranslation>();

            public Action Enter;
            public Action Update;
            public Action Exit;
        }

        public class FSMTranslation
        {
            public ushort TranslationId;
            public State FromState;
            public State ToState;
            public TranslationAction Callback;
        }


        public State CurrentState { get; private set; }
        Dictionary<ushort, State> stateDic = new Dictionary<ushort, State>();

        public void Add(State state)
        {
            stateDic[state.StateId] = state;
        }
        public void Add(FSMTranslation fsmTranslation)
        {
            stateDic[fsmTranslation.FromState.StateId].FSMTranslationDic[fsmTranslation.TranslationId] = fsmTranslation;
        }

        public void Start(State state)
        {
            if (CurrentState == null)
            {
                CurrentState = state;
                CurrentState.Enter?.Invoke();
            }
        }
        public void Update()
        {
            CurrentState?.Update?.Invoke();
        }

        public void HandleEvent(ushort translationId)
        {
            if (CurrentState == null)
            {
                Debug.Log("CurrentState == Null,Please call Start Func");
                return;
            }
            if (CurrentState.FSMTranslationDic.TryGetValue(translationId, out FSMTranslation fsmTranslation) == false)
            {
                Debug.LogError($"FSMTranslation id:{translationId} not find");
                return;
            }
            if (fsmTranslation.ToState != CurrentState)
            {
                fsmTranslation.Callback?.Invoke();

                CurrentState.Exit?.Invoke();
                CurrentState = fsmTranslation.ToState;
                CurrentState.Enter?.Invoke();
            }
        }
    }
}