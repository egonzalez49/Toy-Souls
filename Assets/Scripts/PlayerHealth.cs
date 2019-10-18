﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Image damageImage;
    public Image healthBar;
    //public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    //private Animator anim;
    //private AudioSource playerAudio;
    //private PlayerMovement playerMovement; //TODO: creates script for player movement
    private BarScript healthAmount;
    private bool isDead;
    private bool damaged;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        //playerMovement = GetComponent<PlayerMovement>();
        healthAmount = healthBar.GetComponent<BarScript>();
        currentHealth = startingHealth;
        healthAmount.SetFillAmount(currentHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TakeDamage(5);
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    //call this function to deal damage to the player
    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
        healthAmount.SetFillAmount(currentHealth);

        //healthSlider.value = currentHealth;

        /* play a hurt noise */
        //playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void IncreaseHealth(int amount)
    {
        if (currentHealth + amount > 100)
        {
            currentHealth = 100;
        } else
        {
            currentHealth += amount;
        }
        healthAmount.SetFillAmount(currentHealth);
    }

    void Death()
    {
        isDead = true;

        /* play a death animation */
        //anim.SetTrigger("Die");

        /* play a death noise */
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        /* disable player movement */
        //playerMovement.enabled = false;
    }
}
