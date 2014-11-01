using UnityEngine;
using System.Collections;

public class order : MonoBehaviour {
	public int ordering;

	// Use this for initialization
	void Start () {
		renderer.sortingLayerName = "foreground";
			renderer.sortingOrder = ordering;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
