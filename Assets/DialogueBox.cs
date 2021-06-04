using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueBox : MonoBehaviour
{
    TextMeshProUGUI tmpro;
    public static DialogueBox current;
    [SerializeField] float dialogueSpeed = 0.01f;
    IEnumerator currentCoroutine;

    private void Awake()
    {
        current = this;
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void OnEnable()
    {
        TypeSentence("Welcome to the Home Screen!");
    }

    public void TypeSentence (string sentence)
    {
        if (sentence == "") return;
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = AnimateSentence(sentence);
        StartCoroutine(currentCoroutine);
    }

    IEnumerator AnimateSentence (string sentence)
    {
        tmpro.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tmpro.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
}
