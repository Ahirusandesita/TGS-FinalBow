using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testinput : MonoBehaviour
{
    [SerializeField]
    EnchantsChenger Chenger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Select);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Select);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Select);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Select);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Notselect);
            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Notselect);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Select);
            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Notselect);

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Decision);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Decision);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Decision);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Decision);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Decision);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.CenterCircle, EnchantsChenger.E_Event.Notselect);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.OuterCircle, EnchantsChenger.E_Event.Cancel);
            Chenger.SetOnlyBackGround(EnchantsChenger.E_BackGround.Emiter, EnchantsChenger.E_Event.Cancel);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Explosion, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Thunder, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Penetration, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Homing, EnchantsChenger.E_Event.Cancel);
            Chenger.SetCircleEnum(EnchantsChenger.E_Enchant.Rapid, EnchantsChenger.E_Event.Cancel);
            Chenger.SetBackGroundEnum(EnchantsChenger.E_Event.Cancel);

        }
    }
}
