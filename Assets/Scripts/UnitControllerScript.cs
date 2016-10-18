using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitControllerScript : MonoBehaviour {

    public string unitName;
    public string faction;
	public Sprite unitIcon;

    public int healthPoints;
    public int maxHealth;

    public int damage;
    public int meleeArmor;
    public int shootArmor;
    public float range;

    public float speed;
    public float attackSpeed;

	private bool moving;
	private Vector3 targetPosition;
	private Rigidbody rB;


	// Use this for initialization
	void Start () {
		gameObject.tag = "Unit";
		moving = false;
		rB = GetComponent<Rigidbody> ();
	}

	void Awake(){
		//Debug.Log ("Unidad " + unitName + " creada.");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (moving) {
			Move ();
			Rotate ();
		}
	}

	public void StartMoving(Vector3 target){
		transform.LookAt (targetPosition);
		targetPosition = target;
		targetPosition.y = 0;
		moving = true;
		//Debug.Log ("Target: "+targetPosition);
	}

	private void Move(){
		//Debug.Log("Me estoy moviendo");
		//transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
		rB.velocity = rB.transform.forward * speed;
		if ((Vector3.Distance(transform.position,targetPosition)) < 0.5) {
			rB.velocity =rB.transform.forward*0f;
			moving = false;
		}
	}

	private void Rotate(){
		transform.LookAt (targetPosition);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Unit") {
			//Debug.Log (gameObject.name + " ha colisionado con " + other.gameObject.name);
			//other.rigidbody.isKinematic = true;
			//other.rigidbody.velocity = rB.transform.forward * 0f;
			//other.rigidbody.isKinematic = false;
			rB.velocity=rB.transform.forward * 0f;
			moving = false;
			//other.rigidbody.velocity = rB.transform.forward * 0f;
		}
	}

	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "Unit") {
			//Debug.Log(gameObject.name + " ya no colisiona con " + other.gameObject.name);
			//other.rigidbody.isKinematic = false;
			other.rigidbody.velocity = rB.transform.forward * 0f;
			//rB.velocity=rB.transform.forward * 0f;
			//rB.velocity=rB.transform.forward * 0f;
			//other.rigidbody.isKinematic = true;
		}
	}

}
