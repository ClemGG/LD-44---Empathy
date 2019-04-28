using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CamMovement : MonoBehaviour
{
    public Transform[] limits;
    public Transform player;

    Transform t;


    private void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 v = player.position;
        v.z = -10f;
        v.x = Mathf.Clamp(v.x, limits[0].position.x, limits[1].position.x);
        t.position = v;
    }
}
