using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeBehaviour : MonoBehaviour
{

    Rigidbody rb;
    public float jumpForce = 10f;
    public float time = 1f;
    public bool loseGame;
    [SerializeField]
    private LineRenderer line;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loseGame)
        {
            if (Input.GetMouseButton(0) && Mathf.Approximately(rb.velocity.x, 0) && Mathf.Approximately(rb.velocity.y, 0))
            {
                line.positionCount = (int)(time * 10);
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
                line.SetPosition(0, transform.position);
                Vector3 startVelocity = (transform.position - mousePosition) * jumpForce;
                if (startVelocity.x < 0 && startVelocity.y > 0 && startVelocity.z > 0)
                {
                    PlotTrajectory(transform.position, startVelocity, time / 10, time);
                }
                else
                {
                    line.positionCount = 0;
                }
            }
            if (Input.GetMouseButtonUp(0) && line.positionCount > 0)
            {
                line.positionCount = 0;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
                rb.AddForce((transform.position - mousePosition) * jumpForce, ForceMode.Impulse);
            }
        }
    }

    public void PlotTrajectory(Vector3 start, Vector3 startVelocity, float timestep, float maxTime)
    {
        Vector3 prev = start;
        for (int i = 0; i < line.positionCount; i++)
        {   
            float t = timestep * i;
            if (t > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            if (Physics.Linecast(prev, pos)) break;
            line.SetPosition(i, pos);
            prev = pos;
        }
    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

}
