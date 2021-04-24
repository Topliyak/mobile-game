using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILogScript : MonoBehaviour
{
    public Transform player;
    public AIControl controller;
    public Transform cube;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Policeman").transform;
        controller = GameObject.Find("Policeman").GetComponent<AIControl>();
        cube = GameObject.Find("Cube").transform;
        //controller.target = cube;
    }

    // Update is called once per frame
    void Update()
    {
        //LogPrint();

        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            cube.position = hit.point;
        }

    }

    void LogPrint()
    {
        Vector3 move = cube.position - player.position;
        move = player.InverseTransformPoint(cube.position);

        print(Mathf.Atan2(move.z, move.x) * 180 / Mathf.PI);
    }
}
