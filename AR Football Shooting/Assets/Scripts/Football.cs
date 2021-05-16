using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Football : MonoBehaviour
{
    public Rigidbody football;
    public Joystick joystick;
    public Vector3 MoveVector { get; set; }
    public float speed;
    public Transform camera;


    // Update is called once per frame
    void Update()
    {
        
        // get joystick input
        MoveVector = Input();

        // normalize the input direction with camera angle
        MoveVector = RotateWithCam();

        // move the ball
        Move();

        // reset the ball when it drop outside of the world
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0f, 3f, 0f);
        }
        
    }

    private Vector3 Input()
    {
        MoveVector = Vector3.zero;
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        return direction;
    }

    private Vector3 RotateWithCam()
    {
        if (camera != null)
        {
            Vector3 direction = camera.TransformDirection(MoveVector);
            direction.Set(direction.x, 0, direction.z);
            return direction.normalized * MoveVector.magnitude;
        }
        else
        {
            camera = Camera.main.transform;
            return MoveVector;
        }
    }

    private void Move()
    {
        football.AddForce(MoveVector * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
