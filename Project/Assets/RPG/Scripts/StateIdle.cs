using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class StateIdle : FSM.State
    {
        public MoveController MoveController;

        public override void OnUpdate()
        {
            Vector2 move = Vector2.zero;//MoveController.inputActions.Player.Move.ReadValue<Vector2>();

            if (move.x != 0 || move.y != 0)
            {
                MoveController.FSM.HandleEvent((int)TranslationIdleType.Idle_To_Move);
            }
        }
    }
}