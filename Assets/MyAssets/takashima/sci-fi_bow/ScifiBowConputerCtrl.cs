using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ScifiBowConputerCtrl : MonoBehaviour
{
    public enum ENCHANT_TYPE
    {
        EXPLOSION,
        RIOT,
        PENETRATION,
        HOMING,
        MULTIPLE
    }
    private ENCHANT_TYPE enchantType;

    public Texture tex_encExplosion;
    public Texture tex_encRiot;
    public Texture tex_encPenetration;
    public Texture tex_encHoming;
    public Texture tex_encMultiple;

    private Renderer enchantTypeImageRender;

    private TextMeshPro tmp_EnchantLevelText;
    private TextMeshPro tmp_HpText;

    private Material mat_Graph;
    private Material mat_Heart;

    private Vector2 graphOffset;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        tmp_EnchantLevelText = GameObject.Find("TMP_EnchantLevel").GetComponent<TextMeshPro>();
        tmp_HpText = GameObject.Find("TMP_HP").GetComponent<TextMeshPro>();

        mat_Graph = GameObject.Find("IMG_Graph").GetComponent<MeshRenderer>().material;
        mat_Heart = GameObject.Find("IMG_Heart").GetComponent<MeshRenderer>().material;
        color = mat_Heart.GetColor("_TintColor");

        enchantTypeImageRender = GameObject.Find("IMG_EnchantType").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mat_Graph.mainTextureOffset = new Vector2(graphOffset.x + Time.time * 0.1f, graphOffset.y);

        float val = Mathf.Cos((float)(2 * Mathf.PI * Time.time / .5f)) * 0.5f + 0.5f;
        color = new Color(color.r, color.g, color.b, val);
        mat_Heart.SetColor("_TintColor", color);

        if (Input.GetKeyDown(KeyCode.K))
            HpUpdate(Random.Range(0, 100));

        if (Input.GetKeyDown(KeyCode.L))
            EnchantValueUpdate(Random.Range(0, 9));

        if (Input.GetKeyDown(KeyCode.J))
            EnchantTypeUpdate((ENCHANT_TYPE)Random.Range(0, 4));

    }

    /// <summary>
    /// ＨＰ表示更新
    /// </summary>
    /// <param name="hpValue"></param>
    public void HpUpdate(int hpValue)
    {
        tmp_HpText.text = Mathf.Clamp(hpValue, 0, 100).ToString("000");
    }

    /// <summary>
    /// エンチャントレベル更新
    /// </summary>
    /// <param name="EnchantValue"></param>
    public void EnchantValueUpdate(int EnchantValue)
    {
        tmp_EnchantLevelText.text = Mathf.Clamp(EnchantValue, 0, 9).ToString("F0");
    }

    /// <summary>
    /// エンチャントタイプ表示更新
    /// </summary>
    /// <param name="hpValue"></param>
    public void EnchantTypeUpdate(ENCHANT_TYPE EnchantType)
    {
        switch (EnchantType)
        {
            case ENCHANT_TYPE.EXPLOSION:
                enchantTypeImageRender.material.mainTexture = tex_encExplosion;
                break;
            case ENCHANT_TYPE.RIOT:
                enchantTypeImageRender.material.mainTexture = tex_encRiot;
                break;
            case ENCHANT_TYPE.PENETRATION:
                enchantTypeImageRender.material.mainTexture = tex_encPenetration;
                break;
            case ENCHANT_TYPE.HOMING:
                enchantTypeImageRender.material.mainTexture = tex_encHoming;
                break;
            case ENCHANT_TYPE.MULTIPLE:
                enchantTypeImageRender.material.mainTexture = tex_encMultiple;
                break;
            default:
                break;
        }
    }
}
