using UnityEngine;

using System.Collections.Generic;

public class GameAssets: MonoBehaviour
{
    public static GameAssets current;
    private void Awake()
    {
        current = this;
        //spriteDict.Add(Box.Type.Bronze, boxSprites[0]);
        //spriteDict.Add(Box.Type.Silver, boxSprites[1]);
        //spriteDict.Add(Box.Type.Gold, boxSprites[2]);
    }


    //public Transform pfDamagePopup;
    //public Transform pfNumberPopup;
    //public Transform pfNumberPlusPopup;
    //public Transform pfTextPopup;
    public GameObject popUpPrefab;
    public Canvas popUpCanvas;
    public Sprite QuestionMark;
    public Sprite[] boxSprites;
    //public Dictionary<Box.Type, Sprite> spriteDict;

}
