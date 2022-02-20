using UnityEngine;

public class collidingCode : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("goalTag"))
            gameManagment.managment.gettingGoal();

        else if(other.CompareTag("dangerGroundTag"))
            gameManagment.managment.gameIsEnded();
    }
}
