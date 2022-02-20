using UnityEngine;
using UnityEngine.UI;

public class gameManagment : MonoBehaviour
{
    //Singleton
    public static gameManagment managment;
    [HideInInspector]
    public bool isGameStarted;

    [Header("For UI")]
    [SerializeField] GameObject startingTextGameObject;
    [SerializeField] GameObject playingGameItems;

    [Header("Goal - Places")]
    [Tooltip("The object which player should take")]
    [SerializeField] Transform goalObject;

    [Tooltip("Places where the goal object can be put on")]
    [SerializeField] Transform[] placePoints;
    Vector3 currentPlace, newPlace;
    [Header("About Player")]
    [SerializeField] Transform playerBody;
    [SerializeField] movementCode playerMovementCode;
    [SerializeField] Vector3 firstPositionOfPlayer;
    [Header("For Danger Ground")]
    [SerializeField] dangerGroundMovemement dangerGroundMovementCode;
    void Awake()
    {
        //Singleton
        if(managment == null)
        {
            managment = this;
        }
    }

    void Start()
    {
        //Get first position - to be able to take the player on same point again
        firstPositionOfPlayer = playerBody.position;

        AssingingNewPlaceForGoal(); //First goal
    } 

    //Finding a place to put the goal on
    public void AssingingNewPlaceForGoal()
    {
        newPlace = placePoints[Random.Range(0, placePoints.Length)].position;
        if(currentPlace != null && currentPlace == newPlace)
        {
            //Call this function until find different place from last one
            AssingingNewPlaceForGoal();
        }
        else //Has been found
        {
            currentPlace = newPlace;
            goalObject.position = currentPlace;

            //Set
            scoreManager.instance.SetScoreAmount(playerBody.position, currentPlace);
        }
    }

    public void DownTheGround() => dangerGroundMovementCode.DownDangerGround();
    public void gameIsEnded() 
    {
        isGameStarted = false;

        scoreManager.instance.ScoreTable();
        
        initializationGame();

        AssingingNewPlaceForGoal();
    }
    
    void initializationGame()
    {
        //Player Position
        playerBody.GetComponent<CharacterController>().enabled = false;
        playerBody.position = firstPositionOfPlayer;
        playerBody.GetComponent<CharacterController>().enabled = true;
        playerBody.rotation = Quaternion.Euler(0, 180, 0);

        //Initializate movement of the player
        playerMovementCode.velocity.y = 0;
        playerMovementCode.rotateRight = false;
        playerMovementCode.rotateLeft = false;
        playerMovementCode.Sprint(false);

        //Walking Sound
        audioManager.instance.walkingAudioSource.Pause();

        //Danger Ground
        dangerGroundMovementCode.ZeroingDangerGround();

        //Score
        scoreManager.instance.ResetScore();

        //Starting Text
        startingTextGameObject.SetActive(true);

        //Hide Controllers and Score
        playingGameItems.SetActive(false); 
    }

    public void gettingGoal()
    {
        audioManager.instance.GettingGoalSoundEffectPlay();

        scoreManager.instance.UpdateScore();
        scoreManager.instance.RefreshingHighScore();
        
        AssingingNewPlaceForGoal();

        DownTheGround();
    }
}
