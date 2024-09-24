using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class temptlinedraw : MonoBehaviour
{

    public RectTransform p0;
    public RectTransform p1;
    public RectTransform p2;
    public RectTransform p3;

    public int pointCount;
    [SerializeField] private int worldNum; 

    public RectTransform[] rectFoot = new RectTransform[15];
   
    private Vector3 GetRotation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 q0 = Vector3.Lerp(p0, p1, t);
        Vector3 q1 = Vector3.Lerp(p1, p2, t);
        Vector3 q2 = Vector3.Lerp(p2, p3, t);

        Vector3 r0 = Vector3.Lerp(q0, q1, t);
        Vector3 r1 = Vector3.Lerp(q1, q2, t);

        return Vector3.Lerp(r0, r1, t);
    }

   public void FootTween()
    {
        for (int i = 0; i <20; i++)
        {
            rectFoot[i].DOScale(new Vector3(0.15f,0.15f,0.15f),0.4f).SetEase(Ease.OutElastic).SetDelay((float)i * 0.1f);
        }
    }

    public Vector3 GetBezierPosition(int start , int end, int level , int scount ,int ecount, float t)
    {
        float pos_x;
        float pos_y;

        float Comp_x;
        float Comp_y;


        Vector3 StartPoint;
        Vector3 EndPoint;
        Vector3 s_relaypoint;
        Vector3 e_relaypoint;
     
        pos_x = (375 - (scount- 1) * 125) - 445 + 250 * (start - 1);
        pos_y = -550 + (level) * 432;

        StartPoint = new Vector3(pos_x, pos_y+70, 0);

        pos_x = (375 - (ecount - 1) * 125) - 445 + 250 * (end - 1);
        pos_y = -550 + (level+1) * 432;

        EndPoint = new Vector3(pos_x, pos_y - 98, 0);
        
        Comp_x = (EndPoint.x - StartPoint.x) * 0.33f;
        Comp_y = (EndPoint.y - StartPoint.y) * 0.33f;

        s_relaypoint = new Vector3(StartPoint.x + Comp_x, StartPoint.y + Comp_y, 0);
        e_relaypoint = new Vector3(EndPoint.x - Comp_x, EndPoint.y - Comp_y, 0);
       
        Vector3 q0 = Vector3.Lerp(StartPoint, s_relaypoint, t);
        Vector3 q1 = Vector3.Lerp(s_relaypoint, e_relaypoint, t);
        Vector3 q2 = Vector3.Lerp(e_relaypoint, EndPoint, t);

        Vector3 r0 = Vector3.Lerp(q0, q1, t);
        Vector3 r1 = Vector3.Lerp(q1, q2, t);

        
        return Vector3.Lerp(r0, r1, t);
    }

    public void Reset()
    {        
        for (int i = 14; i >=0; i--)
        {
            rectFoot[i].anchoredPosition = new Vector2(0, -768f);           
        }        
    }

    public void SetLine(int start, int end, int level,int scount,int ecount,int time)
    {
      
        for (int i = 0; i < this.pointCount - 1; i++)
        {            
            rectFoot[i].anchoredPosition = GetBezierPosition(start, end, level,scount,ecount, (float)i / (pointCount - 1));
        }
        for (int i = 0; i < 15; i++)
        {
            rectFoot[i].DOScale(new Vector3(0.15f, 0.15f, 0.15f), 0.2f).SetEase(Ease.OutElastic).SetDelay(i * 0.015f+ (time) * 0.15f + level *0.6f).From(new Vector3(0.01f, 0.01f, 0.01f),true);            
        }
    }

    public void MapSetLine(int level)
    {
        if (level < worldNum) return;

        for (int i = 0; i < this.pointCount - 1; i++)
        {
            rectFoot[i].anchoredPosition = GetRotation(p0.anchoredPosition, p1.anchoredPosition, p2.anchoredPosition, p3.anchoredPosition, (float)i / (pointCount - 1));
        }
        for (int i = 0; i < 15; i++)
        {
            rectFoot[i].DOScale(new Vector3(0.15f, 0.15f, 0.15f), 0.2f).SetEase(Ease.OutElastic).SetDelay(i * 0.015f + worldNum * 1f).From(new Vector3(0.01f, 0.01f, 0.01f), true);
        }
    }

}
