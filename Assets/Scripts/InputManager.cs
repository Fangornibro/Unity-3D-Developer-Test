using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public RightHandMoving hm;
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    try
                    {
                        Fruit curFruit = hit.collider.GetComponent<Fruit>();
                        if (curFruit.isSelected && !hm.goingBack)
                        {
                            hm.curFruit = curFruit;
                        }
                    }
                    catch { }
                }
            }
        }

    }
}
