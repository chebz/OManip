using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LineAttachment : MonoBehaviour {
    private LineRenderer _renderer;

    public Transform t1, t2;

    public float r1, r2;

    void Start()
    {
        _renderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        float dist = Vector3.Distance(t1.position, t2.position);

        if (dist < r1 + r2)
            _renderer.enabled = false;
        else
        {
            _renderer.enabled = true;

            Vector3 dir = (t2.position - t1.position).normalized;
            Vector3 p1 = t1.position + dir * r1;
            Vector3 p2 = t2.position - dir * r2;
            _renderer.SetPosition(0, p1);
            _renderer.SetPosition(1, p2);
        }
    }
}
