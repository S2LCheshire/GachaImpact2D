using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(CanvasGroup))]
public class SceneTransition : MonoBehaviour
{
    [SerializeField] float delayInSeconds=0.5f;
    CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    private void OnEnable()
    {
        canvasGroup.DOFade(1, delayInSeconds);
    }
    private void OnDisable()
    {
        //canvasGroup.DOFade(0, delayInSeconds);
    }
}
