using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public Transform map;
    public Transform location;
    public Transform anchor;


    // Start is called before the first frame update
    void Start()
    {
        //map.parent = anchor;
        //map.localPosition = Vector3.zero;
        //map.localRotation= Quaternion.Euler(90f, 0f, 0f);
        //map.parent = null;

        //map.position = anchor.position;
        //map.localEulerAngles = anchor.localEulerAngles;

        // map.Rotate(90f, 0, 0, Space.Self);

        //map.position = map.position - location.localPosition;

        //location.parent = null;
        //map.parent = location;
        //location.position = anchor.position;
        //map.parent = null;
        //location.parent = map;

        // location.parent = null;
        // map.parent = location;
        // location.position = anchor.position;
        // location.eulerAngles = anchor.eulerAngles;
        // map.parent = null;
        // location.parent = map;


        map.position = anchor.position;
        map.eulerAngles = anchor.eulerAngles;
        //map.Rotate(90f, 0, 0, Space.Self);
        map.position = map.position - location.localPosition;




    }




}
