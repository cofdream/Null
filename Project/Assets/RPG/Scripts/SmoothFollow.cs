//SmoothFollow.cs
//C#
using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;        //GameObject that we should follow
    public float distance;            //how far back?
    public float maxDriftRange;        //how far are we allowed to drift from the target position
    public float angleX;            //angle to pitch up on top of the target
    public float angleY;            //angle to yaw around the target

    private Transform m_transform_cache;    //cache for our transform component
    private Transform myTransform
    {//use this instead of transform
        get
        {//myTransform is guarunteed to return our transform component, but faster than just transform alone
            if (m_transform_cache == null)
            {//if we don't have it cached, cache it
                m_transform_cache = transform;
            }
            return m_transform_cache;
        }
    }

    //this runs when values are changed in the inspector
    void OnValidate()
    {
        if (target != null)
        {//we have a target, move the camera to target position for preview purposes
            Vector3 targetPos = GetTargetPos();
            //update position
            myTransform.position = targetPos;
            //look at our target
            myTransform.LookAt(target);
        }
    }

    //this runs every frame, directly after Update
    void LateUpdate()
    {//use this so that changes are immediate after the object has been affected

        Vector3 targetPos = GetTargetPos();
        //calculate drift theta
        float t = Vector3.Distance(myTransform.position, targetPos) / maxDriftRange;

        //smooth camera position using drift theta
        myTransform.position = Vector3.Lerp(myTransform.position, targetPos, t * Time.deltaTime);
        //look at our target
        myTransform.LookAt(target);
    }

    void OnDrawGizmos()
    {//this is for editor purpose only, shows the relationship between this script and target as a line
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(myTransform.position, target.position);
    }

    private Vector3 GetTargetPos()
    {//returns where the camera should aim to be
        //opposite of (-forward) * distance
        Vector3 targetPos = new Vector3(0, 0, -distance);
        //calculate pitch and yaw
        targetPos = Quaternion.Euler(angleX, angleY, 0) * targetPos;
        //return angled target position relative to target.position
        return target.position + (target.rotation * targetPos);
    }
}