using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public Animator animator;
    public Player1Controller player1Controller;
    public Player2Controller player2Controller;

    public string playerTag;

    private bool isColliding;
    
    public GameManager gameManager;
    public CutsceneManager cutsceneManager;

    private void Start()
    {

    }

    private void Update()
    {
        isColliding = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            if (isColliding) return;
            isColliding = true;
            gameManager.cutsceneNum++;
            cutsceneManager.cutscenePlaying += 0.5f;
            player1Controller.runSpeed = 0f;
            player2Controller.runSpeed = 0f;
            player1Controller.jumpAmount = 0f;
            player2Controller.jumpAmount = 0f;
            animator.SetBool(gameManager.cutsceneList[gameManager.cutsceneNum-1], true);
            Invoke("StopCutscene", 2f);
        }
    }

    void StopCutscene()
    {
        animator.SetBool(gameManager.cutsceneList[gameManager.cutsceneNum-1], false);
        Destroy(this);
    }
}
