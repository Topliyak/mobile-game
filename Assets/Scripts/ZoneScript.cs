using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public Transform mainHero;

    public Transform[] wayPoints;
    public bool[] dangerousStatusOfPoints;

    public int maxPoliceOfficers;
    public int maxCitizens;
    public int maxGangsters;

    public int startPoliceOfficers;
    public int startCitizens;
    public int startGangsters;

    private bool isMainHeroHere = false;

    private int actuallyPoliceOfficers = 0;
    private int actuallyCitizens = 0;
    private int actuallyGangsters = 0;

    public GameObject[] policeOfficerPrefabs;
    public GameObject[] citizenPrefabs;
    public GameObject[] gangsterPrefabs;

    private List<GameObject> policeOfficers = new List<GameObject>();
    private List<GameObject> citizens = new List<GameObject>();
    private List<GameObject> gangsters = new List<GameObject>();

    private List<GameObject> criminals = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DangerousStatusInitialize();
        WayPointsIndexesInitialize();
        NearPointsIndexesInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        KeepMinimumNPC();
        CheckPersonsForDangerous();
        DoPoliceAttackCriminals();
    }

    void DoPoliceAttackCriminals()
    {
        foreach (GameObject i in policeOfficers)
        {
            PoliceOfficerAsist iPOA = i.GetComponent<PoliceOfficerAsist>();

            if (iPOA.criminal == null)
            {
                GameObject criminalForThatPoliceOfficer = null;
                float minDist = 0;

                foreach (GameObject j in criminals)
                {
                    float dist = Vector3.Distance(i.transform.position, j.transform.position);
                    if (criminalForThatPoliceOfficer == null || dist < minDist)
                    {
                        minDist = dist;
                        criminalForThatPoliceOfficer = j;
                    }
                }

                if (criminalForThatPoliceOfficer != null)
                {
                    iPOA.criminal = criminalForThatPoliceOfficer;
                }
            }
        }
    }

    void CheckPersonsForDangerous()
    {
        criminals.Clear();
        int maxDangerDegree = 1;

        foreach (GameObject i in citizens)
        {
            PersonScript iPS = i.GetComponent<PersonScript>();

            if (iPS.enabled && iPS.dangerDegree == maxDangerDegree)
            {
                criminals.Add(i);
            }
            else if (iPS.enabled && iPS.dangerDegree > maxDangerDegree)
            {
                maxDangerDegree = iPS.dangerDegree;
                criminals.Clear();
                criminals.Add(i);
            }
        }

        foreach (GameObject i in gangsters)
        {
            PersonScript iPS = i.GetComponent<PersonScript>();

            if (iPS.enabled && iPS.dangerDegree == maxDangerDegree)
            {
                criminals.Add(i);
            }
            else if (iPS.enabled && iPS.dangerDegree > maxDangerDegree)
            {
                maxDangerDegree = iPS.dangerDegree;
                criminals.Clear();
                criminals.Add(i);
            }
        }

        if (isMainHeroHere)
        {
            PersonScript iPS = mainHero.GetComponent<PersonScript>();

            if (iPS.enabled && iPS.dangerDegree == maxDangerDegree)
            {
                criminals.Add(mainHero.gameObject);
            }
            else if (iPS.enabled && iPS.dangerDegree > maxDangerDegree)
            {
                maxDangerDegree = iPS.dangerDegree;
                criminals.Clear();
                criminals.Add(mainHero.gameObject);
            }
        }

        if (maxDangerDegree > 1 && criminals.Count != 0)
        {
            UpdateDangerousStatusOfPoints();
        }
    }

    void UpdateDangerousStatusOfPoints()
    {
        for (int i = 0; i < dangerousStatusOfPoints.Length; i++)
        {
            dangerousStatusOfPoints[i] = false;
        }

        foreach (GameObject i in criminals)
        {
            float minDist = -1;
            int indexOfNeares = -1;

            for (int j = 0; j < wayPoints.Length; j++)
            {
                float dist = Vector3.Distance(i.transform.position, wayPoints[j].position);

                if (indexOfNeares == -1 || dist < minDist)
                {
                    minDist = dist;
                    indexOfNeares = j;
                }
            }

            if (indexOfNeares != -1)
            {
                dangerousStatusOfPoints[indexOfNeares] = true;
            }
        }

        List<int> indexesOfPointsWhichMustHaveDangerousStatusTrue = new List<int>();

        for (int i = 0; i < dangerousStatusOfPoints.Length; i++)
        {
            if (!dangerousStatusOfPoints[i])
            {
                continue;
            }

            NodeData nodeData = wayPoints[i].GetComponent<NodeData>();

            foreach (int j in nodeData.nearPointsIndexes)
            {
                indexesOfPointsWhichMustHaveDangerousStatusTrue.Add(j);
            }
        }

        foreach (int i in indexesOfPointsWhichMustHaveDangerousStatusTrue)
        {
            dangerousStatusOfPoints[i] = true;
        }
    }

    void NearPointsIndexesInitialize()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            NodeData node = wayPoints[i].GetComponent<NodeData>();
            node.nearPointsIndexes = new int[node.nearPoints.Length];

            for (int j = 0; j < node.nearPoints.Length; j++)
            {
                for (int k = 0; k < wayPoints.Length; k++)
                {
                    if (wayPoints[k] == node.nearPoints[j])
                    {
                        node.nearPointsIndexes[j] = k;
                    }
                }
            }
        }
    }

    void WayPointsIndexesInitialize()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            NodeData node = wayPoints[i].GetComponent<NodeData>();
            node.nodePointsIndexes = new int[node.nodePoints.Length];

            for (int j = 0; j < node.nodePoints.Length; j++)
            {
                for (int k = 0; k < wayPoints.Length; k++)
                {
                    if (node.nodePoints[j] == wayPoints[k])
                    {
                        node.nodePointsIndexes[j] = k;
                    }
                }
            }
        }
    }

    void DangerousStatusInitialize()
    {
        dangerousStatusOfPoints = new bool[wayPoints.Length];

        for (int i = 0; i < wayPoints.Length; i++)
        {
            dangerousStatusOfPoints[i] = false;
        }
    }

    void KeepMinimumNPC()
    {
        if (actuallyCitizens < startCitizens)
        {
            SpawnNPC(citizenPrefabs, startCitizens - actuallyCitizens);
        }

        if (actuallyGangsters < startGangsters)
        {
            SpawnNPC(gangsterPrefabs, startGangsters - actuallyGangsters);
        }

        if (actuallyPoliceOfficers < startPoliceOfficers)
        {
            SpawnNPC(policeOfficerPrefabs, startPoliceOfficers - actuallyPoliceOfficers);
        }
    }

    void SpawnNPC(GameObject[] prefabs, int numberOfNPC)
    {
        Transform mainHeroIn = wayPoints[0];
        float minDist = Vector3.Distance(mainHero.position, wayPoints[0].position);

        foreach (Transform i in wayPoints)
        {
            if (Vector3.Distance(i.position, mainHero.position) < minDist)
            {
                mainHeroIn = i;
                minDist = Vector3.Distance(mainHero.position, i.position);
            }
        }

        Transform[] spawnPoints = mainHeroIn.GetComponent<NodeData>().hidePoints;

        for (int i = 0; i < numberOfNPC; i++)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPoints[i % spawnPoints.Length].position, Quaternion.Euler(0, 0, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoliceOfficer"))
        {
            actuallyPoliceOfficers++;
            policeOfficers.Add(other.gameObject);
            other.GetComponent<AIControl>().wayPoints = wayPoints;
            other.GetComponent<AIControl>().dangerousStatusOfPoints = dangerousStatusOfPoints;
        }
        else if (other.CompareTag("Citizen"))
        {
            actuallyCitizens++;
            citizens.Add(other.gameObject);
            other.GetComponent<AIControl>().wayPoints = wayPoints;
            other.GetComponent<AIControl>().dangerousStatusOfPoints = dangerousStatusOfPoints;
        }
        else if (other.CompareTag("Gangster"))
        {
            actuallyGangsters++;
            gangsters.Add(other.gameObject);
            other.GetComponent<AIControl>().wayPoints = wayPoints;
            other.GetComponent<AIControl>().dangerousStatusOfPoints = dangerousStatusOfPoints;
        }
        else if (other.CompareTag("MainHero"))
        {
            isMainHeroHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PoliceOfficer"))
        {
            actuallyPoliceOfficers--;
            policeOfficers.Remove(other.gameObject);
        }
        else if (other.CompareTag("Citizen"))
        {
            actuallyCitizens--;
            citizens.Remove(other.gameObject);
        }
        else if (other.CompareTag("Gangster"))
        {
            actuallyGangsters--;
            gangsters.Remove(other.gameObject);
        }
        else if (other.CompareTag("MainHero"))
        {
            isMainHeroHere = false;
        }
    }
}
