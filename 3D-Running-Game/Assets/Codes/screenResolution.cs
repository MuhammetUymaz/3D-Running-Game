using UnityEngine;

public class screenResolution : MonoBehaviour
{
    [Header("For Screen Resolution Settings")]
    //Write the reference Aspect Ratio on X and Y (such as X: 16, Y: 9)
    [SerializeField] float ratioX;
    [SerializeField] float ratioY;
    float ratio;
    float rX;
    float rY;
    float initialScaleX, initialScaleY, initialScaleZ;
    float currentRatioToRatio;
    float ratioCurrent;
    public static float coefficientX, coefficientY;

    [Header("For Terrain")]
    [SerializeField] Transform dangerGroundTransform;
    [SerializeField] Transform terrainTransform;
    void Awake()
    {
        //Screen.SetResolution(720, 1280, false);

        //Get the initial scale
        initialScaleX = transform.localScale.x;
        initialScaleY = transform.localScale.y;
        initialScaleZ = transform.localScale.z;

        SetCameraScale();
    }

    void SetCameraScale()
    {
        //Current scale
        Vector3 scale = transform.localScale; 

        //Ratio which is rationed between current ratio and the determined ratio
        ratio = ratioX / ratioY; //Determined ratio
        rX = Screen.width;
        rY = Screen.height;
        ratioCurrent = rX / rY;  //Current ratio
        currentRatioToRatio = ratioCurrent / ratio; //The rationed which we will use

        //Set the coefficients
        coefficientX = currentRatioToRatio;
        coefficientY = 1;

        //Set the scale objects by ratio's situation
        transform.localScale = new Vector3(initialScaleX * currentRatioToRatio, initialScaleY, initialScaleZ);
        /*
        if(ratioCurrent > ratio)
           transform.localScale = new Vector3(initialScaleX * currentRatioToRatio, initialScaleY, initialScaleZ);
        else if (ratioCurrent < ratio)
            transform.localScale = new Vector3(initialScaleX * currentRatioToRatio, initialScaleY, initialScaleZ);
        */

        //Detach all objects 
        transform.DetachChildren();

        //Terrain should follow danger ground
        terrainTransform.parent = dangerGroundTransform;
    }

}
