using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test : Editor
{


    [MenuItem("MyMenu/Do Something")]
    static void DoSomething()
    {
        Debug.Log("Doing Something...");
        
        Debug.Log(GameObject.Find("QRCode"));
        GameObject.Find("QRCode").SetActive(false);
    }


}
