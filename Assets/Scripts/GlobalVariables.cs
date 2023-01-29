using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Fruit;

public class GlobalVariables : MonoBehaviour
{
    [HideInInspector]
    public int apples, oranges, lemons;
    public TextMeshProUGUI applesText, orangesText, lemonsText;
    [SerializeField]
    private QuestSystem qs;
    [SerializeField]
    private WinLosePanel wlp;
    [HideInInspector]
    public int difficult;
    [SerializeField]
    private Transform cameraPoint;
    [HideInInspector]
    public bool isWin;

    public Spawner s;
    public void Start()
    {
        isWin = false;
        apples = 0; oranges = 0; lemons = 0;
        applesText.SetText("Apples: " + apples + "/" + qs.apples);
        applesText.color = new Color(0.6792453f, 0.1569954f, 0.1569954f, 1f);
        orangesText.SetText("Oranges: " + oranges + "/" + qs.oranges);
        orangesText.color = new Color(0.9150943f, 0.3679602f, 0.09064611f, 1f);
        lemonsText.SetText("Lemons: " + lemons + "/" + qs.lemons);
        lemonsText.color = new Color(1f, 0.9770688f, 0.09019607f, 1f);
        Camera.main.transform.position = new Vector3(-6.058f, 3.307f, -0.5f);
    }

    public void AddFruit(Fruit.FruitType fruitType)
    {
        if (fruitType == FruitType.apple)
        {
            apples++;
            applesText.SetText("Apples: " + apples + "/" + qs.apples);
        }
        else if (fruitType == FruitType.orange)
        {
            oranges++;
            orangesText.SetText("Oranges: " + oranges + "/" + qs.oranges);
        }
        else if (fruitType == FruitType.lemon)
        {
            lemons++;
            lemonsText.SetText("Lemons: " + lemons + "/" + qs.lemons);
        }
    }

    private void Update()
    {
        if (apples == qs.apples)
        {
            applesText.color = Color.green;
        }
        else if (apples > qs.apples)
        {
            applesText.color = Color.red;
            Lose();
        }
        if (oranges == qs.oranges)
        {
            orangesText.color = Color.green;
        }
        else if (oranges > qs.oranges)
        {
            orangesText.color = Color.red;
            Lose();
        }
        if (lemons == qs.lemons)
        {
            lemonsText.color = Color.green;
        }
        else if (lemons > qs.lemons)
        {
            lemonsText.color = Color.red;
            Lose();
        }
        if (apples == qs.apples && oranges == qs.oranges && lemons == qs.lemons)
        {
            Win();
        }
        if (s.spawnedFruitsFolder.childCount <= 0 && s.fruitsLeft <= 0 && !isWin)
        {
            Lose();
        }
    }

    public void Win()
    {
        isWin = true;
        wlp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Level Passed! Current difficult = " + (difficult + 1));
        wlp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Next Level");
        wlp.SetVisability(true);
        wlp.transform.GetChild(0).GetComponent<Buttons>().buttonType = Buttons.ButtonType.Next;
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraPoint.position, Time.deltaTime);
    }

    public void Lose()
    {
        wlp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("You lose");
        wlp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Retry");
        wlp.SetVisability(true);
        wlp.transform.GetChild(0).GetComponent<Buttons>().buttonType = Buttons.ButtonType.Retry;
        Time.timeScale = 0;
    }
}
