using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public enum FollowType
    {
        VelocityBased,
        RigidFollow
    }

    //public FollowType followtype;
    public float offsetX;
    public float offsetY;
    public float lerpSpeed;

    private Camera followCam;
    private Rigidbody2D playerRigidBody;

	// Use this for initialization
	void Awake () {

        playerRigidBody = GetComponent<Rigidbody2D>();

        // Get reference to main camera
        followCam = Camera.main;

        // Set initial camera position
        followCam.transform.position = new Vector3(transform.position.x + offsetX, 
            transform.position.y + offsetY, followCam.transform.position.z);
	}
	
	// Update is called once per frame
	void LateUpdate () {

        followCam.transform.position =
            Vector3.Lerp(followCam.transform.position, new Vector3(transform.position.x + offsetX + playerRigidBody.velocity.x,
                        transform.position.y + offsetY + playerRigidBody.velocity.y,
                        followCam.transform.position.z), lerpSpeed * Time.deltaTime);
	}
}
