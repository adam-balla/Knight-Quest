using UnityEngine;
using System.Collections;

public class flower : MonoBehaviour {
	private bool alive;
	private bool playerActive;

	// Use this for initialization
	void Start () {
		playerActive = false;
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("slash") && playerActive && alive) {
			if(Random.Range(0, 2) == 0){
				GetComponent<SkeletonAnimation>().loop = false;
				GetComponent<SkeletonAnimation>().state.AddAnimation(1, "animation4", false, 0.0f);
				GetComponent<SkeletonAnimation>().timeScale = 1;
			} else {
				GetComponent<SkeletonAnimation>().loop = false;
				GetComponent<SkeletonAnimation>().state.ClearTrack(1);
				GetComponent<SkeletonAnimation>().state.AddAnimation(1, "animation1", false, 0.0f);
				GetComponent<SkeletonAnimation>().timeScale = 0.25f;
				alive = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name == "player") {
			playerActive = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.name == "player") {
			playerActive = false;
		}
	}
}
