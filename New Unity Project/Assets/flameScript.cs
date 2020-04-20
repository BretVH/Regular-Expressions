using UnityEngine;
using System.Collections;

public class flameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "bomb" || other.name != "Flame(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log(other.name + " triggered me");
            if(other.name != "enemy" && other.name != "eBlock" && other.name != "enemy1" && other.name != "enemy2")
               Application.LoadLevel("Menu");
        }
    }
}
