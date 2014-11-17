using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	public float speed;
	private string currentAnimation;
	private string direction;
	private Vector3 way, wayPrev;
	private float timer;
	private bool alive;
	private int life;
	private bool playerActive;
	
	// Use this for initialization
	void Start() {
		currentAnimation = "left_run";
		direction = "left";
		way = new Vector3(1, 0, 0);
		timer = 0.0f;
		playerActive = false;
		alive = true;
		life = Random.Range (1, 6);
	}
	
	// Update is called once per frame
	void Update() {
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			if (life <= 0) transform.renderer.enabled = false;
			float step = speed * Time.deltaTime;
			if (currentAnimation == "left_damaged" || currentAnimation == "right_damaged"){
				currentAnimation = direction + "_run";
				GetComponent<SkeletonAnimation>().loop = true;
				GetComponent<SkeletonAnimation>().state.ClearTrack(1);
				GetComponent<SkeletonAnimation>().state.SetAnimation(1, currentAnimation, true);
			}
			transform.localScale = new Vector3(direction == "right" ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			if (direction == "left") {
				transform.Translate(way * -step);
			}
			if (direction == "right") {
				transform.Translate(way * step);
			}

			if (Input.GetButtonDown("slash") && playerActive && alive) {
				timer = 0.167f;
				life--;
				currentAnimation = direction + "_damaged";
				GetComponent<SkeletonAnimation>().loop = false;
				GetComponent<SkeletonAnimation>().state.SetAnimation(1, currentAnimation, false);
				GetComponent<SkeletonAnimation>().timeScale = 1;
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "trigger") {
			playerActive = true;
		}

		if (other.gameObject.name == "up") {
			way = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 35.0f), Mathf.Sin(Mathf.Deg2Rad * 35.0f), 0);
		}
		if (other.gameObject.name == "down") {
			way = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -35.0f), Mathf.Sin(Mathf.Deg2Rad * -35.0f), 0);
		}
		
		if (other.gameObject.name == "up0") {
			transform.localPosition = new Vector3(transform.localPosition.x, -5.0f + other.transform.parent.localPosition.y, transform.localPosition.z);
		}
		if (other.gameObject.name == "up1") {
			transform.localPosition = new Vector3(transform.localPosition.x, 1.5f + other.transform.parent.localPosition.y, transform.localPosition.z);
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "trigger") {
			playerActive = false;
		}

		if (other.gameObject.name == "position") {
			direction = GameObject.Find("player").transform.localPosition.x > transform.localPosition.x ? "right" : "left";
		}

		if (other.gameObject.name == "up" || other.gameObject.name == "down") {
			way = new Vector3(1, 0, 0);
		}
	}
}
