using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonColliderSize2D : MonoBehaviour
{
    PolygonCollider2D polyCollider;
    // Start is called before the first frame update
    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        Bounds colliderBounds = polyCollider.bounds;

        Debug.Log("The Width Of The Level Is " + colliderBounds.size.x + "\nThe Height Of The Level Is " + colliderBounds.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
