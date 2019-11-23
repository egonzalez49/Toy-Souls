using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC {
	public class Cube1 : MonoBehaviour
	{
		public Material m1;
		public bool isTriggered = false;

        public PopupManager popupManager;

        public AudioSource correctAudio;
		public AudioSource wrongAudio;
		public AudioSource completeAudio;

	 	float displayTime = 7.0f;
	 	bool displayMessage = false;

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

		//public StateManager statemang;

		void Awake()
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
						if (!this.isTriggered) {
							correctAudio.Play();
						}
						this.isTriggered = true;
						this.gameObject.GetComponent<Renderer>().material.color = Color.green;
					} else {
						this.isTriggered = false;
						this.gameObject.GetComponent<Renderer>().material.color = Color.red;
						wrongAudio.Play();
					}
				} else if (this.gameObject == c2) {
					if (!this.isTriggered) {
						correctAudio.Play();
					}
					this.isTriggered = true;
					this.gameObject.GetComponent<Renderer>().material.color = Color.green;
				} else if (this.gameObject == c3) {
					if (c1script.isTriggered && c2script.isTriggered && c4script.isTriggered) {
						if (!this.isTriggered) {
							correctAudio.Play();
						}
						this.isTriggered = true;
						this.gameObject.GetComponent<Renderer>().material.color = Color.green;
					} else {
						this.isTriggered = false;
						this.gameObject.GetComponent<Renderer>().material.color = Color.red;
						wrongAudio.Play();
					}
				} else if (this.gameObject == c4) {
					if (c2script.isTriggered) {
						if (!this.isTriggered) {
							correctAudio.Play();
						}
						this.isTriggered = true;
						this.gameObject.GetComponent<Renderer>().material.color = Color.green;
					} else {
						this.isTriggered = false;
						this.gameObject.GetComponent<Renderer>().material.color = Color.red;
						wrongAudio.Play();
					}
				} else {
					if (c1script.isTriggered && c2script.isTriggered && c3script.isTriggered && c4script.isTriggered) {
						if (!this.isTriggered) {
							correctAudio.Play();
							completeAudio.Play();
                            //displayMessage = true;
                            //displayTime = 7.0f;
                            player.GetComponent<StateManager>().canRoll = true;
						}

                        popupManager.generateTimedPopupMessage("You unlocked the ability to roll! Press B/C to try it out!", 5f);
                        this.isTriggered = true;
						this.gameObject.GetComponent<Renderer>().material.color = Color.green;
					} else {
						this.isTriggered = false;
						this.gameObject.GetComponent<Renderer>().material.color = Color.red;
						wrongAudio.Play();
					}
				}
			}
		}

		void OnTriggerStay(Collider other)
		{
			if (other.tag == "Player") {
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

		void OnTriggerExit(Collider other)
		{
			if (other.tag == "Player") {
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

		void Update() {
	    	displayTime -= Time.deltaTime;
	    	if (displayTime <= 0.0) {
	        	displayMessage = false;
	    	}
		}

		void OnGUI() {
			if (displayMessage) {
                //GUIStyle style = new GUIStyle();
                //style.fontSize = 28;
                //style.normal.textColor = Color.white;
                //style.alignment = TextAnchor.UpperCenter;
                //GUI.Label(new Rect(Screen.width / 2-190, Screen.height / 4.5f, 400f, 400f), "Congratulations! You unlocked the 'Roll' Ability.", style);
                //GUI.Label(new Rect(Screen.width / 2-190, Screen.height / 3.3f, 400f, 400f), "To 'Roll', press the 'C' key.", style);
            }
		}
	}
}