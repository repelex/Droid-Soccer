using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
    public float JumpForce = 60000;
    public float drag = 0.1f;
    public float JumpTimeout = 0.5f;
	public int maxPowerups = 5;
	public int powerups = 3;
    //private bool isSprite;
    private Rigidbody body;
    //private Rigidbody2D body2d;
    private Collider coll;
    public Vector3 dir;
	public Vector3 dirFix;
    public float rad;
	public Time disableTime;
    private GameObject ball;
    public Text powerText;

    public void Start()
    {
        //enabled = photonView.isMine;
        //this.isSprite = (GetComponent<SpriteRenderer>() != null);

        //this.body2d = GetComponent<Rigidbody2D>();
        this.body = GetComponent<Rigidbody>();
        this.coll = GetComponent<Collider>();
        this.rad = GetComponent<SphereCollider>().radius;
        this.ball = GameObject.FindGameObjectWithTag("Ball");

        if (isGame())
        {
            powerText = GameObject.FindGameObjectWithTag("powerups").GetComponent<Text>() as Text;
        }
    }

    bool isGame()
    {
        return SceneManagerHelper.ActiveSceneName.Equals("MiniGame");
    }

    void setPowerText()
    {
        powerText.text = "Power Ups: " + powerups.ToString();
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
		if (body.position.y < -10) {
			body.position = new Vector3 (2.83f,0f,21.43f);
			body.velocity = Vector3.zero;
		}
		
        if (!photonView.isMine)
        {
            return;
        }

        if (isGame()) //we are in main game
        {
            setPowerText();

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (powerups > 0)
                {
                    powerups--;
                    body.AddForce(body.velocity.normalized * 200000); //adds force in direction body is moving
                }

            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                body.velocity = new Vector3(ball.transform.position.x - transform.position.x, ball.transform.position.y - transform.position.y, ball.transform.position.z - transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (powerups > 0)
                {
                    powerups--;
                    body.AddForce(-Vector3.up * 200000); //add downward force to the the player
                }
            }
        }
        if (IsGrounded())
        {
            /*
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
            */

            
            transform.LookAt(ball.transform.position);
			//ball.transform.position.y - transform.position.y
            Vector3 pbvec = new Vector3(ball.transform.position.x - transform.position.x, 0, ball.transform.position.z - transform.position.z);
            pbvec.Normalize();

            if (Input.GetKey(KeyCode.A))
            {
                body.AddForce(new Vector3(pbvec.z * -1, pbvec.y, pbvec.x) * thrust);
            }
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(new Vector3(pbvec.z * -1, pbvec.y, pbvec.x) * thrust *-1);
            }
            if (Input.GetKey(KeyCode.W))
            {
                body.AddForce(pbvec*thrust);
            }
            if (Input.GetKey(KeyCode.S))
            {
                body.AddForce(-1 * pbvec * thrust);
            }

            if (Input.GetKey(KeyCode.Space))
            {

				body.AddRelativeForce(Vector3.up * JumpForce); //player jumping
            }
        }
    }
}
