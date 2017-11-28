using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerUpper : MonoBehaviour {

    // Steamy Valve Crap
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller_ { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject tracked_object_;


    // Not-so-Steamy global variables
    private GameObject game_object_;
    private FixedJoint fixed_joint_;


	// Use this for initialization
	void Start () {
        tracked_object_ = GetComponent<SteamVR_TrackedObject>();
        fixed_joint_ = GetComponent<FixedJoint>();
	}
	
	// Update is called once per frame
	void Update () {
        
        // Check to see if controller is connected
        if (controller_ == null) {
            Debug.Log("Where is Controller?");
            return;
        }

        // Pick Up Object when Button is pressed
        if (controller_.GetPressDown(triggerButton)) {
            PickUpTheObject();
        }

        // Release Object when Button is released
        if (controller_.GetPressUp(triggerButton)) {
            DropTheObject();
        }
	}

    void PickUpTheObject() {
        if (game_object_ != null) { // if there is a game object
            fixed_joint_.connectedBody = game_object_.GetComponent<Rigidbody>; // attach object to controller
        } else {
            fixed_joint_.connectedBody = null; // you can't pickup what's not there...
        }
    }

    void DropTheObject() {
        fixed_joint_.connectedBody = null; // Drop it like it's hot!
    }

    void OnTriggerStay(Collider collider) { // Called when Object is within our grasp
        if (collider.CompareTag("CanBePickedUp")) { // If you can pick it up
            game_object_ = collider.gameObject;     // Set thing to be picked up
        }
    }

    void OnTriggerExit(Collider collider) { // called once object leaves our hand
        game_object_ = null; // make sure we don't pick it up when its not in our hand
    }
}
