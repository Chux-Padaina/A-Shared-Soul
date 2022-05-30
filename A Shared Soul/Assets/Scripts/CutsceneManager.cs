using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public float cutscenePlaying = -0.5f;

    public float textShowTime;

    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject display;
    TMP_Text godText;

    public Player1Controller p1;
    public Player2Controller p2;

    public float cameraDelay = 1f;

    public string[] methodNames = new string[3];
    

    private void Start()
    {
        godText = display.GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        for(int i=0; i < methodNames.Length; i++)
        {
            if(cutscenePlaying == i)
            {
                cutscenePlaying += 0.5f;
                Invoke(methodNames[i], cameraDelay);
            }
        }

    }

    IEnumerator ChangeText(string newText, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        godText.text = newText;
    }

    IEnumerator DisableDisplay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        godText.text = "";
        display.SetActive(false);
    }

    IEnumerator RestoreMovement(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        p1.jumpAmount = gameManager.cacheJump;
        p2.jumpAmount = gameManager.cacheJump;
        p1.runSpeed = gameManager.cacheSpeed;
        p2.runSpeed = gameManager.cacheSpeed;
    }

    void Cutscene_0()
    {
        display.SetActive(true);
        StartCoroutine(ChangeText("Hi there! I'm Jeff.", 0f));
        StartCoroutine(ChangeText("I'm the guide around here.", textShowTime));
        StartCoroutine(ChangeText("I'll show you around.", textShowTime * 2));
        StartCoroutine(DisableDisplay(textShowTime * 3));
        StartCoroutine(RestoreMovement((textShowTime * 3) + 1));
    }

    void Cutscene_1()
    {
        display.SetActive(true);
        StartCoroutine(ChangeText("Don't mind him.", 0f));
        StartCoroutine(ChangeText("He was the last person to come here.", textShowTime));
        StartCoroutine(ChangeText("Didn't follow my advice...", textShowTime * 2));
        StartCoroutine(DisableDisplay(textShowTime * 3));
        StartCoroutine(RestoreMovement((textShowTime * 3) + 1));
    }
    void Cutscene_2()
    {
        display.SetActive(true);
        StartCoroutine(ChangeText("That is a block of death.", 0f));
        StartCoroutine(ChangeText("You shouldn't touch that.", textShowTime));
        StartCoroutine(DisableDisplay(textShowTime * 2));
        StartCoroutine(RestoreMovement((textShowTime * 2) + 1));
    }

    void Cutscene_3()
    {
        display.SetActive(true);
        StartCoroutine(ChangeText("Well...", 0f));
        StartCoroutine(ChangeText("I guess you had no choice.", textShowTime));
        StartCoroutine(ChangeText("Use this body then.", textShowTime * 2));
        StartCoroutine(ChangeText("It'll help sort things out.", textShowTime * 3));
        StartCoroutine(DisableDisplay(textShowTime * 4));
        StartCoroutine(RestoreMovement((textShowTime * 4) + 1));
    }

    void Cutscene_4()
    {
        display.SetActive(true);
        StartCoroutine(ChangeText("This body is unaffected by the block of death.", 0f));
        StartCoroutine(ChangeText("You can interact with blue blocks in this body,", textShowTime));
        StartCoroutine(ChangeText("and red blocks in your real body", textShowTime * 2));
        StartCoroutine(ChangeText("Try pushing it down to help your real body.", textShowTime * 3));
        StartCoroutine(DisableDisplay(textShowTime * 4));
        StartCoroutine(RestoreMovement((textShowTime * 4) + 1));
    }

    void Cutscene_5()
    {
        display.SetActive(true);
        StartCoroutine(ChangeText("Nice, you can now switch back.", 0f));
        StartCoroutine(ChangeText("Press 'K' to return to your body.", textShowTime));
        StartCoroutine(DisableDisplay(textShowTime * 2));
        StartCoroutine(RestoreMovement((textShowTime * 2) + 1));
    }

    void Cutscene_6()
    {
        display.SetActive(true);
        StartCoroutine(ChangeText("Now off you go.", 0f));
        StartCoroutine(ChangeText("There aren't many levels in here,", textShowTime));
        StartCoroutine(ChangeText("you know, because I had no time...", textShowTime * 2));
        StartCoroutine(ChangeText("But have fun anyway!", textShowTime * 3));
        StartCoroutine(DisableDisplay(textShowTime * 4));
        StartCoroutine(RestoreMovement((textShowTime * 4) + 1));
    }

}
