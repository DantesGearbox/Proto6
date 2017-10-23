using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//Other objects
	public Transform ring2;
	public Transform dot;
	private Rigidbody2D rb;

	//Calculation variables
	private bool isDragging = false;
	private Vector3 secondPoint;
	private Vector3 normalizedMoveVector;
	private float lengthOfMoveVector = 0.0f;

	//Tweakable variables (for gamefeel)
	private float throwSpeed = 4;
	private float slowDown = 0.1f;
	private float deadzoneRadius = 0.0f;
	private float maxThrowRadius = 4.0f;
	private float elasticity = 0.85f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	float elastic(float x){
		float k = -elasticity;
		return x / k;
	}

	void activateSlowMo(){
		Time.timeScale = slowDown;
		Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
	}

	void deactivateSlowMo(){
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime;
	}

	void Update () {
		if (Input.GetKey (KeyCode.Mouse0)) {
			activateSlowMo ();
			if (!isDragging) {
				isDragging = true;
			} else {
				//Calculations
				secondPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition + new Vector3(0,0,10));
				normalizedMoveVector = (transform.position - secondPoint).normalized;
				lengthOfMoveVector = (transform.position - secondPoint).magnitude;
				if(lengthOfMoveVector > maxThrowRadius){
					lengthOfMoveVector = maxThrowRadius;
				}
				float lengthOfMoveVectorUncapped = (transform.position - secondPoint).magnitude;

				float elasticLengthOfMoveVector = elastic (lengthOfMoveVector/maxThrowRadius);
				float elasticLengthOfMoveVectorUncapped = elastic (lengthOfMoveVectorUncapped/maxThrowRadius);

				//Graphics
				dot.position = secondPoint - (normalizedMoveVector * elasticLengthOfMoveVectorUncapped) + new Vector3(0,0,-9);
				dot.transform.localScale = (-elasticLengthOfMoveVector + 0.1f) * new Vector3(1.0f, 1.0f, 1.0f);
				dot.gameObject.SetActive (true);

				//ring2.transform.localScale = elasticLengthOfMoveVector * new Vector3(maxThrowRadius*2, maxThrowRadius*2, 1);
				//ring2.position = transform.position;
				//ring2.gameObject.SetActive (true);
			}
		} else {
			deactivateSlowMo ();
			ring2.gameObject.SetActive (false);
			dot.gameObject.SetActive (false);
		}

		if(isDragging && Input.GetKeyUp (KeyCode.Mouse0)){
			isDragging = false;

			if(lengthOfMoveVector > deadzoneRadius){
				
				rb.velocity = Vector3.zero;
				rb.velocity += new Vector2(normalizedMoveVector.x, normalizedMoveVector.y) * -lengthOfMoveVector * throwSpeed;;
			}
		}
	}
}