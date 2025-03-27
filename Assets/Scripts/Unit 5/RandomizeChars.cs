using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeChars : MonoBehaviour {
	public Image body1;
	public Image face1;
	public Image hair1;
	public Image kit1;
	public Image body2;
	public Image face2;
	public Image hair2;
	public Image kit2;
	public Image body3;
	public Image face3;
	public Image hair3;
	public Image kit3;
	public Image body4;
	public Image face4;
	public Image hair4;
	public Image kit4;
	public Image bodyDupe;
	public Image faceDupe;
	public Image hairDupe;
	public Image kitDupe;
	public Sprite[] body;
	public Sprite[] face;
	public Sprite[] hair;
	public Sprite[] kit;
	private Camera cam;


	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		RandomizeCharacter();
	}
	
	public void RandomizeCharacter(){
		body1.sprite = body[Random.Range(0,body.Length)];
		bodyDupe.sprite = body1.sprite;
		face1.sprite = face[0];
		faceDupe.sprite = face1.sprite;
		hair1.sprite = hair[Random.Range(0,hair.Length)];
		hairDupe.sprite = hair1.sprite;
		kit1.sprite = kit[Random.Range(0,kit.Length)];
		kitDupe.sprite = kit1.sprite;

		body2.sprite = body[Random.Range(0,body.Length)];
		face2.sprite = face[0];
		hair2.sprite = hair[Random.Range(0,hair.Length)];
		kit2.sprite = kit[Random.Range(0,kit.Length)];

		body3.sprite = body[Random.Range(0,body.Length)];
		face3.sprite = face[0];
		hair3.sprite = hair[Random.Range(0,hair.Length)];
		kit3.sprite = kit[Random.Range(0,kit.Length)];

		body4.sprite = body[Random.Range(0,body.Length)];
		face4.sprite = face[0];
		hair4.sprite = hair[Random.Range(0,hair.Length)];
		kit4.sprite = kit[Random.Range(0,kit.Length)];

	}

}
