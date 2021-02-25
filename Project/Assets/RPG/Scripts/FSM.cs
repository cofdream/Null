using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class FSM
    {
        public delegate void TranslationAction();

        public interface IState
        {
            ushort StateId { get; }
            Dictionary<ushort, FSMTranslation> FSMTranslationDic { get; set; }

            void OnEnter();
            void OnUpdate();
            void OnExit();
        }
        public class State : IState
        {
            public virtual ushort StateId { get; set; }
            public virtual Dictionary<ushort, FSMTranslation> FSMTranslationDic { get; set; } = new Dictionary<ushort, FSMTranslation>();
            public virtual void OnEnter() { }
            public virtual void OnUpdate() { }
            public virtual void OnExit() { }

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


        public IState CurrentState { get; private set; }
        Dictionary<ushort, IState> stateDic = new Dictionary<ushort, IState>();

        public void Add(IState state)
        {
            stateDic[state.StateId] = state;
        }
        public void Add(FSMTranslation fsmTranslation)
        {
            stateDic[fsmTranslation.FromState.StateId].FSMTranslationDic[fsmTranslation.TranslationId] = fsmTranslation;
        }

        public void Start(IState state)
        {
            if (CurrentState == null)
            {
                CurrentState = state;
                CurrentState.OnEnter();
            }
        }
        public void Update()
        {
            CurrentState?.OnUpdate();
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

                CurrentState.OnExit();
                CurrentState = fsmTranslation.ToState;
                CurrentState.OnEnter();
            }
        }
    }
}