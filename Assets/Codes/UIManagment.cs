using UnityEngine;
using UnityEngine.UI;
public class UIManagment : MonoBehaviour
{
    [Header("About Player")]
    [SerializeField] movementCode theMovementCode;

    [Header("Text UI Element")]
    [SerializeField] GameObject controllerParent;
    [SerializeField] GameObject gameStartingText;

    void Start() => scoreManager.instance.WriteHighScoreInitially();

    public void StartingGame()
    {
        gameManagment.managment.isGameStarted = true;

        gameStartingText.SetActive(false);

        //Score text boards
        scoreManager.instance.HideLastHighScoreTables();

        //Player Walking-Sprinting Sound
        audioManager.instance.walkingAudioSource.Play();
        audioManager.instance.ChangeWalkingAudioSourcePitch(true);

        //Controllers
        ShowTheControllers();
    }

    public void ShowTheControllers() => controllerParent.SetActive(true);

    public void RotatePlayer(int direction)
    {
        switch(direction)
        {
            case 1: //Right
                theMovementCode.rotateRight = true;
            break;
            
            case 2: //Left
                theMovementCode.rotateLeft = true;
            break;
        }
    }

    public void StopRotatingPlayer(int direction)
    {
        switch(direction)
        {
            case 1: //Right
                theMovementCode.rotateRight = false;
            break;
            
            case 2: //Left
                theMovementCode.rotateLeft = false;
            break;
        }
    }
    public void SpeedingUpDown(int controlling)
    {
        if(controlling == 1) //For 1
        {
            theMovementCode.Sprint(true);
        }
        else //For 0
        {
            theMovementCode.Sprint(false);
        }
    }
    public void Jumping() => theMovementCode.Jumping();
}
