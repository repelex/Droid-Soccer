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
	private GameObject timer;
	public float lastPowerup = 300;
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
			this.timer = GameObject.FindGameObjectWithTag ("Timer");
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
			transform.LookAt(ball.transform.position);
			//ball.transform.position.y - transform.position.y
			Vector3 pbvec = new Vector3(ball.transform.position.x - transform.position.x, 0, ball.transform.position.z - transform.position.z);
			pbvec.Normalize();

			if (timer.GetComponent<Timer>().GetTime() < lastPowerup - 5) {
				if (powerups < maxPowerups) {
					powerups++;
					lastPowerup = timer.GetComponent<Timer>().GetTime();
				}
			}
            setPowerText();


			for (var i = 0; i < Input.touchCount; ++i) {
				Debug.Log (Input.GetTouch (i).position.x + " " + Input.GetTouch (i).position.y);
				if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary) {
					if (Input.GetTouch (i).phase == TouchPhase.Began) {
						if (IsGrounded () && Input.GetTouch (i).position.x > Screen.width / 1.91 && Input.GetTouch (i).position.x < Screen.width / 1.52 && Input.GetTouch (i).position.y < Screen.height / 4.11) {
							body.AddRelativeForce (Vector3.up * JumpForce); //player jumping
						} else if (Input.GetTouch (i).position.x < Screen.width / 1.29 && Input.GetTouch (i).position.x > Screen.width / 1.52 && Input.GetTouch (i).position.y < Screen.height / 4.11) {
							if (powerups > 0) {
								powerups--;
								body.AddForce (body.velocity.normalized * 200000); //adds force in direction body is moving
							}
						} else if (Input.GetTouch (i).position.x > Screen.width / 1.29 && Input.GetTouch (i).position.x < Screen.width / 1.14 && Input.GetTouch (i).position.y < Screen.height / 4.11) {
							if (powerups > 0) {
								powerups--;
								body.AddForce (-Vector3.up * 200000); //add downward force to the the player
							}
						} else if (Input.GetTouch (i).position.x > Screen.width / 1.14 && Input.GetTouch (i).position.y < Screen.height / 4.11) {
							if (powerups >= maxPowerups) {
								Vector3 temp = new Vector3 (ball.transform.position.x - transform.position.x, ball.transform.position.y - transform.position.y, ball.transform.position.z - transform.position.z);
								if (temp.magnitude > 80) {
									powerups = 0;


									temp.Scale (new Vector3 (100 / temp.magnitude, 100 / temp.magnitude, 100 / temp.magnitude));
									Debug.Log (temp.magnitude);
									body.velocity = temp;
								}

							}
						}
					}
					if (IsGrounded() && Input.GetTouch (i).position.x>Screen.width/12.57 &&Input.GetTouch (i).position.x<Screen.width/4.31 && Input.GetTouch (i).position.y<Screen.height/1.76 && Input.GetTouch (i).position.y>Screen.height/2.41) {
						
							body.AddForce(pbvec*thrust);

					}
					else if (IsGrounded() && Input.GetTouch (i).position.x>0 &&Input.GetTouch (i).position.x<Screen.width/7.33 && Input.GetTouch (i).position.y<Screen.height/2.41 && Input.GetTouch (i).position.y>Screen.height/4.11) {

						body.AddForce(new Vector3(pbvec.z * -1, pbvec.y, pbvec.x) * thrust);

					}
					else if (IsGrounded() && Input.GetTouch (i).position.x<Screen.width/4 &&Input.GetTouch (i).position.x>Screen.width/7.33 && Input.GetTouch (i).position.y<Screen.height/2.41 && Input.GetTouch (i).position.y>Screen.height/4.11) {

						body.AddForce(new Vector3(pbvec.z * -1, pbvec.y, pbvec.x) * thrust *-1);

					}
					else if (IsGrounded() && Input.GetTouch (i).position.x>Screen.width/12.57 &&Input.GetTouch (i).position.x<Screen.width/4.31 && Input.GetTouch (i).position.y>0 && Input.GetTouch (i).position.y<Screen.height/2.41) {

						body.AddForce(-1 * pbvec * thrust);

					}
				}
			}


         
        }
    }
}
