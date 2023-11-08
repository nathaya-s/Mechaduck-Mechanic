using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System.Collections;


public class CarController : MonoBehaviour
{
    public Transform[] waypoints;

    private float moveSpeed = 1.0f;
    private float jumpForce = 20.0f;
    private float jumpCooldown = 1.0f;
    private float maxSpeed = 10.0f;
    private float speedIncreaseAmount = 2.0f;
    private float speedDecreaseAmount = 2.0f;

    public LayerMask obstacleLayer;
    public Transform startingPosition;

    public GameObject objectToReset;

    public QuizManager quizManager;
    public Transform[] checkpoint;
    private int currentCheckpoint = 0;


    public float rotationSpeed = 5.0f;
    private int currentWaypoint = 0;

    private bool gameStarted = false;
    private Rigidbody rb;
    private bool isGrounded = true;
    private int jumpsRemaining = 2;
    private float lastJumpTime = 0.0f;


    public GameObject popupCanvas;
    private bool isFollowingWaypoints = true;

    public GameObject StartNewgame;

    public GameObject Nong;
    public GameObject GoalMK;
    public GameObject NextLevelPopup;

    [SerializeField] private AudioSource jumpEffect;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        //if (PlayerPrefs.GetInt("Startgame") == 0)
        //{
        //    StartNewgame.SetActive(true);
        //} 
        //else
        //{
        //    StartNewgame.SetActive(false);
        //}

        print("Position checkpoint " + PlayerPrefs.GetInt("Checkpoint"));
        if (PlayerPrefs.GetString("Reset") == "false")
        {
            print("-------------Resume-------------");
            transform.position = checkpoint[PlayerPrefs.GetInt("Checkpoint")].position;
            currentWaypoint = System.Array.IndexOf(waypoints, checkpoint[PlayerPrefs.GetInt("Checkpoint")]);
            currentCheckpoint = PlayerPrefs.GetInt("Checkpoint");
        }
        else
        {
            print("-------------Reset Game -------------");
            PlayerPrefs.DeleteAll();
            currentWaypoint = 0;
            currentCheckpoint = 0;
            transform.position = startingPosition.position;
        }
    }

    private void Update()
    {
        if (Nong == null)
            return;

        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameStarted = true;
            }
        }
        else if (isFollowingWaypoints && currentWaypoint < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[currentWaypoint].position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            moveSpeed = Mathf.Max(moveSpeed, 0.0f);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                if (currentCheckpoint < checkpoint.Length)
                {
                    print("currentCheckpoint = " + checkpoint[currentCheckpoint]);
                    print("currentWaypoint = " + waypoints[currentWaypoint]);
                    if (waypoints[currentWaypoint] == checkpoint[currentCheckpoint])
                    {
                        PlayerPrefs.SetInt("Checkpoint", currentCheckpoint);
                        PlayerPrefs.Save();

                        currentCheckpoint++;
                        print("New Checkpoint = " + PlayerPrefs.GetInt("Checkpoint"));
                    }
                }
                currentWaypoint++;
            }
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 0.1f, obstacleLayer))
            {
                isFollowingWaypoints = false;
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                popupCanvas.SetActive(true);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if ((isGrounded || jumpsRemaining > 0) && Time.time - lastJumpTime >= jumpCooldown)
        {
            jumpEffect.Play();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpsRemaining--;
            lastJumpTime = Time.time;
        }
    }

    public void IncreaseSpeed()
    {
        gameStarted = true;
        moveSpeed = Mathf.Min(moveSpeed + speedIncreaseAmount, maxSpeed);
    }

    public void DecreaseSpeed()
    {
        moveSpeed -= 2.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger");
        if (other.CompareTag("Goal"))
        {
            print("set show");
            GoalMK.SetActive(true);
            Nong.SetActive(false);
            NextLevelPopup.SetActive(true);
            //StartCoroutine(ActivateObjectAfterDelay(3.0f));
        }
    }
    private IEnumerator ActivateObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
    }
}
