using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIElementsUtils : MonoBehaviour
{
    public void ActivateCanvasGroup(CanvasGroup canvas)
    {
        if (canvas.interactable) return;
        DOTween.Init();
        canvas.gameObject.SetActive(true);
        canvas.alpha = 0;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
        canvas.DOFade(1f, 0.65f);
    }

    public void DeactivateCanvasGroup(CanvasGroup canvas)
    {
        if (!canvas.interactable) return;
        DOTween.Init();
        canvas.alpha = 1;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
        canvas.DOFade(0f, 0.65f);
    }

}
