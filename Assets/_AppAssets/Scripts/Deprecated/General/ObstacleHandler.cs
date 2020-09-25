using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [Header("IgnoreCollision")]
    [SerializeField] private bool forword;
    [SerializeField] private bool backword, right, left, up, down;

    private bool isCollide;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Player") && !isCollide)
        {
            Vector3 normal = collision.contacts[0].normal;

            if (normal == -(transform.forward) && forword)
            {
                return;
            }
            if (normal == transform.forward && backword)
            {
                return;
            }
            if (normal == -(transform.right) && right)
            {
                return;
            }
            if (normal == transform.right && left)
            {
                return;
            }
            if (normal == -(transform.up) && up)
            {
                return;
            }
            if (normal == transform.up && down)
            {
                return;
            }



            isCollide = true;
            //LevelManager.Instance.mainStack.ApplyObstacleHit();
            LevelManager.Instance.RemoveLastPlayer(true);
        }
    }
}
