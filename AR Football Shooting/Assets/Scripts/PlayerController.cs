using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Joystick joystick;
    public new Transform camera;

    // character movement
    private Vector3 MoveVector { get; set; }
    public float speed;
    public float turnSmooth = 0.1f;
    private float turnSmoothVelocity;
    [Range(-10, -1)] public int fallingBoundary;

    // football control
    public Rigidbody football;
    public float movePower;
    public float shotPower;
    public float detectRange;

    // Update is called once per frame
    private void Update()
    {
        // get joystick input
        MoveVector = Input();

        // move the character
        Move();
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
            // get rotation value with camera view
            var targetAngle = Mathf.Atan2(MoveVector.x, MoveVector.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

            // smooth the rotation
            var smoothAngle =
                Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);

            // rotate the character
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            // move the character
            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * (speed * Time.deltaTime));
        }

        // add gravity to the character
        if (!characterController.isGrounded)
        {
            characterController.Move(new Vector3(0f, -9.81f, 0f) * Time.deltaTime);
        }

        // reset the character when it drops outside of the ground
        if (transform.position.y < fallingBoundary)
        {
            transform.position = new Vector3(0f, 3f, 0f);
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.B))
        {
            ShotBall();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic) return;

        // Debug.Log(body.name);

        if (body.CompareTag("Football"))
        {
            // hold the body
            football = body;
            MoveBall();
        }
    }

    public void ShotBall()
    {
        if (!football) return;

        var distance = Vector3.Distance(transform.position, football.transform.position);
        if (distance >= detectRange) return;


        // add force to the ball
        football.AddForce(transform.forward * shotPower, ForceMode.Impulse);
    }

    private void MoveBall()
    {
        if (!football) return;

        // update the dribbler
        football.GetComponent<Football>().dribbler = transform.name;

        // add force to the ball
        football.AddForce(transform.forward * movePower, ForceMode.Impulse);
        // football.transform.SetParent(transform);
    }
}