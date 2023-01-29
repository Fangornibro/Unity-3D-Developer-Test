using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandMoving : MonoBehaviour
{
    public Transform basket;
    public Transform idolAnimationDot1, idolAnimationDot2, victoryAnimationDot1, victoryAnimationDot2;
    private bool idolAnimationBool = true, victoryAnimationBool = true;
    private GlobalVariables gv;
    private void Start()
    {
        gv = GameObject.Find("GlobalVariables").GetComponent<GlobalVariables>();
    }
    void Update()
    {
        basket.position = transform.position + Vector3.up * 0.2f;
        if (gv.isWin)
        {
            if (victoryAnimationBool)
            {
                float step = 3f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, victoryAnimationDot1.position, step);
                if (Vector3.Distance(transform.position, victoryAnimationDot1.position) < 0.001f)
                {
                    victoryAnimationBool = false;
                }
            }
            else
            {
                float step = 3f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, victoryAnimationDot2.position, step);
                if (Vector3.Distance(transform.position, victoryAnimationDot2.position) < 0.001f)
                {
                    victoryAnimationBool = true;
                }
            }
        }
        else if (idolAnimationBool)
        {
            float step = 0.5f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, idolAnimationDot1.position, step);
            if (Vector3.Distance(transform.position, idolAnimationDot1.position) < 0.001f)
            {
                idolAnimationBool = false;
            }
        }
        else
        {
            float step = 0.5f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, idolAnimationDot2.position, step);
            if (Vector3.Distance(transform.position, idolAnimationDot2.position) < 0.001f)
            {
                idolAnimationBool = true;
            }
        }
    }

}
