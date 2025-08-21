using System.Collections;
using System.Collections.Generic;
using System.Security;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MenuCanvasGroup : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private List<CanvasGroup> mainMenuCanvasGroup = new List<CanvasGroup>();

    [Header("Camera Outfit")]
    [SerializeField] private List<CanvasGroup> outfitCanvasGroup = new List<CanvasGroup>();

    [Header("Camera Result")]
    [SerializeField] private List<CanvasGroup> resultCanvasGroup = new List<CanvasGroup>();

    [Header("QR Result")]
    [SerializeField] private List<CanvasGroup> qrCanvasGroup = new List<CanvasGroup>();
    
    public void ActivateMenuCanvas()
    {
        foreach (var item in mainMenuCanvasGroup)
        {
            item.gameObject.SetActive(true);
            item.interactable = true;
            item.blocksRaycasts = true;
            item.alpha = 0;
            DOTween.Init();
            item.DOFade(1f, 0.65f);
        }
    }

    public void DeactivateMenuCanvas()
    {
        foreach (var item in mainMenuCanvasGroup)
        {
            item.interactable = false;
            item.blocksRaycasts = false;
            DOTween.Init();
            item.alpha = 1;
            item.DOFade(0f, 0.65f).OnComplete( () => item.gameObject.SetActive(false));
        }
    }

    public void ActivateOutfitCanvas()
    {
        foreach (var item in outfitCanvasGroup)
        {
            item.gameObject.SetActive(true);
            item.interactable = true;
            item.blocksRaycasts = true;
            item.alpha = 0;
            DOTween.Init();
            item.DOFade(1f, 0.65f);
        }
    }

    public void DeactivateOutfitCanvas()
    {
        foreach (var item in outfitCanvasGroup)
        {
            item.interactable = false;
            item.blocksRaycasts = false;
            DOTween.Init();
            item.alpha = 1;
            item.DOFade(0f, 0.65f).OnComplete( () => item.gameObject.SetActive(false));
        }
    }

    public void ActivateResultCanvas()
    {
        foreach (var item in resultCanvasGroup)
        {
            item.gameObject.SetActive(true);
            item.interactable = true;
            item.blocksRaycasts = true;
            item.alpha = 0;
            DOTween.Init();
            item.DOFade(1f, 0.65f);
        }
    }

    public void DeactivateResultCanvas()
    {
        foreach (var item in resultCanvasGroup)
        {
            item.interactable = false;
            item.blocksRaycasts = false;
            DOTween.Init();
            item.alpha = 1;
            item.DOFade(0f, 0.65f).OnComplete( () => item.gameObject.SetActive(false));
        }
    }

    public void ActivateQRCanvas()
    {
        foreach (var item in qrCanvasGroup)
        {
            item.gameObject.SetActive(true);
            item.interactable = true;
            item.blocksRaycasts = true;
            item.alpha = 0;
            DOTween.Init();
            item.DOFade(1f, 0.65f);
        }
    }

    public void DeactivateQRCanvas()
    {
        foreach (var item in qrCanvasGroup)
        {
            item.interactable = false;
            item.blocksRaycasts = false;
            DOTween.Init();
            item.alpha = 1;
            item.DOFade(0f, 0.65f).OnComplete( () => item.gameObject.SetActive(false));
        }
    }

}
