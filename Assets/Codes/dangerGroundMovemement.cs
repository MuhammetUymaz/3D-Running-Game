using UnityEngine;

public class dangerGroundMovemement : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] float minPosY, maxPosY, currentPosY;
    [SerializeField] Transform myTransform;

    [Header("Movement Amounts")]
    [Tooltip("How many unit the danger ground will down to down when the goal is taken")]
    [SerializeField] float downingAmount;
    [Tooltip("How many speed the danger ground will move to up")]
    [SerializeField] float uppingAmount;
    void Update()
    {
        //Move to up just when the game is started
        if(!gameManagment.managment.isGameStarted)
            return;
        
        currentPosY += uppingAmount * Time.deltaTime * screenResolution.coefficientY;
        
        Mathf.Clamp(currentPosY, minPosY, maxPosY);

        myTransform.position = new Vector3(myTransform.position.x, currentPosY, myTransform.position.z);
    }

    public void DownDangerGround()
    {
        currentPosY -= downingAmount * screenResolution.coefficientY;
        currentPosY = Mathf.Clamp(currentPosY, minPosY, maxPosY);

        myTransform.position = new Vector3(myTransform.position.x, currentPosY, myTransform.position.z);
    }

    public void ZeroingDangerGround()
    {
        currentPosY = minPosY;
        myTransform.position = new Vector3(myTransform.position.x, currentPosY, myTransform.position.z);
    }

}
