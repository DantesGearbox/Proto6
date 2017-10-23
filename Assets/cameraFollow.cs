using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	public Transform t;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (transform.position.x, t.position.y + 3, transform.position.z);

		if(transform.position.y < 0){
			transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		}


	}
}
