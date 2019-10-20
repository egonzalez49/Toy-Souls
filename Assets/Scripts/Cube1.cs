using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube1 : MonoBehaviour
{
	public Material m1;
	public bool isTriggered = false;

	public GameObject c1;
	public GameObject c2;
	public GameObject c3;
	public GameObject c4;
	public GameObject c5;

	public GameObject player;

	public Cube1 c1script;
	public Cube1 c2script;
	public Cube1 c3script;
	public Cube1 c4script;
	public Cube1 c5script;

	void Start()
	{
		c1script = c1.GetComponent<Cube1>();
		c2script = c2.GetComponent<Cube1>();
		c3script = c3.GetComponent<Cube1>();
		c4script = c4.GetComponent<Cube1>();
		c5script = c5.GetComponent<Cube1>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Debug.Log("Object has entered trigger");
			if (this.gameObject == c1) {
				if (c2script.isTriggered && c4script.isTriggered) {
					this.isTriggered = true;
					this.gameObject.GetComponent<Renderer>().material.color = Color.green;
				} else {
					this.isTriggered = false;
					this.gameObject.GetComponent<Renderer>().material.color = Color.red;
				}
			} else if (this.gameObject == c2) {
				this.isTriggered = true;
				this.gameObject.GetComponent<Renderer>().material.color = Color.green;
			} else if (this.gameObject == c3) {
				if (c1script.isTriggered && c2script.isTriggered && c4script.isTriggered) {
					this.isTriggered = true;
					this.gameObject.GetComponent<Renderer>().material.color = Color.green;
				} else {
					this.isTriggered = false;
					this.gameObject.GetComponent<Renderer>().material.color = Color.red;
				}
			} else if (this.gameObject == c4) {
				if (c2script.isTriggered) {
					this.isTriggered = true;
					this.gameObject.GetComponent<Renderer>().material.color = Color.green;
				} else {
					this.isTriggered = false;
					this.gameObject.GetComponent<Renderer>().material.color = Color.red;
				}
			} else {
				if (c1script.isTriggered && c2script.isTriggered && c3script.isTriggered && c4script.isTriggered) {
					this.isTriggered = true;
					this.gameObject.GetComponent<Renderer>().material.color = Color.green;
				} else {
					this.isTriggered = false;
					this.gameObject.GetComponent<Renderer>().material.color = Color.red;
				}
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		//Debug.Log("Object is within trigger");
	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log("Object has exited trigger");
		if (!this.isTriggered) {
			c1.GetComponent<Renderer>().material = m1;
			c2.GetComponent<Renderer>().material = m1;
			c3.GetComponent<Renderer>().material = m1;
			c4.GetComponent<Renderer>().material = m1;
			c5.GetComponent<Renderer>().material = m1;
			c1script.isTriggered = false;
			c2script.isTriggered = false;
			c3script.isTriggered = false;
			c4script.isTriggered = false;
			c5script.isTriggered = false;
		}
	}
}