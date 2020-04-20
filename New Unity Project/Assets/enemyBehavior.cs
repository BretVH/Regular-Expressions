using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

   CharacterController _controller;
   Transform _transform;
    float speed = 5f;
    Vector3 moveDirection;
    Vector3 target;
    float maxRotSpeed = 200.0f;
    float minTime = 0.1f;
    float velocity;
    void move()
    {
       moveDirection = _transform.forward;
       moveDirection *= speed;
       _controller.Move(moveDirection * Time.deltaTime);
       var newRotation = Quaternion.LookRotation(target - _transform.position).eulerAngles;
       var angles = _transform.rotation.eulerAngles;
       _transform.rotation = Quaternion.Euler(angles.x,
           Mathf.SmoothDampAngle(angles.y, newRotation.y, ref velocity, minTime, maxRotSpeed),
               angles.z);
    }
    bool change; 
    float range;
    void Start () {
        _controller = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
       range = 2f;
       target = GetTarget();
       InvokeRepeating ("NewTarget",0.01f,2.0f);
    }
    void Update () {        
       if(change)
	       target = GetTarget ();
 
       if(Vector3.Distance(_transform.position,target)>range){
	      move();
       }
    }
    Vector3 GetTarget(){
       return new Vector3(Random.Range (0,300),0,Random.Range (0,300));
    }
    void NewTarget()
    {
	    int choice = Random.Range(0, 3);
	    switch (choice)
	    {
		    case 0:
			    change = true;
			    break;
		    case 1:
			    change = false;
			    break;
		    case 2:
			    target = _transform.position;
			    break;
	    }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "eBlock" && other.name != "SolidBlock" && other.name != "Bomb(Clone)" && 
                other.name != "enemy" && other.name != "enemy1"  && other.name != "enemy2")
        {
            Destroy(other.gameObject);
            Debug.Log(other.name + " triggered me");
            if( other.name != "Flame(Clone)" )
                Application.LoadLevel("Menu");
        }
    }
}
