using System;
using UnityEngine;
using UnityEngine.Internal;

namespace RPG
{
    public class FollowTargetMove : MonoBehaviour
    {
        [SerializeField] Transform followTarget;
        //[SerializeField] float smoothSpeed = 0.35f;
        //[SerializeField] float maxSpeed = 1f;
        Vector3 offest;
        //Vector3 currentVelocity;
        //private Vector3 lastPosition;
        private void Awake()
        {
            offest = transform.position - followTarget.position;
        }
        float t;
        private void LateUpdate()
        {
            transform.localPosition = followTarget.position + offest;

            //Vector3 position = followTarget.position + offest;

            //float distance = Vector3.Distance(transform.localPosition, position);

            ////if (distance.Equals(0))
            ////{
            ////    return;
            ////}

            ////transform.localPosition = SmoothDamp(transform.localPosition, position, ref currentVelocity, smoothSpeed, maxSpeed, Time.deltaTime);

            ////Debug.Log("last " + distance);

            ////Debug.Log( "new " + Vector3.Distance(position, currentVelocity));

            //if (lastPosition == position)
            //{
            //    //Mathf.Clamp()
            //}

            //t += Time.deltaTime;
            //transform.localPosition = Vector3.Lerp(transform.position, position, t);
            //lastPosition = position;
        }
    }
}