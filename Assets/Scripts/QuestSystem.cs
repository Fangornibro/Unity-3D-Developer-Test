using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public TextMeshPro questText;
    [HideInInspector]
    public int apples, oranges, lemons;
    public int plusFruit;
    [HideInInspector]
    public bool isThree = false;
    public void Awake()
    {
        apples = 0; oranges = 0; lemons = 0;
        if (isThree)
        {
            apples = Random.Range(1 + plusFruit, 6 + plusFruit);
            oranges = Random.Range(1 + plusFruit, 6 + plusFruit);
            lemons = Random.Range(1 + plusFruit, 6 + plusFruit);
        }
        else
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                apples = Random.Range(1 + plusFruit, 6 + plusFruit);
                oranges = Random.Range(1 + plusFruit, 6 + plusFruit);
            }
            else if (rand == 1)
            {
                oranges = Random.Range(1 + plusFruit, 6 + plusFruit);
                lemons = Random.Range(1 + plusFruit, 6 + plusFruit);
            }
            else
            {
                apples = Random.Range(1 + plusFruit, 6 + plusFruit);
                lemons = Random.Range(1 + plusFruit, 6 + plusFruit);
            }
        }
        string text = "Collect only ";
        if (apples != 0)
        {
            text += apples + " apples ";
        }
        if (oranges != 0)
        {
            text += oranges + " oranges ";
        }
        if (lemons != 0)
        {
            text += lemons + " lemons ";
        }
        questText.SetText(text);
    }
}
