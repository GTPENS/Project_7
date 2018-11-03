using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayGameServices : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        setupGooglePlayeServices();
        signIn();
    }

    private void setupGooglePlayeServices()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    private void signIn()
    {
        Social.localUser.Authenticate(success => { });
    }

    #region Leaderboard
    public static void addScoreToLeaderBoard(string _leaderboardId, long _score)
    {
        Social.ReportScore(_score, _leaderboardId, success => { });
    }

    public static void showLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboard

    // Update is called once per frame
    void Update () {
		
	}
}
