using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundGlide : MonoBehaviour
{
    [SerializeField] float verticalSpeed = 1f;
    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] Sprite backgroundSprite;
    [SerializeField] float canvasMaxY = 720;
    [SerializeField] float canvasMaxX = 1280;
    // Update is called once per frame

    void ChangeAllChildBackground()
    {
        foreach (Image image in GetComponentsInChildren<Image>())
        {
            image.sprite = backgroundSprite;
        }
    }

    private void Start()
    {
        ChangeAllChildBackground();
    }

    void Update()
    {
        if (Mathf.Abs(transform.localPosition.y) >= canvasMaxY || Mathf.Abs(transform.localPosition.x) >= canvasMaxX) transform.position=new Vector3(0,0,0);
        transform.position += new Vector3(horizontalSpeed*Time.deltaTime, verticalSpeed*Time.deltaTime, 0);
    }
}
