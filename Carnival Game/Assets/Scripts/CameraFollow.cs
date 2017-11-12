using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public enum FollowType
    {
        VelocityBased,
        RigidFollow
    }

    //public Offests for camera;
    public float offsetX;
    public float offsetY;
    public float lerpSpeed;

    public BoxCollider2D boundingBox;

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

        // Lerp position based on player velocity
        followCam.transform.position =
            Vector3.Lerp(followCam.transform.position, new Vector3(transform.position.x + offsetX + playerRigidBody.velocity.x,
                        transform.position.y + offsetY + playerRigidBody.velocity.y,
                        followCam.transform.position.z), lerpSpeed * Time.deltaTime);

        // Stay within bounding box if there is one assigned
        if (boundingBox != null)
        {

            Vector3 cameraTrans = followCam.transform.position;

            float boundsOffsetY = (followCam.orthographicSize * 2)/2;
            float boundsOffsetX = (boundsOffsetY * followCam.aspect);

            Debug.Log("X: " + boundsOffsetX + " Y: " + boundsOffsetY);

            if (cameraTrans.x - boundsOffsetX < boundingBox.bounds.min.x)
            {
                cameraTrans = new Vector3(boundingBox.bounds.min.x + boundsOffsetX, cameraTrans.y, cameraTrans.z);
            }
            if (cameraTrans.x + boundsOffsetX > boundingBox.bounds.max.x)
            {
                cameraTrans = new Vector3(boundingBox.bounds.max.x - boundsOffsetX, cameraTrans.y, cameraTrans.z);
            }
            if (cameraTrans.y + boundsOffsetY > boundingBox.bounds.max.y)
            {
                cameraTrans = new Vector3(cameraTrans.x, boundingBox.bounds.max.y - boundsOffsetY, cameraTrans.z);
            }
            if (cameraTrans.y - boundsOffsetY < boundingBox.bounds.min.y)
            {
                cameraTrans = new Vector3(cameraTrans.x, boundingBox.bounds.min.y + boundsOffsetY, cameraTrans.z);
            }

            followCam.transform.position = new Vector3(cameraTrans.x, cameraTrans.y, cameraTrans.z);
        }

    }


    public void SetBoundingBox(BoxCollider2D bb)
    {
        boundingBox = bb;
    }
}
