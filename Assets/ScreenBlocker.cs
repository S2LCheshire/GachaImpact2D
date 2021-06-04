using UnityEngine;
using UnityEngine.UI;

public class ScreenBlocker : MonoBehaviour
{
    public static ScreenBlocker current;
    public Transform screenBlockImage;
    private void Awake()
    {
        current = this;
        TurnOff();
    }

    public void TurnOn()
    {
        ////Debug.Log("turning on");
        screenBlockImage.gameObject.SetActive(true);
    }
    public void TurnOff()
    {
        ////Debug.Log("turning off");
        screenBlockImage.gameObject.SetActive(false);
    }
}
