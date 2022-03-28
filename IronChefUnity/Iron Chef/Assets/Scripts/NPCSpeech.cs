using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPCSpeech : MonoBehaviour
{
    Image DialogueBox;
    Image DialoguePrompt;
    Text DialogueText;
    Text NPCNameText;
    [Space]
    public float DialogueDistance;
    public float DialogueCancelDistance;
    public string[] speech;
    public string NPC_Name;

    bool talking;
    bool inRange;
    int currentSpeechIndex;
    Transform player;

    static int inRangeCount;


    private void Start()
    {
        inRange = false;
        talking = false;
        player = FindObjectOfType<CharacterMover>().transform;
        inRangeCount = 0;
        currentSpeechIndex = 0;

        DialogueBox = GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<Image>();
        DialoguePrompt = GameObject.FindGameObjectWithTag("DialoguePrompt").GetComponent<Image>();
        DialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
        NPCNameText = GameObject.FindGameObjectWithTag("NPCNameText").GetComponent<Text>();

        InputControls.controls.Gameplay.Dialogue.performed += Talk;
    }

    private void Update()
    {
        if(talking)
        {
            if(Vector3.Distance(player.position, transform.position) > DialogueCancelDistance)
            {
                StopTalking();
            }
        }
        else
        {
            if(Vector3.Distance(player.position, transform.position) <= DialogueDistance)
            {
                if (!inRange)
                    inRangeCount++;
                inRange = true;
            }
            else
            {
                if (inRange)
                    inRangeCount--;
                inRange = false;
            }

            TogglePrompt();
        }

           
    }

    private void TogglePrompt()
    {
        DialoguePrompt.enabled = (inRangeCount > 0);
    }

    public void Talk(InputAction.CallbackContext obj)
    {
        if(inRange)
        {

            if (!talking)
            {
                talking = true;
                currentSpeechIndex = -1;
                DialogueBox.enabled = true;
                SetName();
            }

            AdvanceDialogue();

            //TODO: Add sound
        }


    }
    private void StopTalking()
    {
        talking = false;
        DialogueBox.enabled = false;
        DialogueText.text = "";
        NPCNameText.text = "";
    }
    private void AdvanceDialogue()
    {
        currentSpeechIndex++;
        if(currentSpeechIndex >= speech.Length)
        {
            StopTalking();
        }
        else
        {
            SetDialogue();
        }
    }
    private void SetDialogue()
    {
        DialogueText.text = speech[currentSpeechIndex];
        Debug.Log(currentSpeechIndex);
    }
    private void SetName()
    {
        NPCNameText.text = NPC_Name;
    }
}
