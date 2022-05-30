using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
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

    string RUN = "Running 2";
    string IDLE = "Idle 2";
    const string DEATH = "Death 2";

    bool isAlive = true;

    const string enemyLayer = "Enemy";
    const string objectLayer = "Object";
    const string groundLayer = "Ground_Night";

    private void Start()
    {

    }

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
    }

    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) { return; }

        animator.Play(newState);
    }

    private void Update()
    {
        if (!isAlive) { return; }
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
            feetCollider.IsTouchingLayers(LayerMask.GetMask(objectLayer)) ||
            feetCollider.IsTouchingLayers(LayerMask.GetMask(enemyLayer)))
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

    void OnDie()
    {
        Die();
    }

    void Die()
    {
        ChangeAnimationState(DEATH);
        myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        gm.Invoke("ChangeState", deathDelay);
        isAlive = false;        
    }

    private void OnEnable()
    {
        isAlive = true;
        moveInput = Vector2.zero;
        transform.position = spawnPosition;
    }

}
