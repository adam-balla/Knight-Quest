using UnityEngine;
using System.Collections;

public class order : MonoBehaviour {
	public string layer = "foreground";
	public int ordering;

	// Use this for initialization
	void Start () {
		renderer.sortingLayerName = layer;
		renderer.sortingOrder = ordering;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
