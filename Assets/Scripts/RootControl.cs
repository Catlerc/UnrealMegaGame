using System;
using System.Collections;
using UnityEngine;

public class RootControl : MonoBehaviour
{

    public RootPart baseRootPart;

    private Coroutine currentAnimation;

    private void Update()
    {
        // anim.
        if (Input.GetMouseButton(0) && currentAnimation == null)
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

                part = part.CreateNextPart(hit.point + Vector3.up);

                // print(part.name);


                float size = 0.1f;
                float rate = 0.1f;

                currentAnimation = StartCoroutine(InflationAnim(part, 0.3f, 5));
            }
        }
    }

    private IEnumerator InflationAnim(RootPart lastPart, float t, int d)
    {
        var lastId = lastPart.id;


        var start = Time.time;
        var end = start + t;
        while (Time.time < end)
        {
            var part = lastPart;


            while (part.prev != null)
            {
                var endScale = Mathf.Min(((float)lastId - (part.id - 1) + 1) / (d + 1), 1);
                var startScale = Mathf.Min(((float)lastId - part.id + 1) / (d + 1), 1);
                
                
                var f = Mathf.Lerp(startScale, endScale,  (t-(end - Time.time))/t);
                Debug.Log(f);
                part.model.localScale = new Vector3(f, f, 1);
                part = part.prev;
            }

            var lastScale = lastPart.model.localScale;
            lastScale.z = (t-(end - Time.time))/t;
            
            lastPart.model.localScale = lastScale;

            yield return null;
        }


        currentAnimation = null;
    }

    private IEnumerator DeflationAnim(RootPart lastPart)
    {
        var lastId = lastPart.id;

        var part = lastPart;
        while (part.prev != null)
        {
            var f = (lastId - part.id) / lastId;
            part.transform.localScale = new Vector3(f, f, 1);
            part = part.prev;
            yield return null;
        }

        currentAnimation = null;
    }

}
