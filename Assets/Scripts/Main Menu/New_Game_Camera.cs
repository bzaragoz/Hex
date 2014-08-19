using UnityEngine;
using System.Collections;

public class New_Game_Camera : MonoBehaviour {
	/*
	private float startTime;
	private float endTime;

	private static float interval = 1.5f;
	private static float halfTime = 1.0f;

	private static float speedPerSecond = 2000.0f;
	private float speedPerFrame;
	private float maxSpeed;

	// Use this for initialization
	void Start () {
		speedPerFrame = Mathf.Pow(speedPerSecond, Time.deltaTime);
		maxSpeed = 0.0f;
		startTime = Time.time;
		endTime = Time.time + interval;
	}
	
	// Update is called once per frame
	void Update(){
		if (Time.time < startTime + halfTime){
			speedPerFrame *= Mathf.Pow(speedPerSecond, Time.deltaTime);
			transform.Rotate(new Vector3(-speedPerFrame, 0.005f, 0.0f) * Time.deltaTime);
		} else if (Time.time < endTime){
			if (maxSpeed == 0.0f)
				maxSpeed = speedPerFrame;
			speedPerFrame /= Mathf.Pow(maxSpeed, Time.deltaTime*2);
			transform.Rotate(new Vector3(-speedPerFrame, 0.005f, 0.0f) * Time.deltaTime);
		} else
			transform.Rotate(new Vector3(-0.005f, 0.05f, 0.0f), Time.deltaTime);
	}*/

	void Update(){
		transform.Rotate (new Vector3 (0.005f, 0.005f, 0.0f), Time.deltaTime);
	}
}