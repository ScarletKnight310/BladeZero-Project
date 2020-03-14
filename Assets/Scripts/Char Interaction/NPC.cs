using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public Transform ChatBackGround;
    private DialogueSystem dialogueSystem;

    public string Name;
    public string interactables = "Player";

    public int height = 200;
    //public bool isChargeStation = false;

    [TextArea(5, 20)]
    public string[] sentences;

    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(transform.position);
        Pos.y += height;
        ChatBackGround.position = Pos;
        this.gameObject.GetComponent<NPC>().enabled = false;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
         
            this.gameObject.GetComponent<NPC>().enabled = true;
            FindObjectOfType<DialogueSystem>().EnterRangeOfNPC();
            //
            if (other != null && (other.gameObject.tag == interactables) && Input.GetButton("Interact")) {
                this.gameObject.GetComponent<NPC>().enabled = true;
                dialogueSystem.Names = Name;
                dialogueSystem.dialogueLines = sentences;
                FindObjectOfType<DialogueSystem>().NPCName();
            }
        
    }

    public void OnTriggerExit2D()
    {
        FindObjectOfType<DialogueSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
