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

    public FixedJoint joint;

    public RootPart next = null;
    public RootPart old = null;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // joint.autoConfigureConnectedAnchor = true;
    }


    public RootPart CreateNextPart(Vector3 targetPos)
    {
        var nextPart = Instantiate(partPrefab);
        nextPart.transform.rotation = Quaternion.LookRotation(targetPos - transform.position);
        nextPart.transform.position = end.position;
        nextPart.GetComponent<Joint>().connectedBody = rb;
        isLast = false;
        print(end.rotation);
        var nextPartRootPart = nextPart.GetComponent<RootPart>();
        next = nextPartRootPart;
        next.old = this;
        return nextPartRootPart;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W) && isLast)
        // {
        //     CreateNextPart(new Vector3(1, 1, 0));
        // }
    }
}
