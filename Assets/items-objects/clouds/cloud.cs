using UnityEngine;
using System.Collections;

public class cloud : MonoBehaviour {
	public float minSpeed;
	public float maxSpeed;
	public float minSize;
	public float maxSize;
	public float minHeight;
	public float maxHeight;
	public float animLength;
	private const float timeScale = 0.25f;
	private int order;
	private float speed;
	private float size;

	// Use this for initialization
	void Start () {
		speed = Random.Range(minSpeed, maxSpeed);
		size = Random.Range(minSize, maxSize);
		order = 2 * Random.Range(0, 2);
		transform.localPosition = new Vector3(Random.Range(-10, 10), Random.Range(minHeight, maxHeight), 0.0f);
		transform.localScale = new Vector3(size, size, size);
		renderer.sortingLayerName = "mountain";
		renderer.sortingOrder = order;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x > -35) {
			transform.Translate(speed * Time.deltaTime, 0, 0);
		}
		else {
			speed = Random.Range(minSpeed, maxSpeed);
			size = Random.Range(minSize, maxSize);
			order = 2 * Random.Range(0, 2);
			transform.localPosition = new Vector3(15.0f, Random.Range(minHeight, maxHeight), 0.0f);
			transform.localScale = new Vector3(size, size, size);
			renderer.sortingOrder = order;
			GetComponent<SkeletonAnimation>().AnimationName = "";
			GetComponent<SkeletonAnimation>().AnimationName = gameObject.name;
			GetComponent<SkeletonAnimation>().timeScale = timeScale;
		}
	}
}
