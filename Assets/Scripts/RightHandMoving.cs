using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandMoving : MonoBehaviour
{
    public float targetSpeed;
    [HideInInspector]
    public Fruit curFruit;
    [HideInInspector]
    public bool goingBack = false;
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
        else if (curFruit != null && !goingBack)
        {
            float step = targetSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, curFruit.transform.position, step);

            if (Vector3.Distance(transform.position, curFruit.transform.position) < 0.001f)
            {
                curFruit.KinematicActivation();
                goingBack = true;
            }
        }
        else if (goingBack)
        {
            curFruit.transform.position = transform.position;
            float step = targetSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, basket.position, step);
            if (Vector3.Distance(transform.position, basket.position) < 0.001f)
            {
                curFruit.Collect();
                curFruit = null;
                goingBack = false;
            }
        }
        else
        {
            if (idolAnimationBool)
            {
                float step = 1f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, idolAnimationDot1.position, step);
                if (Vector3.Distance(transform.position, idolAnimationDot1.position) < 0.001f)
                {
                    idolAnimationBool = false;
                }
            }
            else
            {
                float step = 1f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, idolAnimationDot2.position, step);
                if (Vector3.Distance(transform.position, idolAnimationDot2.position) < 0.001f)
                {
                    idolAnimationBool = true;
                }
            }
            
        }
    }

}
