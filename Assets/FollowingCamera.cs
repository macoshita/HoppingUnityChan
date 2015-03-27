using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour {

    public Transform target;

    void LateUpdate()
    {
        if (!target)
        {
            return;
        }

        //Debug.Log(target.position.x + "," + target.position.y);

        transform.position = new Vector3(
            target.position.x + 10,
            5,
            target.position.z);
    }
}
