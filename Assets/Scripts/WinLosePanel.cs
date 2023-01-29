using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLosePanel : MonoBehaviour
{
    private Vector3 defaultPos;
    private void Start()
    {
        defaultPos = transform.position;
        SetVisability(false);
    }
    public void SetVisability(bool isVisible)
    {
        if (isVisible)
        {
            transform.position = defaultPos;
        }
        else
        {
            transform.position = Vector3.one * -4000;
        }
    }
}
