﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    Animator animator;

    // Items to be used
    public GameObject shield;
    public GameObject freeze;

    public bool usingFreeze = false;
    public bool usingShield = false;

    private bool visible;
    public bool Visible
    {
        get => visible;
        set
        {
            visible = value;
            if (!visible) animator.Play("Invisible");
        }
    }

    public bool Played { get; set; }
    public int Id { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameController.Instance.GameStarted.Value && !GameController.Instance.GameOver.Value)
        {
            transform.Translate(Vector2.down * GameController.Instance.noteSpeed * Time.deltaTime);
        }
    }

    public void Play()
    {
        if (GameController.Instance.GameStarted.Value && !GameController.Instance.GameOver.Value)
        {
            if (Visible)
            {
                if (!Played && GameController.Instance.LastPlayedNoteId == Id - 1)
                {
                    Played = true;
                    GameController.Instance.LastPlayedNoteId = Id;
                    GameController.Instance.Score.Value++;
                    GameController.Instance.PlaySomeOfSong();
                    animator.Play("Played");
                }
            }
            else
            {
                StartCoroutine(GameController.Instance.EndGame());
                animator.Play("Missed");
            }
        }
    }

    public void OutOfScreen()
    {
        if (Visible && !Played)
        {
            StartCoroutine(GameController.Instance.EndGame());
            animator.Play("Missed");
        }
    }


    public void OnFreezeButtonClick()
    {
        if (usingFreeze){
            usingFreeze = false;
            freeze.SetActive(false);
        }
        else{
            usingFreeze = true;
            freeze.SetActive(true);
        }
    }

    public void OnShieldButtonClick()
    {
        if (usingShield){
            usingShield = false;
            shield.SetActive(false);
        }
        else{
            usingShield = true;
            shield.SetActive(true);
        }
    }
}
