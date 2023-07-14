// --------------------------------------------------------- 
// Yumi_Material_Controller.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections.Generic;

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
public class Yumi_Material_Controller : MonoBehaviour
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

    #region method

    #endregion
}