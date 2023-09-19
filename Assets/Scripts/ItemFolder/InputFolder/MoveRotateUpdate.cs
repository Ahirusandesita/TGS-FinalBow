// --------------------------------------------------------- 
// MoveRotateUpdate.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class MoveRotateUpdate : MonoBehaviour
{
    #region variable 
    [SerializeField] Transform mesh = default;

    [SerializeField] float speedToRotateRate = 1f;

    [SerializeField] float maxRotate = 45f;

    [SerializeField] float maxAddRotate = 4.5f;

    [SerializeField] float dicrease = 4.5f;

    Vector3 startRote = default;
    Vector3 cashPosition = default;
    bool isStart = false;
    #endregion
    #region property
    #endregion
    #region method
    private void Awake()
    {
        startRote = mesh.eulerAngles;

    }
    private void OnEnable()
    {
        isStart = true;
        mesh.rotation = Quaternion.Euler(startRote);
        cashPosition = transform.position;
    }
    private void Update()
    {
        if (isStart)
        {
            Process();
        }
    }

    private void OnDisable()
    {
        isStart = false;
    }

    private void Process()
    {
        Vector3 moveDistance = transform.position - cashPosition;

        Vector3 changed;

        if (moveDistance == Vector3.zero)
        {
            changed = Dicrease();
        }
        else
        {
            changed = Increase(moveDistance);

        }

        changed.x = Limit(changed.x, maxRotate, startRote.x);

        changed.z = Limit(changed.z, maxRotate, startRote.z);

        mesh.rotation = Quaternion.Euler(changed);

        cashPosition = transform.position;

    }

    private Vector3 Increase(Vector3 moveDistance)
    {
        Vector3 local = Vector3.zero;

        local.x = Vector3.Dot(moveDistance, transform.right);

        local.z = Vector3.Dot(moveDistance, transform.forward);

        local.x = local.x * speedToRotateRate;

        local.z = local.z * speedToRotateRate;

        local.x = Limit(local.x, maxAddRotate, 0);

        local.z = Limit(local.z, maxAddRotate, 0);

        Vector3 nowRotation = mesh.rotation.eulerAngles + local;

        return nowRotation;

    }

    float Limit(float rote, float max, float root)
    {


        if (rote < root)
        {
            if (rote <= -max + root)
            {

                rote = -max + root;
            }
        }

        if (rote > root)
        {
            if (max + root <= root)
            {
                rote = max + root;
            }
        }

        return rote;
    }


    private Vector3 Dicrease()
    {
        Vector3 rote = mesh.rotation.eulerAngles;

        

        rote.x = DicreaceProcess(rote.x, startRote.x);

        rote.z = DicreaceProcess(rote.z, startRote.z);

        return rote;


        float DicreaceProcess(float rote, float root)
        {
            if (rote < root)
            {
                rote += dicrease;

                if (rote > root)
                {
                    rote = root;
                }
            }
            else if (rote > root)
            {
                rote -= dicrease;

                if (rote < root)
                {
                    rote = root;
                }
            }

            return rote;
        }

    }


    #endregion
}