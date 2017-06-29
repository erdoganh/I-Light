using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public GameObject player;
	public float verticalOffset = 1f;
	public float lookAheadDstX = 4f;
	public float lookSmoothTimeX = 0.5f;
	public float verticalSmoothTime = 0.2f;
	public Vector2 focusAreaSize;

    public float yPosForCamera;
    public float zPosForCamera;

	private FocusArea focusArea;

    private float currentLookAheadX;
    private float targetLookAheadX;
    private float lookAheadDirX;
    private float smoothLookVelocityX;
    private float smoothVelocityY;
    private bool lookAheadStopped;


	void Start() {
		focusArea = new FocusArea (focusAreaSize, player.transform);
	}
	
	void FixedUpdate() {
		focusArea.Update ();
		
		Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;
		
		if (focusArea.velocity.x != 0) {
			lookAheadDirX = Mathf.Sign (focusArea.velocity.x);
			if (Mathf.Sign( Input.GetAxis("Horizontal") ) == Mathf.Sign(focusArea.velocity.x) && Input.GetAxis("Horizontal") != 0) {
				lookAheadStopped = false;
				targetLookAheadX = lookAheadDirX * lookAheadDstX;
			}
			else {
				if (!lookAheadStopped) {
					lookAheadStopped = true;
					targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX)/4f;
				}
			}
		}
		
		
		currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);
		
		focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
		focusPosition += Vector2.right * currentLookAheadX;
		transform.position = (Vector3)focusPosition + Vector3.forward * -10;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosForCamera);
	}

	
	void OnDrawGizmos() {
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawCube (focusArea.center, focusAreaSize);
	}
	
	
	struct FocusArea {
		public Vector2 center;
		public Vector2 velocity;
		float left,right;
		float top,bottom;
        Transform target;

		public FocusArea(Vector2 size, Transform player) {
            target = player;

            left = target.position.x - size.x/2;
			right = target.position.x + size.x/2;
			bottom = target.position.y - size.y/2;
			top = target.position.y + size.y/2;

			velocity = Vector2.zero;
			center = new Vector2((left+right)/2,(top +bottom)/2);
		}
		
		public void Update() {
			float shiftX = 0;
			if (target.position.x < left) {
				shiftX = target.position.x - left;
			} else if (target.position.x > right) {
				shiftX = target.position.x - right;
			}
			left += shiftX;
			right += shiftX;
			
			float shiftY = 0;
			if (target.position.y < bottom) {
				shiftY = target.position.y - bottom;
			} else if (target.position.y > top) {
				shiftY = target.position.y - top;
			}
			top += shiftY;
			bottom += shiftY;
			center = new Vector2((left+right)/2,(top +bottom)/2);
			velocity = new Vector2 (shiftX, shiftY);
		}
	}
	
}
