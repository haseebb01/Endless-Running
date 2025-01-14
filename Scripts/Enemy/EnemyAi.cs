using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private Transform player; // The player's Transform
    private NavMeshAgent navMeshAgent; // NavMeshAgent for pathfinding
    public float moveSpeed = 5f; // Speed of the enemy movement
    public float attackDistance = 2f; // Distance at which the enemy attacks the player
    public float disappearanceTime = 3f; // Time before the enemy disappears after missing
    public string playerTag = "Player"; // Tag assigned to the player

    private bool isMissed = false; // Whether the enemy missed the player
    private float disappearTimer = 0f; // Timer for disappearance

    void Start()
    {
        // Find the player dynamically by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene! Make sure the player has the correct tag.");
        }

        // Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
        }
        else
        {
            Debug.LogError("NavMeshAgent component is missing! Add it to the enemy.");
        }
    }

    void Update()
    {
        if (playermanager.gameover || player == null) return;

        if (isMissed)
        {
            HandleDisappearance();
            return;
        }

        // Calculate the direction to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards the player using NavMeshAgent
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(player.position);
            }
        }
        else
        {
            TriggerGameOver();
        }
    }

    private void HandleDisappearance()
    {
        disappearTimer += Time.deltaTime;
        if (disappearTimer >= disappearanceTime)
        {
            Destroy(gameObject); // Destroy the enemy after the timer completes
        }
    }

    private void TriggerGameOver()
    {
        // Trigger the game over state in the PlayerManager
        playermanager.gameover = true;

        // Optionally, you can add sound effects or animations for the enemy here
        Debug.Log("Enemy triggered Game Over!");
    }

    public void MissedHit()
    {
        // Called when the enemy or player misses a hit
        isMissed = true;
        navMeshAgent.isStopped = true; // Stop the enemy from moving
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detect obstacles and adjust position
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            navMeshAgent.SetDestination(transform.position + newDirection * 2f); // Move in a new direction
        }
    }
}
