using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour {

	//Variables
	private Animator werewolfAnim;
	public GameObject[] werewolfs = new GameObject[6];
	public int currentCharIndex = 0;
	public string currentAnimation = "battle_werewolf_idle";

	//Functions
	void Start(){
		werewolfAnim = werewolfs [currentCharIndex].transform.GetChild (0).GetComponent<Animator> ();
	}
	public void ChangeAnimation(string animToPlay){
		currentAnimation = animToPlay;
		werewolfAnim.Play (currentAnimation,0,0);
	}
	public void ChangeChar(int charIndex){
		werewolfs [currentCharIndex].gameObject.SetActive (false);
		werewolfAnim = werewolfs [charIndex].transform.GetChild (0).GetComponent<Animator> ();
		werewolfs [charIndex].gameObject.SetActive (true);
		ChangeAnimation (currentAnimation);
		currentCharIndex = charIndex;
	}
}
