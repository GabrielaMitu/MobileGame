using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public Transform lastSpawnedNote;
    private static float noteHeight;
    private static float noteWidth;
    public Note notePrefab;
    private Vector3 noteLocalScale;
    private float noteSpawnStartPosX;
    public float noteSpeed = 5f;
    public const int NotesToSpawn = 53;
    private int prevRandomIndex = -1;
    public static GameController Instance { get; private set; }
    public Transform noteContainer;
    public ReactiveProperty<bool> GameStarted { get; set; }
    public ReactiveProperty<bool> GameOver { get; set; }
    public ReactiveProperty<int> Score { get; set; }
    private int lastNoteId = 1;
    public int LastPlayedNoteId { get; set; } = 0;
    public int ErrorNoteId { get; set; } = 0;
    public AudioSource audioSource;
    private Coroutine playSongSegmentCoroutine;
    private float songSegmentLength = 0.8f;
    private bool lastNote = false;
    private bool lastSpawn = false;
    public ReactiveProperty<bool> ShowGameOverScreen { get; set; }
    public bool PlayerWon { get; set; } = false;
    public GameObject WinScreen;
    public GameObject OverScreen;
    // shield
    public bool usingShield = false;

    // freeze
    public bool usingFreeze = false;
    public bool isSlow = false;
    public int maxNotesFreeze = 7;
    public int currentNotesFreeze = 0;

    // detect when pressed start
    public bool startButtonPressed  = false;

    public string argument;
    public float speed;
    public AudioClip mozart;
    public AudioClip mozart2;
    public AudioClip classica1;
    public AudioClip demo;
    private List<int> lista_notas = new List<int>();
    public string filePath;


    public void MissedNote()
    {
        if(usingFreeze)
        {
            if(isSlow) {
                noteSpeed /= 2;
            }
            isSlow = true;
        } else if (usingShield)
        {
            usingShield = false;
        }
    }


    public void ClickedNote()
    {
        if(isSlow)
        {
            currentNotesFreeze++;
            if (currentNotesFreeze == maxNotesFreeze)
            {
                usingFreeze = false;
                isSlow = false;
                currentNotesFreeze = 0;
                noteSpeed *= 2;
            }
        }
    }


    private void Awake()
    {
        Instance = this;
        GameStarted = new ReactiveProperty<bool>();
        GameOver = new ReactiveProperty<bool>();
        Score = new ReactiveProperty<int>();
        ShowGameOverScreen = new ReactiveProperty<bool>();
    }

    void Start()
    {
        WinScreen.SetActive(false);
        OverScreen.SetActive(false);
        argument = PlayerPrefs.GetString("Music");
        Debug.Log(argument);

        if (argument == "mozart")
        {
            audioSource.clip = mozart;
            filePath = "Assets/Notas/mozart.txt";
        }
        else if (argument == "mozart2")
        {
            audioSource.clip = mozart2;
            filePath = "Assets/Notas/mozart2.txt";
        }
        else if (argument == "classica1")
        {
            audioSource.clip = classica1;
            filePath = "Assets/Notas/classica1.txt";
        }
        else
        {
            audioSource.clip = demo;
            filePath = "Assets/Notas/classica1.txt";
        }

        ReadTextFile();
        Debug.Log(lista_notas.Count);
        speed = float.Parse(PlayerPrefs.GetString("Speed"));
        Time.timeScale = 1f*speed;
        // string speed_nova = (speed+1).ToString();
        // PlayerPrefs.SetString("Speed",speed_nova);

        SetDataForNoteGeneration();
        SpawnNotes();
    }

    private void Update()
    {
        DetectNoteClicks();
        DetectStart();
    }

    public void OnStartButtonClick()
    {
        startButtonPressed = true;
    }

    private void DetectStart()
    {
        if (!GameController.Instance.GameStarted.Value && startButtonPressed)
        {
            GameController.Instance.GameStarted.Value = true;
        }
    }

    private void DetectNoteClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(origin, Vector2.zero);
            if (hit)
            {
                var gameObject = hit.collider.gameObject;
                if (gameObject.CompareTag("Note"))
                {
                    var note = gameObject.GetComponent<Note>();
                    note.Play();
                }
            }
        }
    }


    private void SetDataForNoteGeneration()
    {
        var topRight = new Vector3(Screen.width, Screen.height, 0);
        var topRightWorldPoint = Camera.main.ScreenToWorldPoint(topRight);
        var bottomLeftWorldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var screenWidth = topRightWorldPoint.x - bottomLeftWorldPoint.x;
        var screenHeight = topRightWorldPoint.y - bottomLeftWorldPoint.y;
        noteHeight = screenHeight / 4;
        noteWidth = screenWidth / 4;
        var noteSpriteRenderer = notePrefab.GetComponent<SpriteRenderer>();
        noteLocalScale = new Vector3(
               noteWidth / noteSpriteRenderer.bounds.size.x * noteSpriteRenderer.transform.localScale.x,
               noteHeight / noteSpriteRenderer.bounds.size.y * noteSpriteRenderer.transform.localScale.y, 1);
        var leftmostPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2));
        var leftmostPointPivot = leftmostPoint.x + noteWidth / 2;
        noteSpawnStartPosX = leftmostPointPivot;
    }

    public void SpawnNotes()
    {
        if (lastSpawn) return;

        var noteSpawnStartPosY = lastSpawnedNote.position.y + noteHeight;
        Note note = null;
        var timeTillEnd = audioSource.clip.length - audioSource.time;
        int notesToSpawn = NotesToSpawn;
        if (timeTillEnd < NotesToSpawn)
        {
            notesToSpawn = Mathf.CeilToInt(timeTillEnd);
            lastSpawn = true;
        }
        for (int i = 0; i < notesToSpawn; i++)
        {
            var randomIndex = lista_notas[i]-1;
            for (int j = 0; j < 4; j++)
            {
                note = Instantiate(notePrefab, noteContainer.transform);
                note.transform.localScale = noteLocalScale;
                note.transform.position = new Vector2(noteSpawnStartPosX + noteWidth * j, noteSpawnStartPosY);
                note.Visible = (j == randomIndex);
                if (note.Visible)
                {
                    note.Id = lastNoteId;
                    lastNoteId++;
                }
            }
            noteSpawnStartPosY += noteHeight;
            if (i == NotesToSpawn - 1) lastSpawnedNote = note.transform;
        }
        if (lastNoteId >= (NotesToSpawn)) 
        {
            Debug.Log("!!!!!!!!!!!!!!!!!");
            Invoke("Ganhou", 1.5f);

        }
    }

    private int GetRandomIndex()
    {
        var randomIndex = Random.Range(0, 4);
        while (randomIndex == prevRandomIndex) randomIndex = Random.Range(0, 4);
        prevRandomIndex = randomIndex;
        return randomIndex;
    }

    public void PlaySomeOfSong()
    {
        if (!audioSource.isPlaying && !lastNote)
        {
            audioSource.Play();
        }
        if (audioSource.clip.length - audioSource.time <= songSegmentLength)
        {
            lastNote = true;
        }
        if (playSongSegmentCoroutine != null) StopCoroutine(playSongSegmentCoroutine);
        playSongSegmentCoroutine = StartCoroutine(PlaySomeOfSongCoroutine());
    }

    private IEnumerator PlaySomeOfSongCoroutine()
    {
        yield return new WaitForSeconds(songSegmentLength);
        audioSource.Pause();
        if (lastNote)
        {
            PlayerWon = true;
            StartCoroutine(EndGame());
        }
    }

    public void Ganhou()
    {
        Time.timeScale = 0;
        WinScreen.SetActive(true);
    }
    public void PlayAgain()
    {
        PlayerPrefs.SetString("Speed",speed.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayAgainFaster()
    {
        string speed_nova = (speed+0.5).ToString();
        PlayerPrefs.SetString("Speed",speed_nova);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public IEnumerator EndGame()
    {
        GameOver.Value = true;
        yield return new WaitForSeconds(1);
        // ShowGameOverScreen.Value = true;
        OverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Voltar()
    {
        Debug.Log("Voltar");
        GameOver.Value = false;
        // yield return new WaitForSeconds(1);
        // ShowGameOverScreen.Value = false;

        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Note");
        // Loop through the tagged objects
        foreach (GameObject taggedObject in taggedObjects)
        {
            taggedObject.transform.position = new Vector3(taggedObject.transform.position.x, taggedObject.transform.position.y+5, taggedObject.transform.position.z);
        }
        OverScreen.SetActive(false);
        Time.timeScale = 1f*speed;
    }


    private void ReadTextFile()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (int.TryParse(line, out int nota))
                {
                    lista_notas.Add(nota);
                }
                else
                {
                    Debug.LogError("Invalid integer value in the text file: " + line);
                }
            }
        }
        else
        {
            Debug.LogError("Text file not found at path: " + filePath);
        }
    }




}
