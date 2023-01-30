using System;
using UnityEngine;

public class RootControl : MonoBehaviour
{

    public RootPart baseRootPart;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var part = baseRootPart;
                while (part.next != null)
                {
                    part = part.next;
                }

                part = part.CreateNextPart(hit.point+ Vector3.up);

                // print(part.name);


                float size = 0.1f;
                float rate = 0.1f;

                while (part.old != null)
                {
                    var f = Mathf.Min(size, 1);
                    part.transform.localScale = new Vector3(f, f, 1);
                    size += rate;
                    part = part.old;
                }
            }
        }
    }


}
