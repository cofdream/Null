using UnityEngine;
using static RPG.FiniteStateMachine;

namespace RPG
{
	public class Conditions
	{
        public ushort TranslationId;
        public State FromState;
        public State ToState;
        public TranslationAction Callback;
    }
}