using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;
    public Joystick joystick;
    public Vector3 MoveVector { get; set; }
    public float speed;
    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;
    public Transform camera;

    // Update is called once per frame
    void Update()
    {
        // get joystick input
        MoveVector = Input();

        // normalize the input direction with camera angle
        //MoveVector = RotateWithCam();

        // move the ball
        Move();

        // reset the ball when it drop outside of the world
        /*if (transform.position.y < -10)
        {
            transform.position = new Vector3(0f, 3f, 0f);
        }*/
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
        if (MoveVector.magnitude >= 0.1f)
        {
            // rotate player with camera view
            float targetAngle = Mathf.Atan2(MoveVector.x, MoveVector.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

            // smooth the rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            transform.position.Set(transform.position.x, 1.1f, transform.position.z);

            // move player with camera view
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0, -1, 1);

            CharacterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            CharacterController.Move(new Vector3(0, -1, 0)  * speed * Time.deltaTime);

            if (transform.position.y <= -1)
            {
                transform.position = new Vector3(0, 2, 0);
            }
        }
    }
}
