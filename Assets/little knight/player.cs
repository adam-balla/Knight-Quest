using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public float speed;
	public string currentAnimation;
	public string direction;
	private Vector3 way, wayPrev;
	private float timer;
	private bool bam;
	private const float jumpTime = 0.900f;
	private const float slashTime = 0.333f;

	// Use this for initialization
	void Start() {
		currentAnimation = "right_idle";
		direction = "right";
		way = new Vector3(1, 0, 0);
		wayPrev = way;
		timer = 0.0f;
		bam = false;
	}
	
	// Update is called once per frame
	void Update() {
		if (timer > 0) { timer -= Time.deltaTime; }
		else {
			GameObject.Find (currentAnimation).renderer.enabled = false;
			currentAnimation = direction + "_idle";
			GameObject.Find (currentAnimation).renderer.enabled = true;
			GameObject.Find(direction + "_bam").renderer.enabled = false;

			if (Input.GetButton ("runRight") && !Input.GetButton ("runLeft")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "right_run";
				direction = "right";
				way = (way.x == 0) ? wayPrev : way;
			}
			if (Input.GetButton ("runLeft") && !Input.GetButton ("runRight")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "left_run";
				direction = "left";
				way = (way.x == 0) ? wayPrev : way;
			}

			if (direction == "right" && Input.GetButtonUp ("runRight")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "right_idle";
			}

			if (direction == "left" && Input.GetButtonUp ("runLeft")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "left_idle";
			}

			if (Input.GetButtonDown("jump")) {
				GameObject.Find(currentAnimation).renderer.enabled = false;
				currentAnimation = direction + "_jump";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = "";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = currentAnimation;
				timer = jumpTime;
			}

			if (Input.GetButtonDown("slash")) {
				GameObject.Find(currentAnimation).renderer.enabled = false;
				currentAnimation = direction + "_slash";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = "";
				GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = currentAnimation;
				timer = slashTime;
				wayPrev = (way.x == 0) ? wayPrev : way;
				way = new Vector3(0, 0, 0);
				if (bam) {
					GameObject.Find(direction + "_bam").renderer.enabled = true;
					GameObject.Find(direction + "_bam").GetComponent<SkeletonAnimation>().AnimationName = "";
					GameObject.Find(direction + "_bam").GetComponent<SkeletonAnimation>().AnimationName = "bam0";
				}
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

		if (other.gameObject.tag == "hittable") {
			bam = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "up" || other.gameObject.name == "down") {
			way = new Vector3(1, 0, 0);
			transform.Translate(0, 0, 0);
		}

		if (other.gameObject.tag == "hittable") {
			bam = false;
		}
	}

	void FixedUpdate(){
			if (Input.GetButton ("runRight") && !Input.GetButton ("runLeft")) {
				float step = speed * Time.deltaTime;
				transform.Translate (way * step);
				direction = "right";
			}
			if (Input.GetButton ("runLeft") && !Input.GetButton ("runRight")) {
				float step = speed * Time.deltaTime;
				transform.Translate (way * -step);
				direction = "left";
			}
		if (timer <= 0) {
			if (direction == "right" && Input.GetButtonUp ("runRight")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "right_idle";
			}

			if (direction == "left" && Input.GetButtonUp ("runLeft")) {
				GameObject.Find (currentAnimation).renderer.enabled = false;
				currentAnimation = "left_idle";
			}
		}
		
		GameObject.Find(currentAnimation).renderer.enabled = true;
	}
}
