using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () 
	{
		if (Input.GetButtonDown("Fire1"))
            Application.LoadLevel("NetworkMenu");
        if(Input.GetButtonDown("Fire2"))
            Application.LoadLevel("Screen1");
    }
}
