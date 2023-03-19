using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] GameObject target; // The enemy's current target
    [SerializeField] float attackRange = 1;

    [Header("Spawning")]
    [SerializeField] float xMin = -1; // The minimum x value the enemy can spawn within
    [SerializeField] float xMax = 1; // The maximum x value the enemy can spawn within
    [SerializeField] float yMin = -1; // The minimum y value the enemy can spawn within
    [SerializeField] float yMax = 1; // The maximum y value the enemy can spawn within

    [Header("Animations")]
    [SerializeField] string enemyType;
    [SerializeField] string ENEMY_WALK;
    [SerializeField] string ENEMY_ATTACK;
    [SerializeField] string ENEMY_FAINT;
    [SerializeField] string ENEMY_IDLE;
    string currentState;

    [Header("Misc")]
    bool isLeft;
    CharacterStatistics stats;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        #region Spawning
        int canSpawn = Mathf.RoundToInt(Random.Range(0, 2)); // Determine whether the enemy will spawn
        Debug.Log(canSpawn);

        // If the enemy cannot spawn
        if (canSpawn == 0)
            Destroy(gameObject); // Destroy enemy GameObject
        #endregion

        #region Initiate
        stats = GetComponent<CharacterStatistics>();
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player"); // Find and assign the player to target
        transform.localPosition = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax)); // Spawn enemy in a random location within the room
        #endregion

        #region AnimationReferences
        ENEMY_WALK = enemyType + "_Walk";
        ENEMY_ATTACK = enemyType + "_Attack";
        ENEMY_FAINT = enemyType + "_Faint";
        ENEMY_IDLE = enemyType + "_Idle";
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region isLeft
        if (isLeft)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            stats.hitBox.transform.localPosition = new Vector2(-0.8f, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            stats.hitBox.transform.localPosition = new Vector2(0.8f, 0);
        }
        #endregion

        #region hasFainted
        if (stats.isFainted)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            ChangeAnimationState(ENEMY_FAINT);
        }
        #endregion

        #region playerFaints
        if (target != null && target.GetComponent<CharacterStatistics>().isFainted)
        {
            target = null;
            ChangeAnimationState(ENEMY_IDLE);
            GameManager.state = GameManager.GameState.PostGame;
            GameManager.ChangeState();
        }
        #endregion

        if (GameManager.state == GameManager.GameState.InGame) Attack();
    }

    private void FixedUpdate()
    {
        if(!stats.isFainted && GameManager.state == GameManager.GameState.InGame)
            Movement();
    }

    void Movement()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, stats.moveSpeed * Time.deltaTime);

            if (target.transform.position.x < transform.position.x && !stats.isAttacking)
                isLeft = true;
            else if(target.transform.position.x > transform.position.x && !stats.isAttacking)
                isLeft = false;
        }
    }

    public void Attack()
    {
        if(target != null)
        {
            if(Vector3.Distance(transform.position, target.transform.position) <= attackRange && !stats.isAttacking && !stats.isFainted)
            {
                stats.attackTimer = stats.attackDuration;
                StartCoroutine(AttackAnimation());
            }
        }
    }

    IEnumerator AttackAnimation()
    {
        animator.Play(ENEMY_ATTACK); // Play attack animation

        yield return new WaitForSecondsRealtime(stats.attackDuration);
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "REnemyVoid")
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);

        if (collision.gameObject.tag == "LEnemyVoid")
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);

        if (collision.gameObject.tag == "UEnemyVoid")
            transform.position = new Vector2(transform.position.x, transform.position.y + 1);

        if (collision.gameObject.tag == "DEnemyVoid")
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
    }
}
