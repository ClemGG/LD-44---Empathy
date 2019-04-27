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
    public Text textCanvas;
    int currentDialogueID;


    [Space(10)]
    [Header("Fade effect : ")]
    [Space(10)]

    [Tooltip("Plus la valeur est haute, plus le fondu sera rapide, et inversement plus la valeur est basse.")]
    public float fadeSpeed = 1f;
    public float displayDelay = 5f;

    public AnimationCurve fadeCurve;


    private void Awake()
    {
        textCanvas.gameObject.SetActive(false);
    }




    public void ReadDialogue(int ID)
    {
        currentDialogueID = ID;
        textCanvas.text = dialogues[currentDialogueID].StartDialogue();

        StartCoroutine(ReadAllDialogue());
    }

    private IEnumerator ReadAllDialogue()
    {
        while(dialogues[currentDialogueID].index <= dialogues[currentDialogueID].répliques.Length)
        {
            float t = 0f;
            textCanvas.gameObject.SetActive(true);
            Color c = textCanvas.color;
            textCanvas.color = new Color(c.r, c.g, c.b, t);

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime * fadeSpeed;
                float a = fadeCurve.Evaluate(t);
                Color newCol = textCanvas.color;
                textCanvas.color = new Color(newCol.r, newCol.g, newCol.b, a);
                yield return 0;
            }

            yield return new WaitForSeconds(displayDelay);

            while (t > 0f)
            {
                t -= Time.unscaledDeltaTime * fadeSpeed;
                float a = fadeCurve.Evaluate(t);
                Color newCol = textCanvas.color;
                textCanvas.color = new Color(newCol.r, newCol.g, newCol.b, a);
                yield return 0;
            }

            yield return new WaitForSeconds(displayDelay);

            textCanvas.text = dialogues[currentDialogueID].NextReplique();


            yield return 0;
        }
    }
}
