using UnityEngine;
using System.Collections;

public class dimension : MonoBehaviour {
	private GameObject mountain;
	private const float mountainLength = 30.29604f;
	private GameObject landscape0;
	private const float landscape0Length = 31.87335f;
	private GameObject landscape1;
	private const float landscape1Length = 25.39828f;
	//private GameObject rock0;
	//private const float rock0Length = 0;
	//private GameObject rock1;
	//private const float rock1Length = 0;
	private Vector3 previousPosition;
	public float mountainDistance;
	public float landscape0Distance;
	public float landscape1Distance;
	//public float rock0Distance;
	//public float rock1Distance;

	// Use this for initialization
	void Start () {
		mountain = GameObject.Find("mountain");
		landscape0 = GameObject.Find("landscape0");
		landscape1 = GameObject.Find("landscape1");
		//rock0 = GameObject.Find("rock0");
		//rock1 = GameObject.Find("rock1");
		previousPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (previousPosition != transform.localPosition) {
			float mountainRepeat = mountainLength * Mathf.Floor((transform.localPosition.x - GameObject.Find("mountainLocator").transform.localPosition.x) / mountainLength);
			mountain.transform.localPosition = new Vector3 (mountainRepeat + transform.localPosition.x / mountainDistance, transform.localPosition.y / 5 / mountainDistance, 0);
			GameObject.Find("mountainLocator").transform.localPosition = new Vector3 (transform.localPosition.x / mountainDistance, transform.localPosition.y / mountainDistance, 0);

			float landscape0Repeat = landscape0Length * Mathf.Floor((transform.localPosition.x - GameObject.Find("landscape0Locator").transform.localPosition.x) / landscape0Length);
			landscape0.transform.localPosition = new Vector3 (landscape0Repeat + transform.localPosition.x / landscape0Distance, transform.localPosition.y / 5 / landscape0Distance, 0);
			GameObject.Find("landscape0Locator").transform.localPosition = new Vector3 (transform.localPosition.x / landscape0Distance, transform.localPosition.y / landscape0Distance, 0);

			float landscape1Repeat = landscape1Length * Mathf.Floor((transform.localPosition.x - GameObject.Find("landscape1Locator").transform.localPosition.x) / landscape1Length);
			landscape1.transform.localPosition = new Vector3 (landscape1Repeat + transform.localPosition.x / landscape1Distance, transform.localPosition.y / 5 / landscape1Distance, 0);
			GameObject.Find("landscape1Locator").transform.localPosition = new Vector3 (transform.localPosition.x / landscape1Distance, transform.localPosition.y / landscape1Distance, 0);
			//rock0.transform.localPosition = new Vector3 (rock0Distance == 0 ? 0 : transform.localPosition.x / rock0Distance, rock0Distance == 0 ? 0 : transform.localPosition.y / rock0Distance, 0);
			//rock1.transform.localPosition = new Vector3 (rock1Distance == 0 ? 0 : transform.localPosition.x / rock1Distance, rock1Distance == 0 ? 0 : transform.localPosition.y / rock1Distance, 0);
			previousPosition = transform.localPosition;
		}
	}
}
