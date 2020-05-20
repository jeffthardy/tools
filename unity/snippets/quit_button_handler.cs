
    void QuitButtonOnClick()
    {
        if (backSceneName == "quit")
        {
            Debug.Log("You have clicked the quit button!");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        //Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
        }
        else
        {
            Debug.Log("Loading Menu");
            SceneManager.LoadScene(backSceneName);
        }

    }