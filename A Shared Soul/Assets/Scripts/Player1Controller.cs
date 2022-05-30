using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controller : MonoBehaviour
{
    public float runSpeed;
    public float jumpAmount;
    [SerializeField] float deathDelay;
    [SerializeField] GameManager gm;
    public Vector2 spawnPosition;
    public AudioSource audioSource;
    public Collider2D movingCollider;

    public bool isOnMoving;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator animator;
    BoxCollider2D myCollider;
    CapsuleCollider2D feetCollider;
    
    string currentState;

    //animation states

    const string RUN = "Running 1";
    const string IDLE = "Idle 1";
    const string DEATH = "Death 1";

    bool isAlive = true;

    const string enemyLayer = "Enemy";
    const string objectLayer = "Object";
    const string groundLayer = "Ground_Day";

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
    }

    void ChangeAnimationState(string newState)
    {
        if(newState == currentState) { return; }

        animator.Play(newState);
    }

    private void Update()
    {
        if (!isAlive) {  return; }
        Die();
    }

    private void FixedUpdate()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            ChangeAnimationState(RUN);
        }
        else
        {
            ChangeAnimationState(IDLE);
        }

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void OnJump()
    {
        if (!isAlive) { return; }
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask(groundLayer)) ||
            feetCollider.IsTouchingLayers(LayerMask.GetMask(objectLayer)))
        {
            if (feetCollider.IsTouching(movingCollider))
            {
                isOnMoving = true;
            }
            else
            {
                isOnMoving = false;
            }

            if (jumpAmount > 0f)
            {
                audioSource.Play();
            }
            myRigidbody.velocity += new Vector2(0f, jumpAmount);
        }
    }

    void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask(enemyLayer)))
        {
            ChangeAnimationState(DEATH);
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            gm.Invoke("ChangeState", deathDelay);
            isAlive = false;
        }
    }

    private void OnEnable()
    {
        isAlive = true;
        moveInput = Vector2.zero;
        transform.position = spawnPosition;
    }
}
