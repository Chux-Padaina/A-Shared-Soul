using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject dayPlatforms;
    [SerializeField] GameObject nightPlatforms;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] CutsceneManager cutsceneManager;
    [SerializeField] AudioSource audioSource;

    public float cacheSpeed;
    public float cacheJump;

    public string[] cutsceneList = new string[2];

    public int cutsceneNum = 0;

    public bool isDaytime = true;

    private bool isChanging = false;

    private void Start()
    {
        cameraAnimator.SetBool("isDay", true);
    }

    void OnSwitchState()
    {
        ChangeState();
    }

    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeState()
    {
        if(player1.GetComponent<Player1Controller>().runSpeed == 0f) { return; }
        if (!isChanging)
        {
            isChanging = true;
            audioSource.Play();

            player1.SetActive(!isDaytime);
            dayPlatforms.SetActive(!isDaytime);
            player2.SetActive(isDaytime);
            nightPlatforms.SetActive(isDaytime);


            cameraAnimator.SetBool("isDay", !isDaytime);
            isDaytime = !isDaytime;

            Invoke("ResetChanging", 1f);
        }
    }

    private void ResetChanging()
    {
        isChanging = false;
    }

    public void LoadLv1()
    {
        SceneManager.LoadScene(1);
    }
}
