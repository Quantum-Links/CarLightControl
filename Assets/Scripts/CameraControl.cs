using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
	public static CameraControl Instance { private set; get; }
	public float mouseRotSpeed = 8;
	[Header("设置数据"), SerializeField] float minDis = -3;
	[SerializeField] float maxDis = 3, minFOV = 1f, maxFOV = 120f;
	[SerializeField] float rSmoothTime = 0.3f, sSmoothTime = 0.3f, mSmoothTime = 0.3f;
	[SerializeField] float minVerticalAngle = -60, maxVerticalAngle = 60;
	//[Header("初始化数据"), SerializeField] float orginDis = 80;
	[SerializeField] Vector3 targetPosition;
	Vector3 currentPosition, movVelocity;
	float currentDis, disVelocity;
	Vector3 currentRotation, targetRotation, rotVelocity;
	Camera m_Camera;
	private void Awake()
	{
		Instance = this;
		currentPosition = transform.position;
		m_Camera = GetComponent<Camera>();
	}
	private void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0))
		{
			currentDis = Mathf.SmoothDamp(currentDis, minDis, ref disVelocity, rSmoothTime);
			targetRotation.y += Input.GetAxis("Mouse X") * mouseRotSpeed;
			targetRotation.x -= Input.GetAxis("Mouse Y") * mouseRotSpeed;
		}
		else
		{
			currentDis = Mathf.SmoothDamp(currentDis, maxDis, ref disVelocity, rSmoothTime);
		}
		currentDis = Mathf.Clamp(currentDis, minDis, maxDis);
		m_Camera.fieldOfView = CalculateFOV(currentDis);
		targetRotation.x = Mathf.Clamp(targetRotation.x, minVerticalAngle, maxVerticalAngle);
		currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotVelocity, rSmoothTime);
		var tmpRotation = Quaternion.Euler(currentRotation);
		var offset = tmpRotation * Vector3.forward * -currentDis;
		currentPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref movVelocity, mSmoothTime);
		var tmpPosition = currentPosition + offset;
		if (transform.rotation != tmpRotation || transform.position != tmpPosition)
		{
			transform.SetPositionAndRotation(tmpPosition, tmpRotation);
		}
	}
	float CalculateFOV(float distance)
	{
		float fov = Mathf.Lerp(maxFOV, minFOV, Mathf.InverseLerp(minDis, maxDis, distance));
		return Mathf.Clamp(fov, minFOV, maxFOV);
	}

}