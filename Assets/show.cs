using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show : MonoBehaviour
{
    public Vector3 location;
    public Vector3 world;


    // Update is called once per frame
    void Update()
    {
        location = transform.localPosition;
        world = transform.position;

    }
}
