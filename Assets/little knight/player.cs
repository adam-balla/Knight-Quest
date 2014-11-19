using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public float speed;
	private string currentAnimation;
	private string direction;
	private Vector3 way, wayPrev;
	private float timer;
	private bool bam;
	private const float jumpTime = 0.900f;
	private const float slashTime = 0.333f;
	private const float damagedTime = 0.167f;

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
		if (timer > 0) {
			float step = speed * Time.deltaTime;
			timer -= Time.deltaTime;
			if (currentAnimation == "right_jump") {
				if (Input.GetButton("runLeft")) {
					GameObject.Find("right_jump").renderer.enabled = false;
					currentAnimation = "left_jump";
					GameObject.Find("left_jump").renderer.enabled = true;
				}
			}
			if (currentAnimation == "left_jump") {
				if (Input.GetButton("runRight")) {
					GameObject.Find("left_jump").renderer.enabled = false;
					currentAnimation = "right_jump";
					GameObject.Find("right_jump").renderer.enabled = true;
				}
			}
			if (currentAnimation == "right_damaged") {
				transform.Translate (way * -step);
			}
			if (currentAnimation == "left_damaged") {
				transform.Translate (way * step);
			}
		}
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
				GameObject.Find("right_jump").GetComponent<SkeletonAnimation>().AnimationName = "";
				GameObject.Find("right_jump").GetComponent<SkeletonAnimation>().AnimationName = "right_jump";
				GameObject.Find("left_jump").GetComponent<SkeletonAnimation>().AnimationName = "";
				GameObject.Find("left_jump").GetComponent<SkeletonAnimation>().AnimationName = "left_jump";
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

		if (other.gameObject.name == "up") {
			way = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 35.0f), Mathf.Sin(Mathf.Deg2Rad * 35.0f), 0);
		}
		if (other.gameObject.name == "down") {
			way = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -35.0f), Mathf.Sin(Mathf.Deg2Rad * -35.0f), 0);
		}

		if (other.gameObject.name == "up0") {
			transform.localPosition = new Vector3(transform.localPosition.x, -6.670548f + other.transform.parent.localPosition.y, transform.localPosition.z);
		}
		if (other.gameObject.name == "up1") {
			transform.localPosition = new Vector3(transform.localPosition.x, 1.629452f + other.transform.parent.localPosition.y, transform.localPosition.z);
		}

		if (other.gameObject.tag == "hittable") {
			bam = true;
		}
		if (currentAnimation != "left_jump" && currentAnimation != "right_jump")
		if (other.gameObject.name == "damage") {
			GameObject.Find(currentAnimation).renderer.enabled = false;
			currentAnimation = direction + "_damaged";
			GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = "";
			GameObject.Find(currentAnimation).GetComponent<SkeletonAnimation>().AnimationName = currentAnimation;
			timer = damagedTime;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "up" || other.gameObject.name == "down") {
			way = new Vector3(1, 0, 0);
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
		} else {
			if (currentAnimation == "right_jump" || currentAnimation == "left_jump") {
				if (timer - Time.deltaTime < 0){
					GameObject.Find ("circle").transform.localPosition = new Vector3(0, 0, 0);
				} else {
					GameObject.Find ("circle").transform.Translate(0.0f, 1.5f*(timer - 0.45f), 0.0f);
				}
			}
		}
		
		GameObject.Find(currentAnimation).renderer.enabled = true;
	}
}
