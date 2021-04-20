using UnityEngine;

namespace Core
{
    public class MovementVariable
    {
        public float InputHorizontal;
        public float InputVertical;

        public float MovementLastValue;
        public float MovementValue;

        public float Drag = 4;
        public float MovementSpeed = 2;

        public void UpdateVariable()
        {
            InputHorizontal = Input.GetAxis("Horizontal");
            InputVertical = Input.GetAxis("Vertical");
            MovementValue = Mathf.Clamp01(Mathf.Abs(InputHorizontal) + Mathf.Abs(InputVertical));
        }
    }
}