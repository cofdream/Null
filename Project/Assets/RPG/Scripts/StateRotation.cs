using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class StateRotation : FSM.State
    {
        public enum RotationType
        {
            None = 0,
            Back,
            Left,
            Right,
        }
        public MoveController MoveController;

        float time;
        Quaternion targetQuaternion;

        public override void OnEnter()
        {
            switch (MoveController.rotationType)
            {
                case RotationType.Back:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, 180, 0)) * MoveController.transform.rotation;
                    break;
                case RotationType.Left:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, -90, 0)) * MoveController.transform.rotation;
                    break;
                case RotationType.Right:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, 90, 0)) * MoveController.transform.rotation;
                    break;
                default:
                    targetQuaternion = Quaternion.Euler(new Vector3(0, 0, 0)) * MoveController.transform.rotation;
                    break;
            }

            Debug.LogWarning((MoveController.transform.rotation * targetQuaternion).eulerAngles);

            Debug.Log("Rotation State Enter");
        }
        public override void OnUpdate()
        {
            time += Time.deltaTime * MoveController.cameraSpeed;
            if (time >= 1)
            {
                time = 0;

                MoveController.transform.rotation = targetQuaternion;
                MoveController.FSM.HandleEvent((int)TranslationIdleType.Rotation_To_Idle);

                Debug.Log("Rotation end...");
            }
            else
            {
                // 需要反方向旋转，交换参数a，b的位置
                Quaternion targetRotation = Quaternion.Lerp(MoveController.transform.rotation, targetQuaternion, time);
                MoveController.transform.rotation = targetRotation;

                Debug.Log("Rotationing...");
            }
        }
    }
}
