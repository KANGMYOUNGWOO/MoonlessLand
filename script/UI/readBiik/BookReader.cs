using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookReader : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] RectTransform BookPanel;
   

    public bool enableShadowEffect = true;

    public Vector3 EndBottomLeft {
        get { return ebl; }
    }
    public Vector3 EndBottomRight {
        get { return ebr; }
    }
    public float Height {
        get {
            return BookPanel.rect.height;
        }
    }

    public Image ClippingPlane;
   

    public Image Shadow;
    public Image ShadowLTR;
 

    public Image Left;
    public Image Right;
  

    public Image RightNext;
    public Image LeftNext;
 

    float radius1, radius2;

    
    Vector3 sb;
    Vector3 st;

    Vector3 c;
    Vector3 c2;
    Vector3 c3;
    Vector3 c4;

    Vector3 ebr;
    Vector3 ebl;

    Vector3 f;

    public int index = 0;


    public float PageFlipTime = 1;
    public float TimeBetweenPages = 1;
    public float DelayBeforeStarting = 0;
    public bool AutoStartFlip = true;
    public int AnimationFramesCount = 40;


    public book b {get; set;}
    public bool bisLast = false;

    private void Start()
    {
        
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        gameObject.transform.SetParent(canvas.transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(-77, 187, 1);

       

        CalcCurlCriticalPoints();

        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;



        ClippingPlane.rectTransform.sizeDelta = new Vector2(pageWidth * 2 + pageHeight, pageHeight + pageHeight * 2);

       
        float hyp = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
        float shadowPageHeight = pageWidth / 2 + hyp;

        Shadow.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        Shadow.rectTransform.pivot = new Vector2(1, (pageWidth / 2) / shadowPageHeight);

        ShadowLTR.rectTransform.sizeDelta = new Vector2(pageWidth, shadowPageHeight);
        ShadowLTR.rectTransform.pivot = new Vector2(0, (pageWidth / 2) / shadowPageHeight);
    }

    private void CalcCurlCriticalPoints()
    {
        sb = new Vector3(0, -BookPanel.rect.height / 2);
        ebr = new Vector3(BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        ebl = new Vector3(-BookPanel.rect.width / 2, -BookPanel.rect.height / 2);
        st = new Vector3(0, BookPanel.rect.height / 2);
        radius1 = Vector2.Distance(sb, ebr);
        float pageWidth = BookPanel.rect.width / 2.0f;
        float pageHeight = BookPanel.rect.height;
        radius2 = Mathf.Sqrt(pageWidth * pageWidth + pageHeight * pageHeight);
    }
    private float CalcClipAngle(Vector3 c, Vector3 bookCorner, out Vector3 t1)
    {
        Vector3 t0 = (c + bookCorner) / 2;
        float T0_CORNER_dy = bookCorner.y - t0.y;
        float T0_CORNER_dx = bookCorner.x - t0.x;
        float T0_CORNER_Angle = Mathf.Atan2(T0_CORNER_dy, T0_CORNER_dx);
        float T0_T1_Angle = 90 - T0_CORNER_Angle;

        float T1_X = t0.x - T0_CORNER_dy * Mathf.Tan(T0_CORNER_Angle);
        T1_X = normalizeT1X(T1_X, bookCorner, sb);
        t1 = new Vector3(T1_X, sb.y, 0);

       
        float T0_T1_dy = t1.y - t0.y;
        float T0_T1_dx = t1.x - t0.x;
        T0_T1_Angle = Mathf.Atan2(T0_T1_dy, T0_T1_dx) * Mathf.Rad2Deg;
        return T0_T1_Angle;
    }
    private float normalizeT1X(float t1, Vector3 corner, Vector3 sb)
    {
        if (t1 > sb.x && sb.x > corner.x)
            return sb.x;
        if (t1 < sb.x && sb.x < corner.x)
            return sb.x;
        return t1;
    }
    private Vector3 Calc_C_Position(Vector3 followLocation)
    {
        Vector3 c;
        f = followLocation;
        float F_SB_dy = f.y - sb.y;
        float F_SB_dx = f.x - sb.x;
        float F_SB_Angle = Mathf.Atan2(F_SB_dy, F_SB_dx);
        Vector3 r1 = new Vector3(radius1 * Mathf.Cos(F_SB_Angle), radius1 * Mathf.Sin(F_SB_Angle), 0) + sb;

        float F_SB_distance = Vector2.Distance(f, sb);
        if (F_SB_distance < radius1)
            c = f;
        else
            c = r1;
        float F_ST_dy = c.y - st.y;
        float F_ST_dx = c.x - st.x;
        float F_ST_Angle = Mathf.Atan2(F_ST_dy, F_ST_dx);
        Vector3 r2 = new Vector3(radius2 * Mathf.Cos(F_ST_Angle),
           radius2 * Mathf.Sin(F_ST_Angle), 0) + st;
        float C_ST_distance = Vector2.Distance(c, st);
        if (C_ST_distance > radius2)
            c = r2;
        return c;
    }
    public void FlipRightPage()
    {

        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (EndBottomRight.x + EndBottomLeft.x) / 2;
        float xl = ((EndBottomRight.x - EndBottomLeft.x) / 2) * 0.9f;
        
        float h = Mathf.Abs(EndBottomRight.y) * 0.9f;
        float dx = (xl) * 2 / AnimationFramesCount;
        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
    }

    
    public void UpdateBookRTLToPoint(Vector3 followLocation)
    {

        f = followLocation;
        Shadow.transform.SetParent(ClippingPlane.transform, true);
        Shadow.transform.localPosition = Vector3.zero;
        Shadow.transform.localEulerAngles = Vector3.zero;
        Right.transform.SetParent(ClippingPlane.transform, true);

        Left.transform.SetParent(BookPanel.transform, true);
        Left.transform.localEulerAngles = Vector3.zero;
        RightNext.transform.SetParent(BookPanel.transform, true);
        c = Calc_C_Position(followLocation);
        Vector3 t1;
        float clipAngle = CalcClipAngle(c, ebr, out t1);
        if (clipAngle > -90) clipAngle += 180;

        ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);
        ClippingPlane.transform.localEulerAngles = new Vector3(0, 0, clipAngle + 90);
        ClippingPlane.transform.position = BookPanel.TransformPoint(t1);

        
        Right.transform.position = BookPanel.TransformPoint(c);
        float C_T1_dy = t1.y - c.y;
        float C_T1_dx = t1.x - c.x;
        float C_T1_Angle = Mathf.Atan2(C_T1_dy, C_T1_dx) * Mathf.Rad2Deg;
        Right.transform.localEulerAngles = new Vector3(0, 0, C_T1_Angle - (clipAngle + 90));

       
        Left.transform.SetParent(ClippingPlane.transform, true);
        

        Shadow.rectTransform.SetParent(Right.rectTransform, true);
    }
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

         DragRightPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);           
            UpdateBookRTLToPoint(new Vector3(x, y, 0));

            yield return new WaitForSeconds(frameTime);
            x -= dx;
        }
        ReleasePage();
    }
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx , int index)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        DragRightPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
                                         
                    UpdateBookRTLToPoint(new Vector3(x, y, 0));
            
            yield return new WaitForSeconds(frameTime);
            x -= dx;
        }
        ReleasePage();
    }




    public void DragRightPageToPoint(Vector3 point)
    {

        f = point;


        
        ClippingPlane.rectTransform.pivot = new Vector2(1, 0.35f);

        Left.gameObject.SetActive(true);
        Left.rectTransform.pivot = new Vector2(0, 0);
        Left.transform.position = RightNext.transform.position;
        Left.transform.eulerAngles = new Vector3(0, 0, 0);
        
        Left.transform.SetAsFirstSibling();

        Right.gameObject.SetActive(true);
        Right.transform.position = RightNext.transform.position;
        Right.transform.eulerAngles = new Vector3(0, 0, 0);
        

        LeftNext.transform.SetAsFirstSibling();
        if (enableShadowEffect) Shadow.gameObject.SetActive(true);
        UpdateBookRTLToPoint(f);
    }
    IEnumerator FlipToEnd()
    {

        WaitForSeconds delay = new WaitForSeconds(DelayBeforeStarting);
        WaitForSeconds TBP   = new WaitForSeconds(TimeBetweenPages);

        yield return delay;
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (EndBottomRight.x + EndBottomLeft.x) / 2;
        float xl = ((EndBottomRight.x - EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(EndBottomRight.y) * 0.9f;
        //y=-(h/(xl)^2)*(x-xc)^2          
        //               y         
        //               |          
        //               |          
        //               |          
        //_______________|_________________x         
        //              o|o             |
        //           o   |   o          |
        //         o     |     o        | h
        //        o      |      o       |
        //       o------xc-------o      -
        //               |<--xl-->
        //               |
        //               |
        float dx = (xl) * 2 / AnimationFramesCount;

        int i = 0;
                while (i<1)
                {
                    StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx,0));
                   
                    yield return TBP;
                    i += 1;
                }

      if(bisLast) b.ActivePage();
             
    }
    public void ReleasePage()
    {
      
            float distanceToLeft = Vector2.Distance(c, ebl);
            float distanceToRight = Vector2.Distance(c, ebr);
            if (distanceToRight < distanceToLeft)
                TweenBack();
            else
                TweenForward();
       
    }


    public void ReleasePage(int index)
    {
        float distanceToLeft = 0;
        float distanceToRight = 0;
        switch (index)
        {
            case 0:
                distanceToLeft = Vector2.Distance(c, ebl);
                distanceToRight = Vector2.Distance(c, ebr);
                break;
            case 1:
                distanceToLeft = Vector2.Distance(c2, ebl);
                distanceToRight = Vector2.Distance(c2, ebr);
                break;
            case 2:
                distanceToLeft = Vector2.Distance(c3, ebl);
                distanceToRight = Vector2.Distance(c3, ebr);
                break;
            case 3:
                distanceToLeft = Vector2.Distance(c4, ebl);
                distanceToRight = Vector2.Distance(c4, ebr);
                break;

        }

       
        if (distanceToRight < distanceToLeft)
            TweenBack();
        else
            TweenForward();
    }


    Coroutine currentCoroutine;
    public void TweenForward()
    {
     
            currentCoroutine = StartCoroutine(TweenTo(ebl, 0.15f, () => { Flip(); }));
       
    }
   
    void Flip()
    {

        LeftNext.transform.SetParent(BookPanel.transform, true);
        Left.transform.SetParent(BookPanel.transform, true);
        LeftNext.transform.SetParent(BookPanel.transform, true);
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Right.transform.SetParent(BookPanel.transform, true);
        RightNext.transform.SetParent(BookPanel.transform, true);
        
       Shadow.gameObject.SetActive(false);
       ShadowLTR.gameObject.SetActive(false);
    }
    public void TweenBack()
    {
        
            currentCoroutine = StartCoroutine(TweenTo(ebr, 0.15f,
                () =>
                {
                  
                    Right.transform.SetParent(BookPanel.transform);

                   
                    Right.gameObject.SetActive(false);
                   
                }
                ));
        
    }
   
    public IEnumerator TweenTo(Vector3 to, float duration, System.Action onFinish)
    {
        WaitForSeconds wait = new WaitForSeconds(0.025f);
        int steps = (int)(duration / 0.025f);
        Vector3 displacement = (to - f) / steps;
        for (int i = 0; i < steps - 1; i++)
        {
           
                UpdateBookRTLToPoint(f + displacement);
          

            yield return wait;
        }
        if (onFinish != null)
            onFinish();
    }
    public void StartFlipping()
    {
        
        StartCoroutine(FlipToEnd());
    }
}
   
