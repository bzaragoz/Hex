using UnityEngine;
using System.Collections;

public class MenuCam_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0.005f, 0.005f, 0.0f));
	}
}
