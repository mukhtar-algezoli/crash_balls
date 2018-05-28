using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_controller : MonoBehaviour {
    Rigidbody sphere_rigidbody;
	public int sphere_speed = 1;
	public int constantVelocity = 1;
	public GameObject particle_sys;
	private Vector3 offset;
	private Vector3 velocity;
	AudioSource collision_audio;
	Material mat;
	Material mat2;
	public Material bumber1_mat;
	public Material bumber2_mat;
	public Material bumber3_mat;
	public Material bumber4_mat;
	private float pitch_range=.5f;
	private float original_pitch;
	public Animator anim;

 
	// Use this for initialization
	void Start () {
		sphere_rigidbody = GetComponent<Rigidbody>();
		//sphere_rigidbody.AddForce (new Vector3(-10,-10,-10)*sphere_speed);
		offset =  particle_sys.transform.position - transform.position ;
		collision_audio = GetComponent<AudioSource> ();
		original_pitch = collision_audio.pitch;
		//dir = particle_sys.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		sphere_rigidbody.velocity = constantVelocity * (sphere_rigidbody.velocity.normalized);	
		particle_sys.transform.position = transform.position + offset;
		//dir = particle_sys.transform.position - dir;
		velocity = sphere_rigidbody.velocity;
		particle_sys.transform.rotation = Quaternion.LookRotation (-velocity,Vector3.up);

	}
	void OnCollisionEnter(Collision collision)
	{
		/*foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}*/
		if (collision.relativeVelocity.magnitude > 2)
			collision_audio.pitch = Random.Range (original_pitch - pitch_range , original_pitch + pitch_range);
		    //collision_audio.Play();
		if (collision.gameObject.tag == "ground") {
			sphere_rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
			collision_audio.Play ();
		}
		if (collision.gameObject.tag == "bumber1") {
			mat = bumber1_mat;
			collision_audio.Play();
			mat.SetColor ("_EmissionColor", Color.white);

			Invoke ("retcolor",.1f);
		}
		if (collision.gameObject.tag == "bumber2") {
			mat = bumber2_mat;
			collision_audio.Play();
			mat.SetColor ("_EmissionColor", Color.white);

			Invoke ("retcolor",.1f);
		}
		if (collision.gameObject.tag == "bumber3") {
			mat = bumber3_mat;
			collision_audio.Play();
			mat.SetColor ("_EmissionColor", Color.white);

			Invoke ("retcolor",.1f);
		}
		if (collision.gameObject.tag == "bumber4") {
			mat = bumber4_mat;
			collision_audio.Play ();
		
			mat.SetColor ("_EmissionColor", Color.white);

			Invoke ("retcolor",.1f);
		}
		if (collision.gameObject.tag == "character_bumber") {
//			Debug.Log ("I AM ANIMATING");
			anim = collision.gameObject.GetComponentInParent<Animator> ();
			anim.Play ("character_bumber_animation");
		}
		if (collision.gameObject.tag == "character_bumber(1)") {
			//			Debug.Log ("I AM ANIMATING");
			anim = collision.gameObject.GetComponentInParent<Animator> ();
			anim.Play ("character_bumber_animation(1)");
		}
		if (collision.gameObject.tag == "character_bumber(2)") {
			//			Debug.Log ("I AM ANIMATING");
			anim = collision.gameObject.GetComponentInParent<Animator> ();
			anim.Play ("character_bumber_animation(2)");
		}
		
	}
	void retcolor(){
		
		mat.SetColor ("_EmissionColor", Color.black);
	
	}


	void OnTriggerEnter(Collider other){
		//mat2 = other.gameObject.GetComponent<Renderer> ().material;
		//mat2.SetColor ("_EmissionColor", Color.cyan);
	    
		//Invoke ("retcolor2",.1f);
		Destroy(gameObject);
		other.gameObject.GetComponent<AudioSource>().Play();


	}

	/*void retcolor2(){

		mat2.SetColor ("_EmissionColor", Color.black);

	}*/



}
