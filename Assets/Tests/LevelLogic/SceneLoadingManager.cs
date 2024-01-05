/*
 * Reference: https://www.youtube.com/watch?v=OmobsXZSRKo
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    #region Scene Names
    public string startSceneName
    {
        get => "StartScene";
    }
    public string gameSceneName
    {
        get => "TestScene by tykuo";
    }
    #endregion


    #region Loading Menu
    [SerializeField] private GameObject loadingMenu;
    [SerializeField] private Image progressBar;
    private float targetProgress;
    [SerializeField] float progressBarSpeed = 0.1f;
    #endregion

    #region Singleton
    public static SceneLoadingManager Instance;

    public static bool DoesInstanceExit() => Instance != null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void LoadGameScene() => LoadScene(gameSceneName);
    public void LoadStartScene() => LoadScene(startSceneName);

    public void LoadScene(string sceneName)
    {
        Debug.Log("load scene " + sceneName);
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadingMenu.SetActive(true);
        targetProgress = 0;
        progressBar.fillAmount = 0f;

        var updateCoroutine = StartCoroutine(UpdateProgressBar());

        do
        {
            yield return new WaitForSecondsRealtime(0.1f);

            targetProgress = scene.progress / 0.9f;
        } while (targetProgress < 1f || progressBar.fillAmount < 1f);

        StopCoroutine(updateCoroutine);
        scene.allowSceneActivation = true;

        yield return new WaitForSeconds(0.3f);
        loadingMenu.SetActive(false);
    }

    private IEnumerator UpdateProgressBar()
    {
        while (true)
        {
            progressBar.fillAmount = Mathf.MoveTowards(
                progressBar.fillAmount, targetProgress,
                progressBarSpeed * Time.unscaledDeltaTime
            );
            //Debug.Log("amout " + progressBar.fillAmount);
            yield return new WaitForSecondsRealtime(0.017f);
        }
    }
}
