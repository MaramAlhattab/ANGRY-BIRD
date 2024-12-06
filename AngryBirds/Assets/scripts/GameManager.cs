using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int MaxNumberOfShot = 3;
    [SerializeField] private float secndstoWaitBeforDeathChec = 3f;
    [SerializeField] private GameObject restartgame;
    [SerializeField] private slingShotHander slingShotHander;
    [SerializeField] private Image nextImageLevel;
    private int UsedNumberOfShot ;
    private IconHandier IconHandier ;
    private List<Baddie> _baddies = new List<Baddie>();

   
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        IconHandier = GameObject.FindObjectOfType<IconHandier>();
        Baddie[] baddies =FindObjectsOfType<Baddie>();

        for (int i = 0; i < baddies.Length; i++)
        {
            _baddies.Add(baddies[i]);
        }
        nextImageLevel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UseShot()
    {
        UsedNumberOfShot++;
        IconHandier.UseShot1(UsedNumberOfShot);
        checkForLastShot();
    }
    public bool HasEnoughShots()
    { 
        if(UsedNumberOfShot < MaxNumberOfShot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void checkForLastShot()
    {
        if (UsedNumberOfShot == MaxNumberOfShot)
        {
            StartCoroutine(checkAfterWaitTime());
        }
    }
    private IEnumerator checkAfterWaitTime()
    {
        yield return new WaitForSeconds(secndstoWaitBeforDeathChec);

        if (_baddies.Count == 0)
        {
            winGame();
        }
        else
        {
            RestartGame();
        }
    }
    public void RemoveBaddie(Baddie baddie)
    {
        _baddies.Remove(baddie);
    }
    #region win/lose
    public void winGame()
    {
        
        restartgame.SetActive(true);
        slingShotHander.enabled = false ;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int MaxSceneIndex   = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex +1 < MaxSceneIndex)
        {
            nextImageLevel.enabled = true;
        }
    }
    public void RestartGame()
    {
       DOTween.Clear(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    #endregion
}
