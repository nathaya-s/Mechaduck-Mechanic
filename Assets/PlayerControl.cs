using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    public PathFollow pathFollowScript;
    private void Update()
    {
        float speedInput = Input.GetAxis("Vertical");
        pathFollowScript.moveSpeed += speedInput;
    }
}