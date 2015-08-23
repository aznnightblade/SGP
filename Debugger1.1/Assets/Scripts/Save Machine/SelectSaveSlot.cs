using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectSaveSlot : MonoBehaviour {

	[SerializeField]
	Transform nameField;

	void Start() {

	}

	public void SlotSelected() {
		//nameField.GetComponent<EnterName>().saveName = gameObject.name;
		Application.LoadLevel ("EnterName");
	}
}
