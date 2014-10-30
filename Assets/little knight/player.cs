using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public float speed;
	private string currentAnimation;
	private string direction;
	private Vector3 way;
	private float timer;
	private const float jumpTime = 0.900f;
	private const float slashTime = 0.333f;

	// Use this for initialization
	void Start() {
		currentAnimation = "right_idle";
		direction = "right";
		way = new Vector3(1, 0, 0);
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update() {
		if (timer > 0) { timer -= Time.deltaTime; }
		else {
			GameObject.Find (currentAnimation).renderer.enabled = false;
			currentAnimation = direction + "_idle";
			GameObject.Find (currentAnimation).renderer.enabled = true;
			if (Input.GetButton ("runRight")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "right_run";
				direction = "right";
			}
			if (Input.GetButtonUp ("runRight")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "right_idle";
			}
			if (Input.GetButton ("runLeft")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "left_run";
				direction = "left";
			}
			if (Input.GetButtonUp ("runLeft")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "left_idle";
			}
			if (Input.GetButtonUp("jump")) {
				GameObject.Find(currentAnimation).renderer.enabled = false;
				currentAnimation = direction + "_jump";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = "";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = currentAnimation;
				timer = jumpTime;
			}
			if (Input.GetButtonUp("slash")) {
				GameObject.Find(currentAnimation).renderer.enabled = false;
				currentAnimation = direction + "_slash";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = "";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = currentAnimation;
				timer = slashTime;
			}
		}

		GameObject.Find(currentAnimation).renderer.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "upperBoundary") {
			GameObject.Find("dimension").transform.Translate(0, 5, 0);
		}

		if (other.gameObject.name == "lowerBoundary") {
			GameObject.Find("dimension").transform.Translate(0, -5, 0);
		}

		if (other.gameObject.name == "up") {
			way = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 35.0f), Mathf.Sin(Mathf.Deg2Rad * 35.0f), 0);
		}

		if (other.gameObject.name == "down") {
			way = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -35.0f), Mathf.Sin(Mathf.Deg2Rad * -35.0f), 0);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "up" || other.gameObject.name == "down") {
			way = new Vector3(1, 0, 0);
			transform.Translate(0, -0.2f, 0);
		}
	}

	void FixedUpdate(){
		if (Input.GetButton("runRight")) {
			float step = speed * Time.deltaTime;
			transform.Translate(way * step);
			direction = "right";
		}

		if (Input.GetButtonUp("runRight")) {
			GameObject.Find(currentAnimation).renderer.enabled = false;
			currentAnimation = "right_idle";
		}

		if (Input.GetButton("runLeft")) {
			float step = speed * Time.deltaTime;
			transform.Translate(way * -step);
			direction = "left";
		}

		if (Input.GetButtonUp("runLeft")) {
			GameObject.Find(currentAnimation).renderer.enabled = false;
			currentAnimation = "left_idle";
		}

		if (Input.GetButtonUp("jump")) {
		}
		
		GameObject.Find(currentAnimation).renderer.enabled = true;
	}
}
