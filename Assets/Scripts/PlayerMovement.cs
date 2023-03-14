using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("bools")]
    bool isMoving; // Whether or not the player is currently moving
    bool isGrounded; // Whether or not the player is currently grounded
    public bool isLeft; // Whether or not the player is facing left
    public bool isRolling; // Whether or not the player is currently rolling

    [Header("Timers")]
    float rollCooldownTimer; // Timer for the roll cooldown
    float rollDurationTimer; // Timer for the roll duration

    [Header("Animations")]
    const string PLAYER_ATTACKDOWN = "Player_AttackDown"; // Stores the "Player_AttackDown" animation
    const string PLAYER_ATTACKSIDE = "Player_AttackSide"; // Stores the "Player_AttackSide" animation
    const string PLAYER_ATTACKUP = "Player_AttackUp"; // Stores the "Player_AttackUp" animation
    const string PLAYER_FAINT = "Player_Faint"; // Stores the "Player_Faint" animation
    const string PLAYER_FAINTED = "Player_Fainted"; // Stores the "Player_Fainted" animation
    const string PLAYER_IDLEDOWN = "Player_IdleDown"; // Stores the "Player_IdleDown" animation
    const string PLAYER_IDLESIDE = "Player_IdleSide"; // Stores the "Player_IdleSide" animation
    const string PLAYER_IDLEUP = "Player_IdleUp"; // Stores the "Player_IdleUp" animation
    const string PLAYER_WALKDOWN = "Player_WalkDown"; // Stores the "Player_WalkDown" animation
    const string PLAYER_WALKSIDE = "Player_WalkSide"; // Stores the "Player_WalkSide" animation
    const string PLAYER_WALKUP = "Player_WalkUp"; // Stores the "Player_WalkUp" animation
    string currentState; // A string containing the current animation state
    string lastDirection; // A string containing the last direction the player was facing
    Animator animator; // The player's Animator component

    [Header("Misc")]
    //Amount of rooms we have in project.
    //List of rooms so we can check if one exists at that location
    public List<GameObject> roomList = new List<GameObject>();
    CharacterStatistics stats; // The player's CharacterStatistics component
    Rigidbody2D rb; // The player's Rigidbody2D component
    GameManager gameManager; // The GameManager instance
    [SerializeField] GameObject walkTowards; // A game object used to determine the player's movement direction
    [SerializeField] float rollDuration = 0.5f; // The duration a roll is active
    [SerializeField] float rollCooldown = 2; // The cooldown needed before another roll

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterStatistics>(); // Get the CharacterStatistics component
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        animator = GetComponent<Animator>(); // Get the Animator component
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1) roomList.Add(Instantiate(Resources.Load<GameObject>("Rooms/" + Random.Range(0, 6)), new Vector3(0f, 0f, 0f), Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        // If player is not fainted
        if (!stats.isFainted)
        {
            Inputs(); // Check for input
            Timers(); // Update timers
            Animations(); // Update animations
        }

        if(walkTowards.transform.localPosition != Vector3.zero)
            stats.hitBox.transform.localPosition = walkTowards.transform.localPosition; // Always align player attack box infront of them

        if(stats.isFainted)
            ChangeAnimationState("Player_Faint");

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
        // If player is not fainted
        if (!stats.isFainted)
            Movement(); // Move the player based on input
    }

    void Inputs()
    {
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
                StartCoroutine(AttackAnimation()); // Play attacking animation
            }
        }
        #endregion
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

    void Animations()
    {
        // If player is not fainted
        if (!stats.isFainted)
        {
            #region Left, Right, Up, Down, Idle
            // Check if walkTowards x position does not equal 0
            if (walkTowards.transform.localPosition.x != 0)
            {
                ChangeAnimationState(PLAYER_WALKSIDE); // Play walking animation
                lastDirection = "Side";
            }

            // Check if walkTowards y position is bellow 0
            else if (walkTowards.transform.localPosition.y < 0)
            {
                ChangeAnimationState(PLAYER_WALKDOWN); // Play walking animation
                lastDirection = "Down";
            }

            // Check if walkTowards y position is above 0
            else if (walkTowards.transform.localPosition.y > 0)
            {
                ChangeAnimationState(PLAYER_WALKUP); // Play walking animation
                lastDirection = "Up";
            }

            // Check if player is idle in the down direction
            else
                switch (lastDirection)
                {
                    case "Side":
                        ChangeAnimationState(PLAYER_IDLESIDE);
                        break;
                    case "Down":
                        ChangeAnimationState(PLAYER_IDLEDOWN);
                        break;
                    case "Up":
                        ChangeAnimationState(PLAYER_IDLEUP);
                        break;
                    default:
                        ChangeAnimationState(PLAYER_IDLESIDE);
                        break;
                }

            #endregion
        }
    }

    IEnumerator AttackAnimation()
    {
        // Check if walkTowards x position does not equal 0
        if (stats.hitBox.transform.localPosition.x != 0)
            ChangeAnimationState(PLAYER_ATTACKSIDE); // Play attack animation

        // Check if walkTowards y position is bellow 0
        else if (stats.hitBox.transform.localPosition.y < 0)
            ChangeAnimationState(PLAYER_ATTACKDOWN); // Play attack animation

        // Check if walkTowards y position is above 0
        else if (stats.hitBox.transform.localPosition.y > 0)
            ChangeAnimationState(PLAYER_ATTACKUP); // Play attack animation
        yield return new WaitForSecondsRealtime(stats.attackDuration);
        animator.Play(currentState);
    }

    public void ChangeAnimationState(string newState)
    {
        // If the new state is the same as the current state, do nothing
        if (currentState == newState)
            return;

        // Otherwise, play the new animation state and update the current state
        animator.Play(newState);
        currentState = newState;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we enter the camera triggers move the camera the desired amount based of the amount given by the name of the object we triggered, making sure to use the right axis for the right part of room
        if (collision.transform.CompareTag("CamTrigger"))
        {
            //X axis triggers have 39 or -39 for a name
            if (collision.transform.name == "-39" || collision.transform.name == "39") Camera.main.transform.position += new Vector3(float.Parse(collision.transform.name), 0f, 0f);  
            //y axis
            else Camera.main.transform.position += new Vector3(0f, float.Parse(collision.transform.name), 0f);
        }
        //else if we enter a room trigger
        else if (collision.transform.CompareTag("RoomTrigger"))
        {
            //Set a bool to determine if we have a room already to false
            bool matchFound = false;
            //If collided trigger has 39 or -39 for a name we are using x axis
            if (collision.transform.name == "-39" || collision.transform.name == "39")
            {
                //move the camera to the location of the next room
                Camera.main.transform.position += new Vector3(float.Parse(collision.transform.name), 0f, 0f);
                //Check the roomList for any current rooms at that location and set bool to true if one exists
                foreach (GameObject room in roomList)
                {
                    if (room.transform.position == Camera.main.transform.position + new Vector3(float.Parse(collision.transform.name), 0f, 10f)) matchFound = true;
                }
                string roomNumber = "Rooms/" + Random.Range(0, 6).ToString();
                //If no match found instantiate a new room using name of triggered object as amount to add to cameras X position to get location of new room
                if (!matchFound) roomList.Add(Instantiate(Resources.Load<GameObject>(roomNumber), Camera.main.transform.position + new Vector3(float.Parse(collision.transform.name), 0f, 10f), Quaternion.identity));
            }
            //Else we are using y axis
            else 
            {
                //Move the camera to the location of new room
                Camera.main.transform.position += new Vector3(0f, float.Parse(collision.transform.name), 0f);
                //Check the roomlist for any existing rooms at that location and set bool to true if one exists
                foreach (GameObject room in roomList)
                {
                    if (room.transform.position == Camera.main.transform.position + new Vector3(0f, float.Parse(collision.transform.name), 10f)) matchFound = true;
                }
                string roomNumber = "Rooms/" + Random.Range(0, 6).ToString();
                Debug.Log(roomNumber);
                //If no match found instantiate a new room using name of triggered object as amount to add to cameras y position to get location of new room
                if (!matchFound) roomList.Add(Instantiate(Resources.Load<GameObject>(roomNumber), Camera.main.transform.position + new Vector3(0f, float.Parse(collision.transform.name), 10f), Quaternion.identity)); 
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if we exit camera trigger disable the camera triggers for that room and enable the room triggers. Set dash to 1 as we are in a room
        if (collision.transform.CompareTag("CamTrigger"))
        {
            collision.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
            collision.transform.parent.gameObject.SetActive(false);
        }
        //Else if we exit a room trigger disable the room triggers and enable the camera triggers. Set dash to 3 so we zoom through path between rooms.
        else if (collision.transform.CompareTag("RoomTrigger"))
        {
            collision.transform.parent.parent.GetChild(0).gameObject.SetActive(true);
            collision.transform.parent.gameObject.SetActive(false);
        }
    }
}