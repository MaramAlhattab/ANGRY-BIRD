using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class slingShotHander : MonoBehaviour
{
    
    [Header("line Renderer")]
    [SerializeField]private LineRenderer LeftLineRenderer;
    [SerializeField] private LineRenderer RightLineRenderer;

    [Header("Transform")]
    [SerializeField] private Transform LeftTransformtLine;
    [SerializeField] private Transform RightTransformtLine;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;
    [SerializeField] private Transform Elastic;
   
    
    private Vector2 slingshotLinesPosition;
    private Vector2 dirction;
    private Vector2 dirctionNormalized;

    [Header("slingshot")]
    [SerializeField] private float maxDistance = 3.5f;
    [SerializeField] private float shotForce = 5f;
    [SerializeField] private float timeBetweenRespawns = 2f;
    [SerializeField] private float ElasticDivider = 1.2f;
    [SerializeField] private AnimationCurve ElasticCurve;
    [SerializeField] private float MaxAnimationTime = 1f;

    private bool clickedwithinArea;
    private bool birdonSlingShot;
    [Header("scripts")]
    [SerializeField] private slingShotArea slingShotArea;
    [SerializeField] private CameraManagerScript CameraManager;

    [Header("Birds")]
    [SerializeField]private AngrryBirds BirdPrefabs;
    [SerializeField] private float angryBirdPositionOffset= 0.275f;
    private AngrryBirds spawnBirds;
    [Header("Sounds")]
    [SerializeField] private AudioClip elasticPulledClip;
    [SerializeField] private AudioClip[] elasticReleasedClips;
    private AudioSource AudioSource;
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        LeftLineRenderer.enabled = false;
        RightLineRenderer.enabled = false;
        spawnAngryBirds();
    }

   
    void Update()
    {
        if (InputManager.wasleftmousebuttonpreesed && slingShotArea.IsWithinSlingShotArea())
        {
            clickedwithinArea=true;
            if (birdonSlingShot)
            {
                SoundManager.instance.PlayClib(elasticPulledClip,AudioSource);
                CameraManager.SwitchTofollowCamera(spawnBirds.transform);
            }
        }
        if (InputManager.isleftmousepreesed && clickedwithinArea && birdonSlingShot)
        {
           DrawSlingShot();
           PositionAndRotitionBird();
        }
        if (InputManager.wasleftmousebuttonReleased && birdonSlingShot && clickedwithinArea)
        {
            if (GameManager.Instance.HasEnoughShots())
            {
                clickedwithinArea = false;
                birdonSlingShot = false;

                spawnBirds.LunchBird(dirction, shotForce);

                SoundManager.instance.PlayRandomClib(elasticReleasedClips, AudioSource);
              
                GameManager.Instance.UseShot();
                AnimateSlingShot();

                setlines(centerPosition.position);

                if (GameManager.Instance.HasEnoughShots())
                {
                    StartCoroutine(spawnAngrayBirdsAfterTime());
                }
            }
          
        }

    }
    #region SlingShot methods
    private void DrawSlingShot()
    {
       
        Vector3 touchPosition =Camera.main.ScreenToWorldPoint(InputManager.mousePosition);

        slingshotLinesPosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance);
        setlines(slingshotLinesPosition);
        dirction = (Vector2)centerPosition.position - slingshotLinesPosition;
        dirctionNormalized = dirction.normalized;
        birdonSlingShot =true;
    }

    private void setlines(Vector3 position)
    {
        if (!LeftLineRenderer.enabled && !RightLineRenderer.enabled)
        {
            LeftLineRenderer.enabled = true;
            RightLineRenderer.enabled = true;
        }
        LeftLineRenderer.SetPosition(0,position);
        LeftLineRenderer.SetPosition(1, LeftTransformtLine.position);

        RightLineRenderer.SetPosition(0, position);
        RightLineRenderer.SetPosition(1, RightTransformtLine.position);
    }
    #endregion

    #region birds methds
    private void spawnAngryBirds()
    {
        Elastic.DOComplete();
        setlines(idlePosition.position);

        Vector2 dir =(centerPosition.position - idlePosition.position).normalized;
        Vector2 spawnPosition=(Vector2)idlePosition.position + dir * angryBirdPositionOffset;
        spawnBirds =Instantiate(BirdPrefabs, spawnPosition,Quaternion.identity);
        spawnBirds.transform.right=dir;
        birdonSlingShot=true;

    }
    private void PositionAndRotitionBird()
    {
        spawnBirds.transform.position = slingshotLinesPosition + dirctionNormalized * angryBirdPositionOffset;
        spawnBirds.transform.right = dirctionNormalized;
    }
    private IEnumerator spawnAngrayBirdsAfterTime()
    {
        yield return new WaitForSeconds(timeBetweenRespawns);
        spawnAngryBirds();
        CameraManager.SwitchToIdleCamera();
    }
    #endregion
    #region animate slingshot
    private void AnimateSlingShot()
    {
        Elastic.position = LeftLineRenderer.GetPosition(0);
        float dist = Vector2.Distance(Elastic.position,centerPosition.position);
        float time = dist / ElasticDivider;

        Elastic.DOMove(centerPosition.position,time).SetEase(ElasticCurve);
        StartCoroutine(AnimateSlingShotLine(Elastic,time));
    }
    private IEnumerator AnimateSlingShotLine(Transform trans , float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time && elapsedTime < MaxAnimationTime) 
        { 
            elapsedTime += Time.deltaTime;
            setlines(trans.position);
            yield return null;
        }
    }

    #endregion
}
