using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootPart : MonoBehaviour
{
    public Transform model;
    public GameObject partPrefab;
    public bool isLast = true;
    public Transform end;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void CreateNextPart(Vector3 vec)
    {
        var nextPart = Instantiate(partPrefab);
        nextPart.transform.position = end.position;
        // nextPart.GetComponent<RootPart>().model.rotation.SetLookRotation(vec);
        nextPart.GetComponent<Joint>().connectedBody = rb;
        // GetComponent<Joint>().connectedBody = nextPart.GetComponent<Rigidbody>();
        isLast = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isLast)
        {
            CreateNextPart(new Vector3(1,1,0));
        }
        
    }
}
