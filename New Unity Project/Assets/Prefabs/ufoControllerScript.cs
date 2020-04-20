using UnityEngine;
using System.Collections;

public class ufoControllerScript : MonoBehaviour 
{
	public float maxSpeed = 10f;
	bool facingRight = true;
	public GameObject bomb;
    public GameObject flame5;
    public GameObject flame1;
    public GameObject flame2;
    public GameObject flame3;
    public GameObject flame4;
    public int numOfBomb = 1;
    private float timeLeft = 0f;
    float totalTime = 3f;
    private float flameTimeLeft = 0f;
    private float totalFlameTime = 2f;
    private bool bomber;
    // Use this for initialization
 
    
	// Use this for initialization
	void Start () 
	{
        timeLeft = totalTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float move = Input.GetAxis ("Horizontal");
		float move2 = Input.GetAxis ("Vertical");
		

		if (Input.GetButtonDown("Jump") && numOfBomb == 1) 
		{
			bomb = (GameObject)Instantiate (Resources.Load("Bomb"), transform.position, transform.rotation);
            bomb.SetActive(true);
            bomb.layer = 0;
            timeLeft = totalTime;
            numOfBomb = 0;
            bomber = true;
		}

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, move2 * maxSpeed);

		if (move > 0 && !facingRight) 
						Flip ();
	    else if (move < 0 && facingRight)
						Flip ();
        timeLeft -= Time.deltaTime;
        flameTimeLeft -= Time.deltaTime;
        if (timeLeft < 0f && bomber)
        {
            flame5 = (GameObject)Instantiate(Resources.Load("Flame"), bomb.transform.position, bomb.transform.rotation);
            Vector3 newPositionl = new Vector3(
            bomb.transform.position.x + .5f,
            bomb.transform.position.y,
            bomb.transform.position.z );
            Vector3 newPositionu = new Vector3(
            bomb.transform.position.x ,
            bomb.transform.position.y + .5f,
            bomb.transform.position.z);
            Vector3 newPositionr = new Vector3(
            bomb.transform.position.x -.5f,
            bomb.transform.position.y ,
            bomb.transform.position.z );
            Vector3 newPositiond = new Vector3(
            bomb.transform.position.x ,
            bomb.transform.position.y -.5f,
            bomb.transform.position.z );
            flame1 = (GameObject)Instantiate(Resources.Load("Flame"), newPositiond , bomb.transform.rotation);
            flame2 = (GameObject)Instantiate(Resources.Load("Flame"), newPositionl, bomb.transform.rotation);
            flame3 = (GameObject)Instantiate(Resources.Load("Flame"), newPositionr, bomb.transform.rotation);
            flame4 = (GameObject)Instantiate(Resources.Load("Flame"), newPositionu, bomb.transform.rotation);
            Destroy(bomb);
            numOfBomb = 1;
            flameTimeLeft = totalFlameTime;
            bomber = false;
        }
        if (flameTimeLeft < 0f)
        {
            Destroy(flame1);
            Destroy(flame2);
            Destroy(flame3);
            Destroy(flame4);
            Destroy(flame5);
        }
	}

    void Flip( )
    {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
}
