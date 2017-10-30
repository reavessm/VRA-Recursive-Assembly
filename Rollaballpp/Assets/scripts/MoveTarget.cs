using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour {

	public int range;
	public float rate;

	private int position;
	bool direction;

	// Use this for initialization
	void Start () {
		position = 0;
		direction = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (direction) {
			transform.Translate (Vector3.left * rate * Time.deltaTime);
			position++;
			if (position == range) {
				direction = !direction;
			}
		} else {
			transform.Translate (Vector3.right * rate * Time.deltaTime);
			position--;
			if (position == -range) {
				direction = !direction;
			}
		}

	}
}
