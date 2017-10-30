using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour {

	public float speed;
    public ParticleSystem victory_effect;
    public ParticleSystem landing_effect;

	public SphereCollider col;
	public LayerMask groundLayers;

	private Rigidbody rb;
    private bool finished;

	void Start() { // Called on the first frame the script is active in.
		rb = GetComponent<Rigidbody> (); // This will grab the object's Rigidbody component, if such a component exists.
        finished = false;
		col = GetComponent<SphereCollider> ();
	}

    void Update() { // This updates before every frame.
		if ((isOnGround()) && (Input.GetKeyDown (KeyCode.Space))) {
			rb.AddForce (Vector3.up * 20, ForceMode.Impulse);
		}
		if ( transform.position.y < -125 ) {
			SceneManager.LoadScene ("youlose");
		}
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bullseye")
        {
            finished = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            victory_effect.Play ();
			SceneManager.LoadScene ("endgame");
        }
        else if (col.gameObject.tag == "Platform")
        {
            landing_effect.Play ();
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            landing_effect.Stop ();
        }
    }

    void FixedUpdate() { //This updates before physics calculations.
		float movehorizontal = Input.GetAxis ("Horizontal");
		float movevertical = Input.GetAxis ("Vertical");
		float mod_speed;

		Vector3 movement = new Vector3 (movehorizontal, 0.0f, movevertical);

		if (Input.GetKey (KeyCode.LeftShift)) {
			mod_speed = speed + 50;
		} else {
			mod_speed = speed - 50;
		}

        if (finished == false)
        {
            rb.AddForce (movement * mod_speed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
	}

	private bool isOnGround() {
		return Physics.CheckCapsule (col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
	}
}
