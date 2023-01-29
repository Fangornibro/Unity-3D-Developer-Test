using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<Fruit> fruitPrefabs;
    [HideInInspector]
    public int fruitsLeft;
    private TextMeshProUGUI FruitsLeftText;
    public float TimeBetweenSpawns;
    [SerializeField]
    private QuestSystem qs;
    public Transform spawnedFruitsFolder;
    private int apples, oranges, lemons;
    public void Start()
    {
        for (int i = 0; i < spawnedFruitsFolder.childCount; i ++)
        {
            Destroy(spawnedFruitsFolder.GetChild(i).gameObject);
        }
        StopAllCoroutines();
        FruitsLeftText = GameObject.Find("FruitsLeft").GetComponent<TextMeshProUGUI>();
        apples = qs.apples + Random.Range(1, 4);
        oranges = qs.oranges + Random.Range(1, 4);
        lemons = qs.lemons + Random.Range(1, 4);
        fruitsLeft = apples + oranges + lemons;
        StartCoroutine(FruitSpawn());
    }
    private IEnumerator FruitSpawn()
    {
        for (int i = 0; i < fruitsLeft; fruitsLeft--)
        {
        M1:
            int rand = Random.Range(0, fruitPrefabs.Count);
            if (rand == 2)
            {
                if (oranges == 0)
                {
                    goto M1;
                }
                else
                {
                    oranges--;
                }
            }
            if (rand == 0)
            {
                if (apples == 0)
                {
                    goto M1;
                }
                else
                {
                    apples--;
                }
            }
            if (rand == 1)
            {
                if (lemons == 0)
                {
                    goto M1;
                }
                else
                {
                    lemons--;
                }
            }
            FruitsLeftText.SetText("Fruits left: " + fruitsLeft);
            Instantiate(fruitPrefabs[rand], transform.position + new Vector3(Random.Range(-0.7f, 0.7f), 0, -2), Quaternion.Euler(Vector3.zero), spawnedFruitsFolder);
            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
        FruitsLeftText.SetText("Fruits left: " + fruitsLeft);
    }
}
