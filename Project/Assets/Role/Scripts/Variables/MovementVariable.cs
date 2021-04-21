using UnityEngine;

namespace DevTool
{
    [System.Serializable]
    public class MovementVariable
    {
        public float InputHorizontal;
        public float InputVertical;

        public float MovementValue;

        public void UpdateVariable()
        {
            InputHorizontal = Input.GetAxis("Horizontal");
            InputVertical = Input.GetAxis("Vertical");

            MovementValue = Mathf.Clamp01(Mathf.Abs(InputHorizontal) + Mathf.Abs(InputVertical));
        }
    }
}