using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance { private set; get; }
    [Header("设置数据"), SerializeField] float minDis = -3;
    [SerializeField] float maxDis = 3;
    [SerializeField] float rSmoothTime = 0.3f, sSmoothTime = 0.3f, mSmoothTime = 0.3f;
    public float mouseRotSpeed = 8, mouseMoveSpeed = 1, mouseZoomSpeed = 30;
    [SerializeField] float minVerticalAngle = -60, maxVerticalAngle = 60;
    [Header("初始化数据"), SerializeField] float orginDis = 80;
    Vector3 currentPosition, movVelocity, targetPosition;
    float targetDis, currentDis, disVelocity;
    Vector3 currentRotation, targetRotation, rotVelocity;
    [SerializeField] Transform target;
    private void Awake()
    {
        Instance = this;
        SetCameraTrans();
    }
    [ContextMenu("设置相机位置")]
    private void SetCameraTrans()
    {
        currentDis = targetDis = orginDis;
        currentPosition = targetPosition = transform.position;
    }
    public void SetTarget(Transform trans,float dis=2)
    {
        target = trans;
        targetPosition = trans.position;
        targetDis = dis;
    }
    public void SetOrginDis(Vector3 orginPos,Vector3 orginRO)
    {
        target = null;
        targetPosition = orginPos;
        targetRotation = orginRO;
        targetDis = 0;
    }
	public void SetTargetRo(Vector3 targetRO)
	{
		targetRotation = targetRO;
	}
	private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetRotation.y += Input.GetAxis("Mouse X") * mouseRotSpeed;
            targetRotation.x -= Input.GetAxis("Mouse Y") * mouseRotSpeed;
        }
        //if (Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //{
        //    targetRotation.y += Input.touches[0].deltaPosition.x*mouseRotSpeed;
        //    targetRotation.x -= Input.touches[0].deltaPosition.y*mouseRotSpeed;
        //}
        targetDis = targetDis - Input.GetAxis("Mouse ScrollWheel") * mouseZoomSpeed;

        targetRotation.x = Mathf.Clamp(targetRotation.x, minVerticalAngle, maxVerticalAngle);
        targetDis = Mathf.Clamp(targetDis, minDis, maxDis);

        currentDis = Mathf.SmoothDamp(currentDis, targetDis, ref disVelocity, rSmoothTime);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotVelocity, rSmoothTime);

        var tmpRotation = Quaternion.Euler(currentRotation);
        var offset = tmpRotation * Vector3.forward * -currentDis;
        if (target != null)
            targetPosition = target.position;
        currentPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref movVelocity, mSmoothTime);
        var tmpPosition = currentPosition + offset;
        if (transform.rotation != tmpRotation || transform.position != tmpPosition)
        {
            transform.SetPositionAndRotation(tmpPosition, tmpRotation);
        }
    }
    private void riseSpeed()
    {
        targetRotation.x = Mathf.Clamp(targetRotation.x, minVerticalAngle, maxVerticalAngle);
        //targetDis = Mathf.Clamp(targetDis, minDis, maxDis);
    }
}