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


        // ランダムな角度を生成
        float randomAngle = Random.Range(0, 360);

        // 基準ベクトルを正規化
        Vector3 normalizedBaseVector = baseVector.normalized;

        Vector3 cross = Vector3.up;

        if (normalizedBaseVector == Vector3.up || normalizedBaseVector == Vector3.down)
        {
            cross = Vector3.right;
        }
        // ランダムな角度に基づいて回転行列を生成
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, normalizedBaseVector);

        // 回転行列をベクトルに適用して垂直ベクトルを得る
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