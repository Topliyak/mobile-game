using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperData : MonoBehaviour
{
    public bool frontFree = false;
    public bool backFree = false;
    public bool rightFree = false;
    public bool leftFree = false;

    private void OnDrawGizmosSelected()
    {
        Color green = Color.green;
        Color red = Color.red;

        Debug.DrawRay(transform.TransformPoint(Vector3.right / 2), transform.right, rightFree ? green : red);
        Debug.DrawRay(transform.TransformPoint(Vector3.left / 2), -transform.right, leftFree ? green : red);
        Debug.DrawRay(transform.TransformPoint(Vector3.back / 2), -transform.forward, backFree ? green : red);
        Debug.DrawRay(transform.TransformPoint(Vector3.forward / 2), transform.forward, frontFree ? green : red);
    }

    public bool IsFree(int angle)
    {
        if (angle == 0 && backFree)
        {
            return true;
        }

        if (angle == 90 && leftFree)
        {
            return true;
        }

        if (angle == -90 && rightFree)
        {
            return true;
        }

        if (angle == 180 && frontFree)
        {
            return true;
        }

        return false;
    }
}
