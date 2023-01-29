using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSystem : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private Conveyor conveyorPrefab;
    [Header("Points")]
    [SerializeField]
    private Transform SpawnPoint;
    [SerializeField]
    private Transform RemovePoint;
    [SerializeField]
    private Conveyor curConveyor, firstConveyor;
    [HideInInspector]
    public float plusSpeed = 0;
    public void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name != "SpawnPoint" && transform.GetChild(i).name != "RemovePoint")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        firstConveyor = null;
        curConveyor = Instantiate(conveyorPrefab, transform.position + new Vector3(0, 0, 3), Quaternion.Euler(Vector3.zero), transform);
        curConveyor.speed += plusSpeed;
        ConveyorCreation();
    }
    void Update()
    {
        if (curConveyor.transform.position.z - conveyorPrefab.transform.localScale.z/2 + 1 >= SpawnPoint.position.z)
        {
            ConveyorCreation();
        }
        if (firstConveyor != null)
        {
            if (firstConveyor.transform.position.z + conveyorPrefab.transform.localScale.z / 2 >= RemovePoint.position.z)
            {
                Destroy(firstConveyor.gameObject);
            }
        }
    }
    private void ConveyorCreation()
    {
        firstConveyor = curConveyor;
        curConveyor = Instantiate(conveyorPrefab, SpawnPoint.position - new Vector3(0, 0, conveyorPrefab.transform.localScale.z / 2), Quaternion.Euler(Vector3.zero), transform);
        curConveyor.speed += plusSpeed;
    }
}
