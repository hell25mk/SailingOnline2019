using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class NCMBTest : MonoBehaviour {

    // Use this for initialization
    void Start() {

        NCMBObject testClass = new NCMBObject("TestClass");

        testClass["message"] = "Hello,NCMB";

        testClass.SaveAsync();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
