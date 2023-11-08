using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public Transform[] waypoints; // List of waypoints or path nodes.
    public float maxSpeed = 10.0f;
    public float moveSpeed = 1f;
    private int currentWaypoint = 0;
    private bool isMoving = false;
    private bool gameStarted = false;
    public float jumpForce = 5.0f;
    private Rigidbody rb;
    public float jumpCooldown = 1.0f;
    public float groundCheckDistance = 0.1f;
    private float jumpCooldownTimer;
    private bool isGrounded = true;
    public float acceleration = 2f;
    public float steeringSpeed = 2f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartGame();

            }
        }
        else
        {
            // isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (jumpCooldownTimer > 0)
            {
                jumpCooldownTimer -= Time.fixedDeltaTime;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveSpeed = moveSpeed * 2f;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                moveSpeed = 0.1f;
            }

            if (currentWaypoint < waypoints.Length)
            {
                Vector3 targetPosition = waypoints[currentWaypoint].position;
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                var rot = Quaternion.LookRotation(waypoints[currentWaypoint].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 2 * Time.deltaTime);
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
                transform.LookAt(waypoints[currentWaypoint].transform.position);
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    currentWaypoint++;
                }
                else
                {

                }
            }
            else
            {
            }
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        rb.GetComponent<PlayerControl>().enabled = true;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCooldownTimer = jumpCooldown;
        isGrounded = false;
    }
    public void StartMovement()
    {
        isMoving = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
