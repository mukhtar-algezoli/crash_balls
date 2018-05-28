using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class spawn_point3 : NetworkBehaviour {
	public GameObject ball_prefab;
	public float between_time = 0.5f;
	public float max_time = 10f;
	public float min_time = 1f;
	Vector3 current_pos;
	public Material mat_1;
	public Material mat_2;
	public Material mat_3;
	public Material mat_4;
	public Material mat_5;
	public Material mat_6;
	public Material mat_7;
	public Material mat_8;
	Material mat_uni1;
	Material mat_uni2;
	private int counter = 0;
	// Use this for initialization
	void Start () {
		if (isServer == false) {
			return;
		}

		Invoke ("spawn_one",Random.Range(10f , 15f));


	}
	
	// Update is called once per frame
	void spawn_one(){

		current_pos = transform.position;

		counter = 0;
		spawnball();
		Rpcarrows_light ();
		Invoke ("spawn_two",between_time);
	}

	void spawn_two(){
		spawnball();
		Invoke ("spawn_one",Random.Range(min_time , max_time));
	
	}
	[ClientRpc]
	void Rpcarrows_light(){
		current_pos = transform.position;
		if (current_pos.x == 15.50f) {
			mat_uni1 = mat_1;
			mat_uni2 = mat_2;
		}
		if (current_pos.x == -16.09f) {
			mat_uni1 = mat_3;
			mat_uni2 = mat_4;
		}
		if (current_pos.x == 16.46f) {
			mat_uni1 = mat_5;
			mat_uni2 = mat_6;
		}
		if (current_pos.x == -15.25f) {
			mat_uni1 = mat_7;
			mat_uni2 = mat_8;
		}		

		
		Invoke ("Rpcarrows_light1",0.1f);


		//Invoke ("arrows_light2",0.2f);
		//Invoke ("arrows_light1",0.001f);
		//Invoke ("arrows_light4",0.3f);
		//Invoke ("arrows_light3",0.3f);

	}
	[ClientRpc]
	void Rpcarrows_light1(){
		mat_uni1.SetColor ("_EmissionColor", Color.green);
		counter += 1;
		Invoke ("Rpcarrows_light2",0.1f);
	}
	[ClientRpc]
	void Rpcarrows_light2(){
		mat_uni2.SetColor ("_EmissionColor", Color.green);
		Invoke ("Rpcarrows_light3",0.1f);
	}
	[ClientRpc]
	void Rpcarrows_light3(){
		mat_uni1.SetColor ("_EmissionColor", Color.black);
		Invoke ("Rpcarrows_light4",0.1f);
	}
	[ClientRpc]
	void Rpcarrows_light4(){
		mat_uni2.SetColor ("_EmissionColor", Color.black);
		if(counter == 1){
        Invoke ("Rpcarrows_light1",0.1f);
		}
		//counter = 0;
	}

   void spawnball(){
		GameObject the_ball = Instantiate (ball_prefab,current_pos,Quaternion.identity);
		NetworkServer.Spawn (the_ball);
   }
}     
