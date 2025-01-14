using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
//public enum SIDE { Left = -2, Mid = 0, Right = 2 }
public enum HitX { Left, Mid, Right, None }
public enum HitY { UP, Mid, Down, None }
public enum HitZ { Forward, Mid, Backward, None }

public class playercontroller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;//0:left 1:middle 2:right
    public float laneDistance = 2.5f;//the distance between two lines

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding = false;
    private bool isJump = false;
    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playermanager.isGameStarted)
            return;

        //increase speed
        if (forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;

        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        //isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        //animator.SetBool("isGrounded", isGrounded);
        if (controller.isGrounded)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);

            if (SwipeManager.swipeUp)
                Jump();
        }
        else

        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            direction.y += Gravity * Time.deltaTime; // Apply gravity
        }


        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }
        //Gather the inputs on which lane we should be

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }



        //transform.position = targetPosition;
        //controller.center = controller.center; 


        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }


        //move player  
        controller.Move(direction * Time.deltaTime);


    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            direction.y = jumpForce;
            animator.SetTrigger("Jump"); // Trigger the jump animation
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Coin collected!");
            playermanager.numberofCoins++; // Increment coin count
            Destroy(other.gameObject); // Remove the coin from the scene
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            playermanager.gameover = true;
            FindObjectOfType<AudioManager>().playsound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
    public void OnCharacterColliderHit(Collider col)
    {
        hitX = GetHitX(col);
        hitY = GetHitY(col);
        hitZ = GetHitZ(col);
    }
    public HitX GetHitX(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
        float average = (min_x = max_x) / 2f - col_bounds.min.x;
        HitX hit;
        if (average > col_bounds.size.x - 0.33f)
            hit = HitX.Right;
        else if (average<0.33f)
            hit = HitX.Left;
        else
            hit = HitX.Mid;
        return hit;
    }
    public HitY GetHitY(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - char_bounds.min.y) / char_bounds.size.y;
        HitY hit;
        if (average < 0.33f)
            hit = HitY.Down;
        else if (average < 0.66f)
            hit = HitY.Mid;
        else
            hit = HitY.UP;
        return hit;
    }
    public HitZ GetHitZ(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f - col_bounds.min.z) / char_bounds.size.y;
        HitZ hit;
        if (average < 0.33f)
            hit = HitZ.Backward;
        else if (average < 0.66f)
            hit = HitZ.Mid;
        else
            hit = HitZ.Forward;
        return hit;
    }


}