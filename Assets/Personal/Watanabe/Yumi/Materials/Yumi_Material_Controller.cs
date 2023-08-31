// --------------------------------------------------------- 
// Yumi_Material_Controller.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

interface IFMaterialChanger_Bow
{
    void ChangeMaterialProcess(EnchantmentEnum.EnchantmentState state);
}
/// <summary>
/// エンチャントを取得したときに弓のマテリアルを変更する
/// 上オブジェクト群
/// 上オブジェクトクリスタル群
/// 下オブジェクト群
/// 下オブジェクトクリスタル群
/// メーター１
/// メーター２
/// メーター３
/// メーター４
/// メーター５
/// 弦上（１つ目のマテリアル）
/// 弦下（２つ目のマテリアル）
/// の順で変更してね
/// </summary>
public class Yumi_Material_Controller : MonoBehaviour,IFMaterialChanger_Bow
{
    /// <summary>
    /// 上から
    /// 上オブジェクト群
    /// 上オブジェクトクリスタル群
    /// 下オブジェクト群
    /// 下オブジェクトクリスタル群
    /// メーター１
    /// メーター２
    /// メーター３
    /// メーター４
    /// メーター５
    /// 弦上
    /// 弦下
    /// のマテリアル
    /// </summary>
    #region マテリアル 

    [Tooltip("ノーマル")]
    [SerializeField] List<Material> _normal = new List<Material>();
    [Tooltip("爆発")]
    [SerializeField] List<Material> _bomb = new List<Material>();
    [Tooltip("サンダー")]
    [SerializeField] List<Material> _thunder = new List<Material>();
    [Tooltip("貫通")]
    [SerializeField] List<Material> _penetrate = new List<Material>();
    [Tooltip("ホーミング")]
    [SerializeField] List<Material> _horming = new List<Material>();
    [Tooltip("アナザー")]
    [SerializeField] List<Material> _another = new List<Material>();

    [Tooltip("爆発サンダー")]
    [SerializeField] List<Material> _bomb_thunder = new List<Material>();
    [Tooltip("爆発貫通")]
    [SerializeField] List<Material> _bomb_penetrate = new List<Material>();
    [Tooltip("爆発ホーミング")]
    [SerializeField] List<Material> _bomb_horming = new List<Material>();
    [Tooltip("爆発アナザー")]
    [SerializeField] List<Material> _bomb_another = new List<Material>();
    [Tooltip("サンダー貫通")]
    [SerializeField] List<Material> _thunder_penetrate = new List<Material>();
    [Tooltip("サンダーホーミング")]
    [SerializeField] List<Material> _thunder_horming = new List<Material>();
    [Tooltip("サンダーアナザー")]
    [SerializeField] List<Material> _thunder_another = new List<Material>();
    [Tooltip("貫通ホーミング")]
    [SerializeField] List<Material> _penetrate_horming = new List<Material>();
    [Tooltip("貫通アナザー")]
    [SerializeField] List<Material> _penetrate_another = new List<Material>();
    [Tooltip("ホーミングアナザー")]
    [SerializeField] List<Material> _horming_another = new List<Material>();


    #endregion


    /// <summary>
    /// 上下オブジェクトは１つ目のマテリアルを変更してね
    /// 弦は上下（１，２）のマテリアルをそれぞれ変更してね
    /// </summary>
    #region オブジェクト

    [Tooltip("上オブジェクト")]
    [SerializeField] List<MeshRenderer> _upObj = new List<MeshRenderer>();

    [Tooltip("上オブジェクトクリスタル")]
    [SerializeField] List<MeshRenderer> _upObj_Crystal = new List<MeshRenderer>();

    [Tooltip("下オブジェクト")]
    [SerializeField] List<MeshRenderer> _downObj = new List<MeshRenderer>();

    [Tooltip("下オブジェクトクリスタル")]
    [SerializeField] List<MeshRenderer> _downObj_Crystal = new List<MeshRenderer>();

    [Tooltip("吸い込みメーター")]
    [SerializeField] List<MeshRenderer> _meter = new List<MeshRenderer>();

    [Tooltip("弦")]
    [SerializeField] SkinnedMeshRenderer _gen = default;

    #endregion

    List<List<Material>> _materialsListLists = new List<List<Material>>();

    List<List<MeshRenderer>> _meshRenderersListLists = new List<List<MeshRenderer>>();

    #region method

    private void Start()
    {
        _materialsListLists = new List<List<Material>> { _normal,_bomb,_thunder,_penetrate,_horming,_another,_bomb_thunder,_bomb_penetrate,_bomb_horming,_bomb_another,
        _thunder_penetrate,_thunder_horming,_thunder_another,_penetrate_horming,_penetrate_another,_horming_another};

        // materは一つ一つ違うマテリアルを使用するため除外
        _meshRenderersListLists = new List<List<MeshRenderer>> { _upObj, _upObj_Crystal, _downObj, _downObj_Crystal };
    }

    public void ChangeMaterialProcess(EnchantmentEnum.EnchantmentState state)
    {
        // エンチャントによって使用するlistを設定
        List<Material> setStateMaterials = _materialsListLists[(int)state];

        int index = -1;
        // メッシュグループを抽出(obj系)
        foreach (List<MeshRenderer> list in _meshRenderersListLists)
        {
            // グループでマテリアル変更が終わるとマテリアルlistのインデックスを1増やす
            // 3で打ち止めのはず
            index++;

            // グループから一つメッシュ選ぶ
            foreach(MeshRenderer mesh in list)
            {
               
                //mesh.material = setStateMaterials[index];

            }

        }

        // meter系のマテリアル変更開始
        foreach(MeshRenderer mesh in _meter)
        {
            index++;
            mesh.material = setStateMaterials[index];
        }

        // genのマテリアル変更
        for(int i = 0; i < _gen.materials.Length;i++)
        {
            index++;
            _gen.materials[i] = setStateMaterials[index];
            
        }
    }
    #endregion
}