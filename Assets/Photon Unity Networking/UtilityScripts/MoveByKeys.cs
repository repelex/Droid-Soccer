using UnityEngine;
using System.Collections;
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
    //public float JumpForce = 20000;
    public float drag = 0.1f;
    public float JumpTimeout = 0.5f;
	private int currPowerUp = 1;
	public int speedBoosts = 99;
	public int spikes = 99;
    //private bool isSprite;
    private Rigidbody body;
    //private Rigidbody2D body2d;
    private Collider coll;
    public Vector3 dir;
	public Vector3 dirFix;
    public float rad;
	public Time disableTime;

    public void Start()
    {
        //enabled = photonView.isMine;
        //this.isSprite = (GetComponent<SpriteRenderer>() != null);

        //this.body2d = GetComponent<Rigidbody2D>();
        this.body = GetComponent<Rigidbody>();
        this.coll = GetComponent<Collider>();
        this.rad = GetComponent<SphereCollider>().radius;
    }

    public bool IsGrounded()
    {
        //return Physics.Raycast(transform.position, -Vector3.up, coll.bounds.extents.y + 0.1f); //detects objects directly below ball
        Collider[] hits = Physics.OverlapSphere(transform.position, rad + 0.7f); //detects objects touching surface of ball
        if (hits.Length > 1) return true;
        return false;
    }
    // Update is called once per frame
    public void FixedUpdate()
    {
		
        if (!photonView.isMine)
        {
            return;
        }
		if (Input.GetKeyDown(KeyCode.X)) {
			if (speedBoosts > 0) {
				speedBoosts--;
				body.AddForce (body.velocity.normalized * 200000); //adds force in direction body is moving
			} 
				
		}
		if(Input.GetKeyDown(KeyCode.V)) {
			body.AddExplosionForce(250000,coll.bounds.center,10);  //adds explosive force at players location

		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			if (spikes > 0) {
				body.AddForce (-Vector3.up * 100000); //add downward force to the the player
				spikes--;
			}
		}
        if (IsGrounded())
        {
			dir = (body.transform.position - Camera.main.transform.position);
			dir.y = 0;
			dir.Normalize (); //gets base 'forward' direction based on camera and player position
            //dir = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
				dirFix = new Vector3 (dir.z, dir.y, -dir.x);
				dirFix = -dirFix;
				body.AddForce (dirFix * thrust);
                //body.AddRelativeForce(-Vector3.right * thrust);
            }

            if (Input.GetKey(KeyCode.D))
            {

				dirFix = new Vector3(dir.z, dir.y, -dir.x);
				body.AddForce (dirFix * thrust);
                //body.AddRelativeForce(Vector3.right * thrust);
            }
            if (Input.GetKey(KeyCode.W))
            {
				body.AddForce (dir * thrust);
                //dir.z += 1;
                //body.AddRelativeForce(Vector3.forward * thrust);
            }
            if (Input.GetKey(KeyCode.S))
            {
				dir = -dir;
				body.AddForce (dir * thrust);
                //body.AddRelativeForce(-Vector3.forward * thrust);
            }



            if (Input.GetKey(KeyCode.Space))
            {

                body.AddRelativeForce(Vector3.up * 20000); //player jumping
            }
        }
    }
}
