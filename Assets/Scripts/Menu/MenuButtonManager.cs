using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonManager : MonoBehaviour
{
    public List<GameObject> buttons;

    private void ShowButtons()
    {

    }

    IEnumerator ShowButtonsWithDelay()
    {
        foreach(var b in buttons)
        {
            b.SetActive(true);
            b.transform.DOScale(1);
        }
    }
}
