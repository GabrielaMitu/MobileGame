using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawning notes
// We created a GameController game object in the scene with a GameController script (to implement the singleton game manager pattern).

public class GameController : MonoBehaviour
{
    public Transform lastSpawnedNote;
    private static float noteHeight;
    private static float noteWidth;
    public Note noteprfab;
    private Vector3 noteLocalScale;
    private float noteSpawnStartPosX;
    public float noteSpeed = 5f;
    public const int NotesToSpawn = 20;
    private int prevRandomIndex = -1;
    public static GameController Instance { get; private set; }
    public Transform noteContainer;

    private void Awake(){
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SetDataForNoteGeneration();
        SpawnNotes();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDataForNoteGeneration(){
        var topRight = new Vector3(Screen.width, Screen.height, 0); // Get the top right corner of the screen
        var topRightWorldPoint = Camera.main.ScreenToWorldPoint(topRight); // Convert screen point to world point
        var bottomLeftWorldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero); // Convert screen point to world point
        
        var screenWidth = topRightWorldPoint.x - bottomLeftWorldPoint.x; // Get the width of the screen
        var screenHeight = topRightWorldPoint.y - bottomLeftWorldPoint.y; // Get the height of the screen

        noteHeight = screenHeight / 4; // Divide the screen height by 4 to get the height of the note
        noteWidth = screenWidth / 4; // Divide the screen width by 4 to get the width of the note

        noteLocalScale = new Vector3(
               noteWidth / noteSpriteRenderer.bounds.size.x * noteSpriteRenderer.transform.localScale.x,
               noteHeight / noteSpriteRenderer.bounds.size.y * noteSpriteRenderer.transform.localScale.y, 1);

        var leftmostPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2));
        var leftmostPointPivot = leftmostPoint.x + noteWidth / 2;
        noteSpawnStartPosX = leftmostPointPivot;

    }

    public void SpawnNotes()
    {
        var noteSpawnStartPosY = lastSpawnedNote.position.y + noteHeight;
        Note note = null;
        for (int i = 0; i < NotesToSpawn; i++)
        {
            var randomIndex = GetRandomIndex();
            for (int j = 0; j < 4; j++)
            {
                note = Instantiate(notePrefab, noteContainer.transform);
                note.transform.localScale = noteLocalScale;
                note.transform.position = new Vector2(noteSpawnStartPosX + noteWidth * j, noteSpawnStartPosY);
                note.Visible = (j == randomIndex);
            }
            noteSpawnStartPosY += noteHeight;
            if (i == NotesToSpawn - 1) lastSpawnedNote = note.transform;
        }
    }
    

    private int GetRandomIndex()
    {
        var randomIndex = Random.Range(0, 4);
        while (randomIndex == prevRandomIndex) randomIndex = Random.Range(0, 4);
        prevRandomIndex = randomIndex;
        return randomIndex;
    }
}
