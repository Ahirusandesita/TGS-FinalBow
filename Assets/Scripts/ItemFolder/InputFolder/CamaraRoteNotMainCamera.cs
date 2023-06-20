// --------------------------------------------------------- 
// CamaraRoteNotMainCamera.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
/// <summary>
/// rotate0,0,0ópÉJÉÅÉâ
/// </summary>
public class CamaraRoteNotMainCamera : MonoBehaviour
{

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	public void CameraRote()
	{
		
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	
	}
}