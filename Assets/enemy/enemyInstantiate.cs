using UnityEngine;
using System.Collections;

public class enemyInstantiate : MonoBehaviour {
	public int SpeedMin, SpeedMax;
	private int speed;
	public int TimeMin, TimeMax;
	private int time;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			speed = Random.Range (SpeedMin, SpeedMax);
			time = Random.Range (TimeMin, TimeMax);
			GameObject enemy0 = (GameObject)Instantiate(Resources.Load("enemy"));
			enemy0.GetComponent<enemy>().speed = speed;
			timer = time;
		}
	}
}
