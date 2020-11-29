using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUIManager : MonoBehaviour
{
    private DialogueBranch branch;

    public GameObject background;
    public GameObject mainText;
    public GameObject responseTab;
    public GameObject continueText;
    public List<Button> responsesHolder;

    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private int responses = 0;

    // Start is called before the first frame update
    void Start()
    {
        DeactiveDialogue();
    }

    public void ActiveDialogue()
    {
        background.SetActive(true);
        mainText.SetActive(true);
        continueText.SetActive(true);
        responseTab.SetActive(true);
        foreach (Button response in responsesHolder)
        {
            response.gameObject.SetActive(false);
        }
    }

    public void DeactiveDialogue()
    {
        background.SetActive(false);
        mainText.SetActive(false);
        continueText.SetActive(false);
        responseTab.SetActive(false);
        foreach (Button response in responsesHolder)
        {
            response.gameObject.SetActive(false);
        }
    }

    public void NextBranch(int branchSelect)
    {
        // Add ReciveDialogueBranch with newBranch being next branch
        RecieveDialogueBranch(branch.ResponseOption[branchSelect].nextBranch);
        ActiveDialogue();
        NextDialogue();
    }

    
    public void RecieveDialogueBranch(DialogueBranch newBranch)
    {
        this.branch = newBranch;
        responses = Mathf.Clamp(branch.ResponseOption.Count, 0,3);
        currentIndex = 0;
    }

    public void NextDialogue()
    {
        //If we have reached the end of the dialogue
        if (currentIndex >= branch.DialogueLines.Count)
        {
            //there are no responses left
            if (responses == 0) {
                DeactiveDialogue();
            } 
            else {
                
                continueText.SetActive(false);
                for (int i = 0; i < responses; i++)
                {
                    if (i >= 3)
                    {
                        break; //exits for loop if i is not equal to a valid index in our response list
                    }
                    //Activate button for response
                    responsesHolder[i].gameObject.SetActive(true);
                    //place response text
                    responsesHolder[i].GetComponentInChildren<TextMeshProUGUI>().text = branch.ResponseOption[i].text;
                }
            }
        } else {
            //There is still text to show
            mainText.GetComponent<TextMeshProUGUI>().text = branch.DialogueLines[currentIndex];
            continueText.SetActive(true);
            currentIndex++;
        }
    }
}
