using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apply_Force : MonoBehaviour
{
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig=GetComponent<Rigidbody>();
        rig.AddForce(Vector2.right, ForceMode.Force);
                  

    }

}
