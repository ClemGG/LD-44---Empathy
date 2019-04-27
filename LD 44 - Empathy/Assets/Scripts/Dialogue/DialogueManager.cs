using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Space(10)]
    [Header("Scripts & Components : ")]
    [Space(10)]

    public Dialogue[] dialogues;
    public Text currentTextCanvas;
    int currentDialogueID;


    [Space(10)]
    [Header("Fade effect : ")]
    [Space(10)]

    [Tooltip("Plus la valeur est haute, plus le fondu sera rapide, et inversement plus la valeur est basse.")]
    public float fadeSpeed = 1f;
    public float displayDelay = 5f;

    public AnimationCurve fadeCurve;


    Coroutine c;


    private void Awake()
    {
        if(currentTextCanvas)
            currentTextCanvas.gameObject.SetActive(false);
    }




    public void ReadDialogue(int ID)
    {
        currentDialogueID = ID;
        currentTextCanvas.text = dialogues[currentDialogueID].StartDialogue();

        c = StartCoroutine(ReadAllDialogue());
    }

    public void ReadDialogue(int ID, Text textCanvas)
    {
        currentDialogueID = ID;
        
        if (c != null)
        {
            StopCoroutine(c);
            c = null;
        }

        if (currentTextCanvas != null)
            currentTextCanvas.gameObject.SetActive(false);

        currentTextCanvas = textCanvas;
        currentTextCanvas.text = dialogues[currentDialogueID].StartDialogue();

        print(c == null);

        if(c == null)
            c = StartCoroutine(ReadAllDialogue());
    }

    private IEnumerator ReadAllDialogue()
    {
        while(dialogues[currentDialogueID].index <= dialogues[currentDialogueID].répliques.Length)
        {
            float t = 0f;
            currentTextCanvas.gameObject.SetActive(true);
            Color c = currentTextCanvas.color;
            currentTextCanvas.color = new Color(c.r, c.g, c.b, t);

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime * fadeSpeed;
                float a = fadeCurve.Evaluate(t);
                Color newCol = currentTextCanvas.color;
                currentTextCanvas.color = new Color(newCol.r, newCol.g, newCol.b, a);
                yield return 0;
            }

            yield return new WaitForSeconds(displayDelay);

            while (t > 0f)
            {
                t -= Time.unscaledDeltaTime * fadeSpeed;
                float a = fadeCurve.Evaluate(t);
                Color newCol = currentTextCanvas.color;
                currentTextCanvas.color = new Color(newCol.r, newCol.g, newCol.b, a);
                yield return 0;
            }

            yield return new WaitForSeconds(displayDelay);

            currentTextCanvas.text = dialogues[currentDialogueID].NextReplique();
            currentTextCanvas.gameObject.SetActive(false);


            yield return 0;
        }
    }
}
