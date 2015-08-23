using UnityEngine;
using System.Collections;

public class SelectSaveSlot : MonoBehaviour {

	public void SlotSelected() {
		Application.LoadLevel ("EnterName");
	}
}
