using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }

public class CollisionDirection : MonoBehaviour
{
    public static HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {

        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {

            if (MyRayHit.collider != null)
            {

                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.up) { hitDirection = HitDirection.Top; }
                else if (MyNormal == -MyRayHit.transform.up) { hitDirection = HitDirection.Bottom; }
                if (MyNormal == MyRayHit.transform.forward) { hitDirection = HitDirection.Forward; }
                else if (MyNormal == -MyRayHit.transform.forward) { hitDirection = HitDirection.Back; }
                if (MyNormal == MyRayHit.transform.right) { hitDirection = HitDirection.Right; }
                else if (MyNormal == -MyRayHit.transform.right) { hitDirection = HitDirection.Left; }
            }
        }
        return hitDirection;
    }
}
