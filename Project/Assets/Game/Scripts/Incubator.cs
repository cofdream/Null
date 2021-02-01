using UnityEngine;

namespace Game
{
    public class Incubator
    {
        private Vector3 bornPosition;
        private Quaternion bornRotation;

        private Soldier soldier;

        private DA.Timer.TimerDisposable timer;

        public Incubator(Vector3 bornPosition, Quaternion bornRotation, Soldier soldier)
        {
            this.bornPosition = bornPosition;
            this.bornRotation = bornRotation;
            this.soldier = soldier;

            timer = new DA.Timer.TimerDisposable(1f, Update);
        }

        public void Update()
        {
            timer.Update(Time.deltaTime);
        }
    }
}