// --------------------------------------------------------- 
// PrintTest.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class PrintTest : MonoBehaviour
{
    Vector3 look;
    [SerializeField] Vector3 baseVec;

    private void Start()
    {
        StartCoroutine(A());
    }
    private Vector3 GenerateRandomVerticalVector(Vector3 baseVector)
    {


        // �����_���Ȋp�x�𐶐�
        float randomAngle = Random.Range(0, 360);

        // ��x�N�g���𐳋K��
        Vector3 normalizedBaseVector = baseVector.normalized;

        Vector3 cross = Vector3.up;

        if (normalizedBaseVector == Vector3.up || normalizedBaseVector == Vector3.down)
        {
            cross = Vector3.right;
        }
        // �����_���Ȋp�x�Ɋ�Â��ĉ�]�s��𐶐�
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, normalizedBaseVector);

        // ��]�s����x�N�g���ɓK�p���Đ����x�N�g���𓾂�
        Vector3 randomVerticalVector = rotation * Vector3.Cross(normalizedBaseVector, cross);

        return randomVerticalVector;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + baseVec * 10);
        Gizmos.DrawLine(transform.position, transform.position + look * 10);

    }

    private IEnumerator A()
    {
        while (true)
        {
            look = GenerateRandomVerticalVector(baseVec);
            yield return new WaitForSeconds(1);
        }
    }
}