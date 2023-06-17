using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    Animator animator;

    bool isSlow = false;

    // Items to be used
    Image shield;
    Image freeze;

    bool usingFreeze = false;
    bool usingShield = false;

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

        shield = GameObject.FindGameObjectWithTag("ImageShield").GetComponent<Image>();
        freeze = GameObject.FindGameObjectWithTag("ImageFreeze").GetComponent<Image>();
    }

    // private void Start()
    // {
        // if (shield!=null)
        // {
        //     shield.SetActive(false);
        // }
        // if (freeze!=null)
        // {
        //     freeze.SetActive(false);
        // }
        // shield.enabled = false;
        // freeze.enabled = false;
    // }

    private void Update()
    {
        isSlow = GameController.Instance.isSlow;
        if (GameController.Instance.GameStarted.Value && !GameController.Instance.GameOver.Value)
        {
            transform.Translate(Vector2.down * GameController.Instance.noteSpeed * Time.deltaTime);
        }
    }

    public void Play()
    {
        if (GameController.Instance.GameStarted.Value && !GameController.Instance.GameOver.Value)
        {
            if (Visible) // if note is visible on screen
            {
                if (!Played && GameController.Instance.LastPlayedNoteId == Id - 1)
                {

                    if(isSlow){
                        GameController.Instance.ClickedNote();
                    }

                    Played = true;
                    GameController.Instance.LastPlayedNoteId = Id;
                    GameController.Instance.Score.Value++;
                    GameController.Instance.PlaySomeOfSong();
                    animator.Play("Played");
                }
            }
            else // if note is not visible on screen
            {
                if((freeze.enabled == false && shield.enabled == false)){
                    Debug.Log("Game Over");
                    GameController.Instance.ErrorNoteId = Id;
                    StartCoroutine(GameController.Instance.EndGame());
                    animator.Play("Missed");
                }

                else if(freeze.enabled){
                    GameController.Instance.MissedNote();

                    usingFreeze = false;
                    freeze.enabled = false;
                    // Debug.Log(this.isSlow);

                    // slow down the note speed for 7 notes
                    GameController.Instance.noteSpeed /= 2f;
                    Played = true;
                    GameController.Instance.LastPlayedNoteId = Id;
                    GameController.Instance.Score.Value++;
                    GameController.Instance.PlaySomeOfSong();
                    animator.Play("Played");
                }

                else if(shield.enabled){
                    GameController.Instance.MissedNote();

                    usingShield = false;
                    shield.enabled = false;
                    Played = true;
                    GameController.Instance.LastPlayedNoteId = Id;
                    GameController.Instance.Score.Value++;
                    GameController.Instance.PlaySomeOfSong();
                    animator.Play("Played");
                }
            }
        }
    }

    public void OutOfScreen()
    {
        if (Visible && !Played)
        {
            if((freeze.enabled == false && shield.enabled == false)){
                Debug.Log("Game Over");
                GameController.Instance.ErrorNoteId = Id;
                StartCoroutine(GameController.Instance.EndGame());
                animator.Play("Missed");
            }

            else if(freeze.enabled){
                GameController.Instance.MissedNote();

                usingFreeze = false;
                freeze.enabled = false;
                // Debug.Log(this.isSlow);

                // slow down the note speed for 7 notes
                GameController.Instance.noteSpeed /= 2f;
                Played = true;
                GameController.Instance.LastPlayedNoteId = Id;
                GameController.Instance.Score.Value++;
                GameController.Instance.PlaySomeOfSong();
                animator.Play("Played");
            }

            else if(shield.enabled){
                GameController.Instance.MissedNote();

                usingShield = false;
                shield.enabled = false;
                Played = true;
                GameController.Instance.LastPlayedNoteId = Id;
                GameController.Instance.Score.Value++;
                GameController.Instance.PlaySomeOfSong();
                animator.Play("Played");
            }
        }
    }

    public void OnFreezeButtonClick()
    {
        if (freeze.enabled){
            usingFreeze = false;
            freeze.enabled = false;
        }
        else{
            usingFreeze = true;
            freeze.enabled = true;
            Debug.Log(freeze.enabled);
        }
        GameController.Instance.usingFreeze = usingFreeze;
    }

    public void OnShieldButtonClick()
    {
        if (shield.enabled){
            usingShield = false;
            shield.enabled = false;
        }
        else{
            usingShield = true;
            shield.enabled = true;
        }
        GameController.Instance.usingShield = usingShield;
    }
}
