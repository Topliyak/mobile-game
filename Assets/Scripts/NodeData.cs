using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData : MonoBehaviour
{
    public Transform[] nodePoints;
    public Transform[] nearPoints;
    public Transform[] hidePoints;

    [HideInInspector]
    public int[] nodePointsIndexes;
    [HideInInspector]
    public int[] nearPointsIndexes;
}
