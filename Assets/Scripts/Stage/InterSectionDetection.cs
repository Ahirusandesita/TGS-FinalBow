// --------------------------------------------------------- 
// InterSectionDetection.cs 
// 
// CreateDay: 2023/07/10
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class InterSectionDetection
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method

    public bool CheckIntersection(Vector3[] verticesMe,Vector3[] verticesPartner)
    {
        for(int i = 0; i < verticesMe.Length; i++)
        {
            Vector3 edgeMeStart = verticesMe[i];
            Vector3 edgeMeEnd = verticesMe[(i + 1) % 4];

            for(int j = 0; j < verticesPartner.Length; j++)
            {
                Vector3 edgePartnerStart = verticesPartner[j];
                Vector3 edgePartnerEnd = verticesPartner[(j + 1) % 4];

                if (DoIntersect(edgeMeStart, edgeMeEnd, edgePartnerStart, edgePartnerEnd))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool DoIntersect(Vector3 p1,Vector3 q1,Vector3 p2,Vector3 q2)
    {
        if(Orientation(p1, q1, p2) == 0 || Orientation(p1, q1, q2) == 0)
        {
            return false;
        }

        if (Orientation(p2, q2, p1) == 0 || Orientation(p2, q2, q1) == 0)
        {
            return false;
        }

        return (Orientation(p1, q1, p2) != Orientation(p1, q1, q2))
            && (Orientation(p2, q2, p1) != Orientation(p2, q2, q1));
    }

    private int Orientation(Vector3  p,Vector3 q,Vector3 r)
    {
        float val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

        if (Mathf.Approximately(val, 0f))
        {
            return 0;
        }
        else if(val > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    #endregion
}