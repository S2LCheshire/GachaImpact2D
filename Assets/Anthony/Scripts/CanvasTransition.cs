using DG.Tweening;
using UnityEngine;

public class CanvasTransition : MonoBehaviour
{

    private void OnEnable()
    {
        transform.DOMoveX(0, 0.5f);
        //
        //g("called on enable");
    }
    private void OnDisable()
    {
        transform.DOMoveX(-30, 0.5f);
        ////Debug.Log("called on enable");
    }
}
