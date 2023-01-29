using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultManager : MonoBehaviour
{
    public ConveyorSystem cs;
    public QuestSystem qs;
    public Spawner s;
    public GlobalVariables gv;
    [Header("Text")]
    public TextMeshProUGUI panelText;
    public TextMeshProUGUI buttonText;
    public void Next()
    {
        gv.difficult++;
        if (gv.difficult >= 3)
        {
            qs.isThree= true;
        }
        cs.plusSpeed += 0.2f;
        qs.plusFruit++;
        s.TimeBetweenSpawns -= .1f;
        if (s.TimeBetweenSpawns <= 0.4f)
        {
            s.TimeBetweenSpawns = 0.4f;
        }
        Restart();
    }
    public void Retry()
    {
        cs.plusSpeed = 0;
        qs.plusFruit = 0;
        s.TimeBetweenSpawns = 2;
        Restart();
    }
    public void Restart()
    {
        qs.Awake();
        cs.Start();
        s.Start();
        gv.Start();
    }
}
