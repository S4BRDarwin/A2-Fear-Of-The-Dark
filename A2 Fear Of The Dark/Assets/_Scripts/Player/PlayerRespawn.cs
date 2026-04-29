using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private PlayerRespawnSO playerRespawnSO;

    void OnEnable()
    {
        playerRespawnSO.PlayerDeath += BeginReset;
    }

    void OnDisable()
    {
        playerRespawnSO.PlayerDeath += BeginReset;
    }

    public void BeginReset()
    {
        StartCoroutine(ResetScene());
    }

    private IEnumerator ResetScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("FirstAreaScene");
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

}
