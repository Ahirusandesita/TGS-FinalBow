// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speedX = 5f;
    public float speedY = 5f;
    Vector3 beforePos;
    private void Start()
    {
        beforePos = Input.mousePosition;
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera(Input.mousePosition);
        beforePos = Input.mousePosition;
    }

    void MoveCamera(Vector3 currentPos)
    {
        const float FIX_SPEED = 0.01f;
        Vector3 move = currentPos - beforePos;

        Vector3 cameraRote = Camera.main.transform.localRotation.eulerAngles;

        cameraRote.x = cameraRote.x + (-move.y * speedX * FIX_SPEED);
        cameraRote.y = cameraRote.y + (-move.x * speedY * FIX_SPEED);

        Camera.main.transform.localRotation = Quaternion.Euler(cameraRote);
    }
}
