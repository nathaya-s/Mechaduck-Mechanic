using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public CarController carController;
    public void SetJump()
    {
        print("Jump");
        carController.Jump();
    }

    public void SetIncreaseSpeed()
    {
        print("Speed");
        carController.IncreaseSpeed();
    }

    public void SetDecreaseSpeed()
    {
        print("Speed");
        carController.DecreaseSpeed();
    }
}
