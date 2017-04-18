using UnityEngine;
using System.Collections;

public class ToggleActive : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void toggleActive()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
