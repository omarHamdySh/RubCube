using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float x_offset = -1.52f;
    public float y_offset = -3.47f;
    public float z_offset = 4.31f;
    Vector3 camOffset = new Vector3 (-1.52f, -3.47f, 4.31f);
    Vector3 camRotationOffset = new Vector3 (20, -21, 0);
    Vector3 forwardCam;
    Vector3 offsetCamToSeeTheStack;
    bool followThestack = false;

    // Start is called before the first frame update
    void Start ()
    {
        if (LevelManager.Instance.currentWalkingCharacter.gameObject) player = LevelManager.Instance.currentWalkingCharacter.gameObject;
        y_offset = 4;

    }

    void Update ()
    {

        /*if (player != null)
        {
            // Temporary vector
            Vector3 temp = player.transform.position;

            temp.x = transform.position.x;
            temp.y = transform.position.y;
            temp.z -= z_offset;

            // Assign value to Camera position
            transform.position = temp;

        }*/
    }

    [ContextMenu ("Dynamic Follow")]
    public void FollowStackOfPlayers ()
    {
        /*print("+++" + LevelManager.Instance.mainStack.transform.childCount);
        transform.rotation = Quaternion.Euler(camRotationOffset);
        // transform.position = camOffset;
        Vector3 camForward = transform.forward;
        offsetCamToSeeTheStack = Vector3.zero;
        int Stackheight = LevelManager.Instance.mainStack.transform.childCount;

        if (Stackheight > 1)
        {
            offsetCamToSeeTheStack = (camForward * Stackheight);
            offsetCamToSeeTheStack.y = LevelManager.Instance.mainStack.transform.GetChild(1).position.y;
            z_offset = offsetCamToSeeTheStack.z * 1.5f;
            z_offset = Mathf.Clamp(z_offset, 0, 15f);

            y_offset = transform.position.y + offsetCamToSeeTheStack.y;
            y_offset = Mathf.Clamp(y_offset, 4f, 12f);

            // transform.position += offsetCamToSeeTheStack;
        }*/

    }

}