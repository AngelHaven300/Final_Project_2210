using UnityEngine;

public class wasd3D : MonoBehaviour
{

    public static wasd3D _instance;
    Rigidbody myRB;
    public float speed = 50f;

    public bool jumped = false;
    public bool grounded = false;
    public float jumpForce = 50f;
    public KeyCode jumpKey;

    public Animator myAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
         if (wasd3D._instance == null)
        {
            _instance = this;
        }
        else { Destroy(this); }
    }
    
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Direction());
        //Debug.Log(lookDir());

        if (Input.GetKeyUp(jumpKey) && grounded)
        {
            jumped = true;
        }

        myAnimator.SetFloat("speed", myRB.linearVelocity.magnitude);
        myAnimator.SetBool("grounded", grounded);
        myAnimator.SetBool("jumped", jumped);

        Debug.DrawRay(transform.position, myRB.linearVelocity, Color.cyan);
        Debug.DrawRay(transform.position + Vector3.up, transform.forward*3f, Color.blue);

        float relativeDirection = Vector3.Dot(myRB.linearVelocity.normalized, transform.forward);
        Debug.Log("Relative Direction: " + relativeDirection);
        myAnimator.SetFloat("lookDot", relativeDirection);
    }

    void FixedUpdate()
    {
        Vector3 rot = lookDir();
        Debug.Log("rot: " + rot);
        Debug.Log("angular Vel: " + myRB.angularVelocity);
        myRB.angularVelocity = Vector3.zero;
        transform.Rotate(rot);
        Vector3 force = Direction();
        Debug.DrawRay(transform.position, force * 2f, Color.white);
        myRB.AddForce(force * speed * Time.fixedDeltaTime);

        if(jumped)
        {
            Debug.Log("Jumping");
            Jump(Time.fixedDeltaTime);
            jumped = false;
        }
    }

    Vector3 Direction()
    {
        Vector3 dir = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        dir = new Vector3(x, 0, z);
        dir = transform.TransformDirection(dir);
        return dir;
    }

    Vector3 lookDir()
    {
        Vector3 dir = Vector3.zero;
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");
        dir = new Vector3(0, x, 0);
        return dir;
    }

    void Jump(float deltaTime)
    {
        Debug.Log("Jumped");
        myRB.AddForce(Vector3.up * jumpForce * deltaTime, ForceMode.Impulse);
    }

    void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }


}
