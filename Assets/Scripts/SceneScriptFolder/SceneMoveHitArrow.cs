// --------------------------------------------------------- 
// SceneMoveHitArrow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class SceneMoveHitArrow : MonoBehaviour
{
    #region variable 

    public SceneObject _sceneObject;

    public TagObject _SceneControllerTagData;



    private SceneManagement _sceneManagement;

    private bool _isRotation = false;

    private Transform _myTransform = default;

    private float _rotatePower = 800f;

    private const float ROTATE_DIRECTION_LEFT = -1f;
    private const float ROTATE_DIRECTION_RIGHT = 1f;

    private const float SCENEMOVETIME = 2f;

    private const float ROTATIONVECTOL_ZERO = 0f;

    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _sceneManagement = GameObject.FindGameObjectWithTag(_SceneControllerTagData.TagName).GetComponent<SceneManagement>();
        _myTransform = this.transform;
    }

    public void SceneMove(Transform arrowTransform)
    {
        //scene���ړ�����R���[�`�����Ăяo��
        StartCoroutine(SceneMoveCoroutine());

        //�Ŕ���]����
        if (_myTransform.position.x < arrowTransform.position.x)
        {
            _rotatePower *= ROTATE_DIRECTION_LEFT;
        }
        else
        {
            _rotatePower *= ROTATE_DIRECTION_RIGHT;
        }
        _isRotation = true;
    }
    /// <summary>
    /// scene�ړ����邽�߂̃R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneMoveCoroutine()
    {
        yield return new WaitForSeconds(SCENEMOVETIME);
        _sceneManagement.SceneLoadSpecifyMove(_sceneObject);
    }
    private void Update()
    {
        if (_isRotation)
        {
            _myTransform.Rotate(ROTATIONVECTOL_ZERO, ROTATIONVECTOL_ZERO, _rotatePower * Time.deltaTime);
            _rotatePower -= 100f * Time.deltaTime;
        }
    }
    #endregion
}