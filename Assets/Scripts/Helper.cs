using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    //First time playing
    public static int GetFirstTime()
    {
        return PlayerPrefs.GetInt("firsttime");
    }
    public static void setfirsttime(int i)
    {
        PlayerPrefs.SetInt("firsttime", i);
    }

    //high score
    // fetch high score 
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("Highscore");
    }
    // updating high score
    public static void sethighscore(int score)
    {
        PlayerPrefs.SetInt("Highscore", score);
    }

    //level
    // fetch level 
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("level");
    }
    // updating level
    public static void setlevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }
}
