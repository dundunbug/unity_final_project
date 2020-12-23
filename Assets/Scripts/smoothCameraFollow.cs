using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothCameraFollow : MonoBehaviour
{
    //offset from the viewport center to fix damping
    public float m_DampTime = 0.15f;
    public Transform m_Target;
    public float m_XOffset = 0;
    public float m_YOffset = 0;
	public float shakeTime = 1f;
	public float shakeMagnitude = 0.5f;
	float margin = 0.1f; 
	float shakeDuration = 0f;
 	
 	float dampingSpeed = 1.0f;
	public bool enableShake = false;
	bool canShake = false;
 	Vector3 initialPosition;
	void Start () {
		if (m_Target==null){
			m_Target = GameObject.Find("player").transform;
		}
		m_XOffset = transform.position.x - m_Target.position.x;
		m_YOffset = transform.position.y - m_Target.position.y;
	}

    void Update() {
        if(m_Target) {
			float targetX = m_Target.position.x + m_XOffset;
			float targetY = m_Target.position.y + m_YOffset;

			if (Mathf.Abs(transform.position.x - targetX) > margin)
				targetX = Mathf.Lerp(transform.position.x, targetX, m_DampTime * Time.deltaTime);

			if (Mathf.Abs(transform.position.y - targetY) > margin)
				targetY = Mathf.Lerp(transform.position.y, targetY, m_DampTime * Time.deltaTime);
            
			transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
		if (enableShake){
			initialPosition = transform.localPosition;
			shakeDuration = shakeTime;
			enableShake = false;
			canShake = true;
		}
		if (canShake &&shakeDuration > 0){
			transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
			shakeDuration -= Time.deltaTime * dampingSpeed;
		}else if (canShake){
			shakeDuration = 0f;
			transform.localPosition = initialPosition;
			enableShake = false;
			canShake = false;
		}
    }

}
