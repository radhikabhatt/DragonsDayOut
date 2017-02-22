using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour {
	public float speed = 10.0f;


	void Start () {
		Debug.Log ("plane pilot script added to:" + gameObject.name);
	}

	void Update () {

		Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f;
		//smoothing function lines. The camera moves closer to object as the speed reduces and vice versa.
		float bias = 0.96f;
		Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f - bias);

		//The below line makes the camera look forward. So the pilot.player does not feel like he is looking right at the plane,
		//rather he Canvas now look ahead and see where the plane is going
		Camera.main.transform.LookAt (transform.position + transform.forward * 30.0f);

		transform.position += transform.forward * Time.deltaTime * speed;

		speed -= transform.forward.y * Time.deltaTime * 10.0f;

		if (speed < 10.0f) {
			speed = 10.0f;
		}

		transform.Rotate (-Input.GetAxis ("Vertical"), 0.0f, -Input.GetAxis ("Horizontal"));

		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight (transform.position);
	
		//if identifies if the plane has crashed into the ground
		if (terrainHeightWhereWeAre > transform.position.y) {
			transform.position = new Vector3 (transform.position.x, terrainHeightWhereWeAre, transform.position.z);
		}
	}
}
