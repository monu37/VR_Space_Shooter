using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public enum status
    {
        Traning,
        Waiting,
        Playing,
        win,
        GameOver
    }
    public status GameStatus;


    [SerializeField] GameObject BulletPrefab;

    [Space]
    [Header("Timer")]
    [SerializeField] Image TimerFillImg;
    [SerializeField] float TimerCount;
    float defaultfillamount = 1;


    [Header("Score")]
    [SerializeField] TextMeshProUGUI ScoreText;
    int ScoreCount;
    
    [Header("HighScore")]
    [SerializeField] TextMeshProUGUI HighScoreText;
    int HighScoreCount;

    [Header("Level")]
    [SerializeField] TextMeshProUGUI CurrentLevelText;
    int CurrentLevel;

    [Header("Target")]
    [SerializeField] TextMeshProUGUI TargetKillText;
    int TargetCount;

    [Header("Totla Kill")]
    [SerializeField] TextMeshProUGUI TotalKillText;
    int TotalKillCount;

    [Header("Win Panel")]
    [SerializeField] TextMeshProUGUI NextLevelText;
    int NextLevel;

    [Header("UI Particle Effect")]
    [SerializeField] GameObject UiHitParticleObj;

    [Header("Traning")]
    [SerializeField] GameObject TraningObj;
    [SerializeField] int TotalTraningTarget;
    [SerializeField] GameObject TraningPopup;

    [Header("Reference")]
    [SerializeField] GameObject FirstPanel;
    [SerializeField] GameObject GamePlayPanel;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject PickGunLeftPanel;
    [SerializeField] GameObject PickGunRightPanel;
    [SerializeField] GameObject ScorePopup;


    private void Awake()
    {
        instance = this;

       
    }

    private void Start()
    {
        //game status
        // activate traning for the first time user
        if (Helper.GetFirstTime() == 0)
        {

            GameStatus = status.Traning;
            TraningObj.SetActive(true);
            TraningPopup.SetActive(true);

            FirstPanel.SetActive(false);
            StartPanel.SetActive(false);
        }
        else
        {
            TraningObj.SetActive(false);
            TraningPopup.SetActive(false);

            FirstPanel.SetActive(true);
            StartPanel.SetActive(true);
            GameStatus = status.Waiting;
        }
       
        GamePlayPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        WinPanel.SetActive(false);
        //level
        CurrentLevel = Helper.GetLevel();
        if (CurrentLevel == 0)
        {
            CurrentLevel = 1;
            Helper.setlevel(CurrentLevel);
        }
        CurrentLevelText.text = CurrentLevel.ToString();

        //timer
        TimerCount = defaultfillamount;
        TimerFillImg.fillAmount = TimerCount;

        //score
        ScoreCount = 0;
        ScoreText.text = ScoreCount.ToString();

        //highscore
        HighScoreCount = Helper.GetHighScore();
        HighScoreText.text = HighScoreCount.ToString();

        
        

        //target assign
        TargetCount = 0;
        targetassign();

        //totalkill
        TotalKillCount = 0;
        TotalKillText.text = TotalKillCount.ToString();
    }

    void targetassign()
    {
        TargetCount = CurrentLevel + 4;
        TargetKillText.text = TargetCount.ToString();
    }
   
    private void Update()
    {
        // if game is started == timer started
        if (GameStatus == status.Playing)
        {
            timer();
        }
    }

    void timer()
    {

        if(CurrentLevel <=5)
        {
            TimerFillImg.fillAmount -= (Time.deltaTime / 2) * (Random.Range(0.01f, .08f));
        }
        else
        {
            TimerFillImg.fillAmount -= (Time.deltaTime / 2) * (Random.Range(0.05f, .12f));
        }
        TimerCount = TimerFillImg.fillAmount;

        // if timer is less then zer thn game over function will called
        if (TimerCount <= 0)
        {
            gameover();

        }
    }

    //updating score 
    public void updatescore(int score)
    {
        ScoreCount += score;
        ScoreText.text = ScoreCount.ToString();
    }
    //traning completed now start the game
    public void checktraningstatus()
    {
        if (TotalTraningTarget > 0)
        {
            TotalTraningTarget -= 1;
        }

        if (TotalTraningTarget <= 0)
        {
            traningcompleted();
        }
    }
    void traningcompleted()
    {
        TraningObj.SetActive(false);
        TraningPopup.SetActive(false);

        FirstPanel.SetActive(true);
        StartPanel.SetActive(true);
        GameStatus = status.Waiting;

        Helper.setfirsttime(100);
    }
    //start game function
    public void startgame()
    {
        StartPanel.SetActive(false);
        FirstPanel.SetActive(false);

        GamePlayPanel.SetActive(true);
        GameStatus = status.Playing;

        asteriodspawn();

        audiomanager.instance.gamemusic(); // play bg music
    }

    //game over function
    public void gameover()
    {
        audiomanager.instance.gameoversound();

        GameOverPanel.SetActive(true);
        GameStatus = status.GameOver;

        checkhighscore(); // checking high score 

        EnemySpawner.instance.resetallenemies(); 
    }

    //game win
    public void gamewin()
    {
        GameStatus = status.win;
        audiomanager.instance.gameoversound();

        WinPanel.SetActive(true);

        //next level set
        CurrentLevel += 1;
        NextLevel = CurrentLevel;
        Helper.setlevel(NextLevel);
        NextLevelText.text = NextLevel.ToString();

        checkhighscore(); // checking high score 
    }
    // restart the game after game over
    public void restartgame()
    {
        GameOverPanel.SetActive(false);
        GameStatus = status.Waiting;

        asteriodspawn();

        //timer
        TimerCount = defaultfillamount;

        //score
        ScoreCount = 0;
        ScoreText.text = ScoreCount.ToString();

        // filling image again to one
        TimerCount = defaultfillamount;
        TimerFillImg.fillAmount = TimerCount;

        //TargetCount = 0;
        //targetassign();

        //totalkill
        TotalKillCount = 0;
        TotalKillText.text = TotalKillCount.ToString();

        StartPanel.SetActive(true);
        FirstPanel.SetActive(false);
        //GamePlayPanel.SetActive(true);

        audiomanager.instance.intromusic();
    }
    
    // next level
    public void nextlevel()
    {
        WinPanel.SetActive(false);

        GameStatus = status.win;

        //timer
        TimerCount = defaultfillamount;

        //score
        ScoreCount = 0;
        ScoreText.text = ScoreCount.ToString();

        // filling image again to one
        TimerCount = defaultfillamount;
        TimerFillImg.fillAmount = TimerCount;

        //target
        TargetCount = 0;
        targetassign();

        //totalkill
        TotalKillCount = 0;
        TotalKillText.text = TotalKillCount.ToString();

        StartPanel.SetActive(true);

        GamePlayPanel.SetActive(true);

        audiomanager.instance.intromusic();
    }

    //highscore function
    void checkhighscore()
    {
        HighScoreCount = Helper.GetHighScore();
        
        // if score is greater thn high score then it will be new high score
        if(ScoreCount >= HighScoreCount)
        {
            HighScoreCount = ScoreCount;
            Helper.sethighscore(HighScoreCount);
            HighScoreText.text = HighScoreCount.ToString();
        }
    }

    //activate or diactivated left pick UI canvas
    public void leftgunpickonoff(bool b)
    {
        PickGunLeftPanel.SetActive(b);
    }

    //activate or diactivated right pick UI canvas
    public void rightgunpickonoff(bool b)
    {
        PickGunRightPanel.SetActive(b);
    }

    //total kill
    public void updatetotalkill(int kill)
    {
        TotalKillCount += kill;

        if(TotalKillCount >= TargetCount) //win logic
        {
            TotalKillCount = TargetCount;

            gamewin();

        }

        TotalKillText.text = TotalKillCount.ToString();
    }

    //task
    void asteriodspawn()
    {
        InvokeRepeating(nameof(spawnenemy), 1, Random.Range(.5f, 2f));
        
    }

    void spawnenemy()
    {
        EnemySpawner.instance.spawnenemy();
    }


    //giving regerence to obj so that we can use this in other scrpits
    public GameObject GetBulletObj()
    {
        return BulletPrefab;
    }

    public status GetGameStatus()
    {
        return GameStatus;
    }

    public GameObject GetScorePopup()
    {
        return ScorePopup;
    }
    public GameObject GetHitUIParticleObj()
    {
        return UiHitParticleObj;
    }

}
