using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("bools")]
    bool isGrounded; // Whether or not the player is currently grounded
    public bool isLeft; // Whether or not the player is facing left
    [SerializeField] bool isRolling; // Whether or not the player is currently rolling

    [Header("Timers")]
    float rollCooldownTimer; // Timer for the roll cooldown
    float rollDurationTimer; // Timer for the roll duration

    [Header("Misc")]
    CharacterStatistics stats;
    Rigidbody2D rb; // The player's Rigidbody2D component
    GameManager gameManager; // The GameManager instance
    [SerializeField] GameObject walkTowards; // A game object used to determine the player's movement direction
    [SerializeField] float rollDuration = 0.5f; // The duration a roll is active
    [SerializeField] float rollCooldown = 2; // The cooldown needed before another roll

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterStatistics>();
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        Inputs(); // Check for input
        Timers(); // Update timers

        #region isLeft
        // Flip the sprite horizontally if the player is facing left
        if (isLeft)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
        #endregion
    }

    private void FixedUpdate()
    {
        Movement(); // Move the player based on input
    }

    void Inputs()
    {
        #region Left, Right, Up, Down
        // Check for input to move left
        if (Input.GetKey(KeyCode.A))
        {
<<<<<<< Updated upstream
            isLeft = true;
            walkTowards.transform.localPosition = new Vector2(-1, walkTowards.transform.localPosition.y);
=======
            #region Left, Right, Up, Down
            if (!stats.isAttacking)
            {
                // Check for input to move left
                if (Input.GetKey(KeyCode.A))
                {
                    isLeft = true;
                    walkTowards.transform.localPosition = new Vector2(-1, walkTowards.transform.localPosition.y);
                }

                // Check for input to move right
                else if (Input.GetKey(KeyCode.D))
                {
                    isLeft = false;
                    walkTowards.transform.localPosition = new Vector2(1, walkTowards.transform.localPosition.y);
                }

                // Reset walkToward localPosition x to 0
                else
                    walkTowards.transform.localPosition = new Vector2(0, walkTowards.transform.localPosition.y);

                // Check for input to move up
                if (Input.GetKey(KeyCode.W))
                {
                    walkTowards.transform.localPosition = new Vector2(walkTowards.transform.localPosition.x, 1);
                }

                // Check for input to move down
                else if (Input.GetKey(KeyCode.S))
                {
                    walkTowards.transform.localPosition = new Vector2(walkTowards.transform.localPosition.x, -1);
                }

                // Reset walkToward localPosition y to 0
                else
                    walkTowards.transform.localPosition = new Vector2(walkTowards.transform.localPosition.x, 0);
            }
            #endregion

            #region Rolling
            // Check for input to initiate a roll and rollCooldownTimer is equal to or lower than 0
            if (Input.GetKeyDown(KeyCode.LeftShift) && rollCooldownTimer <= 0)
            {
                isRolling = true; // Set isRolling to true
                rollDurationTimer = rollDuration; // Initiate rollDurationTimer countdown
                rollCooldownTimer = rollCooldown; // Initiate rollCooldownTimer countdown
                stats.attackTimer = 0;
            }
            //Allow user to pause game
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.state = GameManager.GameState.Paused;
                GameManager.ChangeState();
            }
            #endregion

            #region Attack
            // Check if isPlayer is true
            if (stats.isPlayer)
            {
                // If input is pressed and player is not already attacking
                if (Input.GetKeyDown(KeyCode.Space) && !stats.isAttacking)
                {
                    stats.attackTimer = stats.attackDuration; // Set attack timer to indicate the player is attacking
                    //animator.Play(PLAYER_ATTACKSIDE); // Play attacking animation
                    StartCoroutine(AttackAnimation());
                }
            }
            #endregion
>>>>>>> Stashed changes
        }

        // Check for input to move right
        else if (Input.GetKey(KeyCode.D))
        {
            isLeft = false;
            walkTowards.transform.localPosition = new Vector2(1, walkTowards.transform.localPosition.y);
        }

        // Reset walkToward localPosition x to 0
        else
            walkTowards.transform.localPosition = new Vector2(0, walkTowards.transform.localPosition.y);

        // Check for input to move up
        if (Input.GetKey(KeyCode.W))
        {
            walkTowards.transform.localPosition = new Vector2(walkTowards.transform.localPosition.x, 1);
        }

        // Check for input to move down
        else if (Input.GetKey(KeyCode.S))
        {
            walkTowards.transform.localPosition = new Vector2(walkTowards.transform.localPosition.x, -1);
        }

        // Reset walkToward localPosition y to 0
        else
            walkTowards.transform.localPosition = new Vector2(walkTowards.transform.localPosition.x, 0);
        #endregion

        #region Rolling
        // Check for input to initiate a roll and rollCooldownTimer is equal to or lower than 0
        if (Input.GetKeyDown(KeyCode.LeftShift) && rollCooldownTimer <= 0)
        {
            isRolling = true; // Set isRolling to true
            rollDurationTimer = rollDuration; // Initiate rollDurationTimer countdown
            rollCooldownTimer = rollCooldown; // Initiate rollCooldownTimer countdown
        }
        //Allow user to pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.state = GameManager.GameState.Paused;
            GameManager.ChangeState();
        }
        #endregion
    }
    IEnumerator AttackAnimation()
    {
        animator.Play(PLAYER_ATTACKSIDE);
        yield return new WaitForSecondsRealtime(0.5f);
        animator.Play(currentState);
    }
    void Movement()
    {
        #region Left, Right, Up, Down
        transform.position = Vector3.MoveTowards(transform.position, walkTowards.transform.position, stats.moveSpeed * Time.deltaTime); // Move the player towards walkTowards using moveSpeed
        #endregion

        #region Rolling
        // Check if the player is rolling and rollDurationTimer is less than 0
        if (isRolling && rollDurationTimer > 0)
            transform.position = Vector3.MoveTowards(transform.position, walkTowards.transform.position, stats.rollSpeed * Time.deltaTime); // Move the player towards walkTowards using rollSpeed
        #endregion
    }

    void Timers()
    {
        // Check if rollCooldownTimer is greater than 0
        if (rollCooldownTimer > 0)
            rollCooldownTimer -= Time.deltaTime; // Reduce rollCooldownTimer by delta time

        // Check if rollDurationTimer is greater than 0
        if (rollDurationTimer > 0)
            rollDurationTimer -= Time.deltaTime; // Reduce rollDurationTimer by delta time

        // Check if rollDurationTimer has expired
        if (rollDurationTimer <= 0)
            isRolling = false; // Set isRolling to false
    }
}
