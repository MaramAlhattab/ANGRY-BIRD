using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngrryBirds : MonoBehaviour
{
    [SerializeField] private AudioClip HitClip;
    private AudioSource AudioSource;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private bool hasBeenLanched;
    private bool shouldFaceVelDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        rb.isKinematic = true;
        circleCollider.enabled=false;
        AudioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (hasBeenLanched && shouldFaceVelDirection)
        {
            transform.right = rb.velocity;

        }
    }

    public void LunchBird(Vector2 diraction,float force)
    {
        rb.isKinematic=false;
        circleCollider.enabled=true;
        //apply the force
        rb.AddForce(diraction*force,ForceMode2D.Impulse);
        hasBeenLanched = true;
        shouldFaceVelDirection=true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldFaceVelDirection=false;
        SoundManager.instance.PlayClib(HitClip, AudioSource);
        Destroy(this);
    }
}
