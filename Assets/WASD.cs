using UnityEngine;

public class WASD : MonoBehaviour
{
    Rigidbody myRB;
    public float speed = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Direction());
        //Debug.Log(lookDir());
    }

    void FixedUpdate()
    {

        transform.Rotate(lookDir());
        Vector3 force = Direction();
        Debug.DrawRay(transform.position, force * 2f, Color.white);
        myRB.AddForce(force*speed*Time.fixedDeltaTime);
    }

    Vector3 Direction()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += Vector3.back;
        }

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
}


