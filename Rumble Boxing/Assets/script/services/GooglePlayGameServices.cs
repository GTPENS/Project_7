using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayGameServices : MonoBehaviour {
    #region PUBLIC_VAR
    #endregion
    #region DEFAULT_UNITY_CALLBACKS
    void Start()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        LogIn();
    }
    #endregion
    #region BUTTON_CALLBACKS
    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn()
    {

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        LogIn();
        Social.ShowLeaderboardUI();
        //        Social.ShowLeaderboardUI (); // Show all leaderboard
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(RumbleBoxingResources.leaderboard_longest_round); // Show current (Active) leaderboard
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(GameplayDataManager.getInstance().HighScore, RumbleBoxingResources.leaderboard_longest_round, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
    #endregion
}