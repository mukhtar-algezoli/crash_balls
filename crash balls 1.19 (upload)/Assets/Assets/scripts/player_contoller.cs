using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player_contoller : NetworkBehaviour {
	public float speed = 1f;
	public float joystick_speed = 1f;
	public int counter = 0;
	[SyncVar] public string playerID;
	public float z_dis = 0;
    GameObject dish;
//	protected Joystick joystick;
	Camera playerCam;
	private	int dish_rotation = 1; 
	public Material dish_mat;

	void Awake()
	{
		dish = this.transform.Find ("character_dish_new3").gameObject;
		playerCam = GetComponentInChildren<Camera>();
		playerCam.gameObject.SetActive(false);

	}
	void Start () {
		if (isLocalPlayer == false) {
			return;
		}
		//joystick = FindObjectOfType<Joystick> ();

		if (playerID == "Player 5") {
			transform.position = new Vector3 (20, 4, 0);
			transform.Rotate (new Vector3 (0, 0, 0));
		}
//		if (playerID == "Player 6") {
//			transform.position = new Vector3 (-19, 2, 0);
//			transform.Rotate (new Vector3 (0, 90, 0));	
//		}


	}
	public override void OnStartLocalPlayer ()
	{ 
		string myPlayerID = string.Format("Player {0}", netId.Value);
		CmdSetPlayerID(myPlayerID);
		playerCam.gameObject.SetActive(true);
	}
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer == false)
		{
			return;
		}
		if (playerID == "Player 6" && counter == 0) {
			Debug.Log (playerID);			
				transform.position = new Vector3 (-20, 4, 0);
				transform.Rotate (new Vector3 (0, 180, 0));	
		    	//dish_mat.SetColor ("_Color",Color.yellow);
			    counter += 1;
		}

	var z = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
//		var z = joystick.Horizontal * joystick_speed;   
		if (z_dis < 11.7f  && z_dis > -11.3f) {
			transform.Translate (0, 0, z);
			z_dis += z;
			if (Input.GetKeyDown("right")) {
				dish_rotation = 10;
//				Quaternion originalRot = transform.rotation;    
//				dish.transform.rotation = originalRot * Quaternion.AngleAxis(30, Vector3.up);
//				dish.transform.Rotate (10,0,0);
				//Quaternion target = Quaternion.;

				// Dampen towards the target rotation
				//dish.transform.rotation = target;

			}
			if (Input.GetKeyUp("right")) {
				dish_rotation = 1;
			}
			if (Input.GetKeyDown("left")) {
				dish_rotation = -10;
			}
			if (Input.GetKeyUp("left")) {
				dish_rotation = -1;
			}
			dish.transform.Rotate (0,0,dish_rotation);
		}
		if (z_dis < 11.7f  && z_dis > -11.3f) {
			transform.Translate (0, 0, z);
			z_dis += z;
 									          }
		if (z_dis > 11.7f && z < 0) {
			transform.Translate (0, 0, z);
			z_dis += z;
									}
		if (z_dis < -11.3f && z > 0) {
			transform.Translate (0, 0, z);
			z_dis += z;
									 }
	}
	[Command]
	void CmdSetPlayerID(string newID)
	{   
		playerID = newID;

	}

}
