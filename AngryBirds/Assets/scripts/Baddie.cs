using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float damageBabbie = 0.2f;
    [SerializeField] public GameObject BaddieDeathParticle;
    [SerializeField] private AudioClip DieClip;
    
    private float currentHealth;
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = maxHealth;
    }
    public void DamageBaddie(float DamageAmount)
    {
        currentHealth -= DamageAmount;
        if (currentHealth <= 0f)
        {
           
            AudioSource.PlayClipAtPoint(DieClip,transform.position);  
            
            Instantiate(BaddieDeathParticle,transform.position, Quaternion.identity);
            GameManager.Instance.RemoveBaddie(this);
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity=collision.relativeVelocity.magnitude;
        if (impactVelocity > damageBabbie)
        {
            DamageBaddie(impactVelocity);
        }
    }
}
