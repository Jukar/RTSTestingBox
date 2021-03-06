using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour {

	private GameObject selectedUnit;
	private Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
	private UnitControllerScript unitControllerScript;

	public Text selectedUnitName;
	public Text selectedUnitFaction;
	public RawImage selectedUnitIcon;
	public Text selectedUnitHealth;
	public Text selectedUnitDamage;
	public Text selectedUnitArmor;
	public Text selectedUnitRange;

	//public Canvas canvas;

	// Use this for initialization
	void Start () {
		SelectOff();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			//Debug.Log ("Se ha pulsado Mouse0");
			//Raycast y seleccionar el gameObject siempre que sea Unit o Building
			GameObject hitObject = FindHitObject();
			//Debug.LogFormat ("La etiqueta es {0}", hitObject.tag);
			if(hitObject.CompareTag("Unit")){
				//Debug.Log ("Se ha econtrado una unidad.");
				SelectOn(hitObject);
			}
			else if(hitObject.CompareTag("Ground")){
				//Debug.Log ("Se ha econtrado el suelo.");
				SelectOff();
			}
			else{
				//Debug.Log ("No se ha encontrado unidad o suelo.");
			}
		}
		if ((Input.GetKeyDown (KeyCode.Mouse1)) && (selectedUnit != null) && (unitControllerScript.speed>0)) {
			//Debug.Log ("Se ha pulsado Mouse1");
			//Si la unidad seleccionada no es NULL, esto indica dónde debe moverse la unidad
			Vector3 targetPosition = FindHitPoint();
			unitControllerScript.StartMoving (targetPosition);
		}
	}

	//Used when we click with the mouse
	private GameObject FindHitObject(){
		//Debug.Log ("Entra en FindHitObject");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			//Debug.LogFormat ("El rayo choca en el punto {0}",hit.point);
			return hit.collider.gameObject;
		} else {
			return null;
		}
	}

	//Used when we click the secondary mouse
	private Vector3 FindHitPoint(){
		//Debug.Log ("Entra en FindHitPoint");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			return hit.point;
		} else {
			return invalidPosition;
		}
	}

	private void SelectOn(GameObject selectedGO){
		selectedUnit = selectedGO;
		unitControllerScript = selectedGO.GetComponent<UnitControllerScript> ();
		//GUI
		selectedUnitName.text = unitControllerScript.unitName;
		selectedUnitFaction.text = unitControllerScript.faction;

		//selectedUnitIcon.texture = unitControllerScript.unitIcon.texture;

		selectedUnitHealth.text = unitControllerScript.healthPoints+"/"+unitControllerScript.maxHealth;
		selectedUnitDamage.text = unitControllerScript.damage.ToString();
		selectedUnitArmor.text = unitControllerScript.meleeArmor+"/"+unitControllerScript.shootArmor;
		selectedUnitRange.text = unitControllerScript.range.ToString();
	}

	private void SelectOff(){
		selectedUnit = null;
		unitControllerScript = null;
		//GUI
		selectedUnitName.text = "";
		selectedUnitFaction.text = "";
		selectedUnitIcon = null;
		selectedUnitHealth.text = "";
		selectedUnitDamage.text = "";
		selectedUnitArmor.text = "";
		selectedUnitRange.text = "";
	}
		

}
