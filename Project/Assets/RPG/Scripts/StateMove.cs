using System.Collections.Generic;
using UnityEngine;
using static RPG.StateRotation;

namespace RPG
{
    public class StateMove : FSM.State
    {
        float horiazontal;
        float vertical;

        public MoveController MoveController;
        public override void OnEnter()
        {
            MoveController.rigidbody.drag = 4;
            Debug.Log("Move State Enter");
        }
        public override void OnUpdate()
        {
            Vector2 move = MoveController.inputActions.Player.Move.ReadValue<Vector2>();

            if (move.x == 0 && move.y == 0)
            {
                MoveController.FSM.HandleEvent((int)TranslationIdleType.Move_To_Idle);
                return;
            }

            horiazontal = move.y;
            vertical = move.x;


            float up = 0;
            float back = 0;// not use
            float left = 0;// not use
            float right = 0;

            //float offset = cameraTransform.eulerAngles.y;

            //Debug.Log("Offset " + offset);

            var value = (int)MoveController.transform.eulerAngles.y;
            // 朝前
            if (value == 0)
            {
                up = horiazontal;
                back = -horiazontal;
                left = -vertical;
                right = vertical;
            }
            // 朝brak
            else if (value == 180)
            {
                up = -horiazontal;
                back = horiazontal;
                left = vertical;
                right = -vertical;
            }
            // 朝 Right
            else if (value == 90)
            {
                up = vertical;
                back = -vertical;
                left = horiazontal;
                right = -horiazontal;
            }
            // 朝 Lefg
            else if (value == 270)
            {
                up = -vertical;
                back = vertical;
                left = -horiazontal;
                right = horiazontal;
            }
            else
            {
                Debug.LogError($"{MoveController.transform.forward}  not forward");
            }


            if (up >= 1)
            {
                float moveAmount = Mathf.Clamp01(Mathf.Abs(horiazontal) + Mathf.Abs(vertical));

                Vector3 targetVelocity = MoveController.transform.forward * moveAmount * MoveController.walkSpeed;
                targetVelocity.y = MoveController.rigidbody.velocity.y;
                MoveController.rigidbody.velocity = targetVelocity;
            }
            else
            {
                if (up == 0)
                {
                    if (right > 0)
                    {
                        MoveController.rotationType = RotationType.Right;
                        Debug.Log("Rotation Right");
                    }
                    else if (right < 0)
                    {
                        MoveController.rotationType = RotationType.Left;
                        Debug.Log("Rotation Left");
                    }
                    else
                    {
                        Debug.LogError("Rotation");
                    }
                }
                else
                {
                    MoveController.rotationType = RotationType.Back;
                    Debug.Log("Rotation Back");
                }

                MoveController.FSM.HandleEvent((int)TranslationIdleType.Move_To_Rotation);
            }
        }
    }
}