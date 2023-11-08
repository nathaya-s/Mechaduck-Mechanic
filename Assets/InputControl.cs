using UnityEngine;

public class InputControl : MonoBehaviour
{
    public PathFollow pathFollowScript;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleMovement();
        }
    }

    private void ToggleMovement()
    {
        if (!pathFollowScript)
            return;
        if (!pathFollowScript.enabled)
        {
            pathFollowScript.StartMovement();
        }
        else
        {
            pathFollowScript.enabled = false;
        }
    }
}
