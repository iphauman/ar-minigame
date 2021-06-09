using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Joystick joystick;
    public new Transform camera;

    private Vector3 MoveVector { get; set; }
    public float speed;
    public float turnSmooth = 0.1f;
    private float turnSmoothVelocity;

    // Update is called once per frame
    private void Update()
    {
        // get joystick input
        MoveVector = Input();

        // move the character
        Move();

        // reset the ball when it drop outside of the world
        /*if (transform.position.y < -10)
        {
            transform.position = new Vector3(0f, 3f, 0f);
        }*/
    }

    private Vector3 Input()
    {
        // init joystick input
        MoveVector = Vector3.zero;
        var horizontal = joystick.Horizontal;
        var vertical = joystick.Vertical;
        var direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }

    private void Move()
    {
        if (MoveVector.magnitude >= 0.1f)
        {
            // rotate player with camera view
            var targetAngle = Mathf.Atan2(MoveVector.x, MoveVector.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

            // smooth the rotation
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            var position = transform.position;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            position.Set(position.x, 1.1f, position.z);

            // move player with camera view
            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0, -1, 1);

            characterController.Move(moveDir.normalized * (speed * Time.deltaTime));
        }
        else
        {
            characterController.Move(new Vector3(0, -1, 0) * (speed * Time.deltaTime));

            if (transform.position.y <= -1)
            {
                transform.position = new Vector3(0, 2, 0);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic) return;

        Debug.Log(body.name);

        // apply force to the touched object
        body.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}