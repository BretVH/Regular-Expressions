using UnityEngine;
using System.Collections;

public class WaypointScritp : MonoBehaviour
{

    bool used;
    public bool GetUsed()
    {
        return used;
    }
    public void SetUsed(bool b)
    {
        used = b;
    }
    public float radius = 0.5f;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
