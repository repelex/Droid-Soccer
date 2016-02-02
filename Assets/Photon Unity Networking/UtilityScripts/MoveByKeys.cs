using UnityEngine;

/// <summary>
/// Very basic component to move a GameObject by WASD and Space.
/// </summary>
/// <remarks>
/// Requires a PhotonView. 
/// Disables itself on GameObjects that are not owned on Start.
/// 
/// thrust affects movement-thrust. 
/// JumpForce defines how high the object "jumps". 
/// JumpTimeout defines after how many seconds you can jump again.
/// </remarks>
[RequireComponent(typeof(PhotonView))]
public class MoveByKeys : Photon.MonoBehaviour
{
    public float thrust = 5000;
    public float JumpForce = 20000;
    public float drag = 0.1f;
    public float JumpTimeout = 0.5f;
    //private bool isSprite;
    private Rigidbody body;
    //private Rigidbody2D body2d;
    private Collider coll;
    public Vector3 dir;
    public float rad;

    public void Start()
    {
        //enabled = photonView.isMine;
        //this.isSprite = (GetComponent<SpriteRenderer>() != null);

        //this.body2d = GetComponent<Rigidbody2D>();
        this.body = GetComponent<Rigidbody>();
        //this.coll = GetComponent<Collider>();
        this.rad = GetComponent<SphereCollider>().radius;
        JumpForce = 20000; 
    }

    public bool IsGrounded()
    {
        //return Physics.Raycast(transform.position, -Vector3.up, coll.bounds.extents.y + 0.1f);
        Collider[] hits = Physics.OverlapSphere(transform.position, rad + 0.7f);
        if (hits.Length > 1) return true;
        return false;
        //return Physics.CheckSphere(transform.position, rad);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            return;
        }
        if (IsGrounded())
        {
            dir = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x -= 1;
                //body.AddRelativeForce(-Vector3.right * thrust);
            }

            if (Input.GetKey(KeyCode.D))
            {
                dir.x += 1;
                //body.AddRelativeForce(Vector3.right * thrust);
            }
            if (Input.GetKey(KeyCode.W))
            {
                dir.z += 1;
                //body.AddRelativeForce(Vector3.forward * thrust);
            }
            if (Input.GetKey(KeyCode.S))
            {
                dir.z -= 1;
                //body.AddRelativeForce(-Vector3.forward * thrust);
            }

            body.AddForce(dir * thrust);

            if (Input.GetKey(KeyCode.Space))
            {

                body.AddRelativeForce(Vector3.up * JumpForce);
            }
        }
    }
}
