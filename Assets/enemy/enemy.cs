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
		RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 10.0f), -Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("field"));
		float y = hitInfo.point.y;
		Vector3 pos = transform.position;
		pos.y = y;
		transform.position = pos;

		float step = speed * Time.deltaTime;
		if (timer > 0) {
			if (currentAnimation == "left_damaged" || currentAnimation == "right_damaged") {
				if (direction == "left") {
					transform.Translate(2 * way * step);
				}
				if (direction == "right") {
					transform.Translate(2 * way * -step);
				}
			}
			timer -= Time.deltaTime;
		} else {
			if (life <= 0) Destroy (gameObject);
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
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "trigger") {
			playerActive = false;
		}

		if (other.gameObject.name == "position") {
			direction = GameObject.Find("player").transform.localPosition.x > transform.localPosition.x ? "right" : "left";
		}
	}
}
