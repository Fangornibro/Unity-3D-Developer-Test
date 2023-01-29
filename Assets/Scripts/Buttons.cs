using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour, IPointerClickHandler
{
    public enum ButtonType { Play, Exit, Next, Retry }
    public ButtonType buttonType;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Play)
        {
            SceneManager.LoadScene(1);
        }
        else if (buttonType == ButtonType.Exit)
        {
            Application.Quit();
        }
        else if (buttonType == ButtonType.Next)
        {
            DifficultManager dif = GameObject.Find("DifficultManager").GetComponent<DifficultManager>();
            transform.parent.GetComponent<WinLosePanel>().SetVisability(false);
            dif.Next();
        }
        else if (buttonType == ButtonType.Retry)
        {
            DifficultManager dif = GameObject.Find("DifficultManager").GetComponent<DifficultManager>();
            transform.parent.GetComponent<WinLosePanel>().SetVisability(false);
            dif.Retry();
            Time.timeScale = 1;
        }
    }
}
