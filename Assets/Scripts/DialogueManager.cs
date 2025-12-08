using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text characterNameText;
    public TMP_Text dialogueText;
    public Image portraitLeft;
    public Image portraitRight;
    public GameObject dialoguePanel;

    [Header("Choice Buttons (posicionados manualmente)")]
    public Button[] choiceButtons; // arrastar do Canvas!

    [Header("Typewriter")]
    public float typeSpeed = 0.03f;

    private DialogueLine[] lines;
    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingRoutine;

    public Action OnDialogueEnd;

    void Start()
    {
        dialoguePanel.SetActive(false);

        // Desativa botões no início
        HideChoices();
    }

    public void StartDialogue(DialogueLine[] dialogueLines)
    {
        lines = dialogueLines;
        currentIndex = 0;
        dialoguePanel.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        HideChoices();

        DialogueLine line = lines[currentIndex];

        characterNameText.text = line.speakerName;

        // Retratos
        portraitLeft.sprite = line.leftPortrait;
        portraitRight.sprite = line.rightPortrait;

        portraitLeft.color = new Color(1, 1, 1, line.speakerIsLeft ? 1f : 0.4f);
        portraitRight.color = new Color(1, 1, 1, line.speakerIsLeft ? 0.4f : 1f);

        // Digitação
        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(TypeText(line.text));

        // Se houver escolhas → mostrar
        if (line.choices != null && line.choices.Length > 0)
        {
            ShowChoices(line.choices);
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    // Apenas avança se não for linha de escolhas
    public void NextDialogue()
    {
        if (isTyping)
        {
            StopCoroutine(typingRoutine);
            dialogueText.text = lines[currentIndex].text;
            isTyping = false;
            return;
        }

        // Se existir choice, não pode usar espaço
        if (lines[currentIndex].choices != null && lines[currentIndex].choices.Length > 0)
            return;

        currentIndex++;

        if (currentIndex >= lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        OnDialogueEnd?.Invoke();
    }

    // ----------------------------------------------------------------

    void ShowChoices(DialogueChoice[] choices)
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i]
                    .GetComponentInChildren<TMP_Text>().text = choices[i].text;

                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() =>
                {
                    SelectChoice(choices[choiceIndex]);
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void SelectChoice(DialogueChoice choice)
    {
        HideChoices();

        if (choice.nextIndex >= 0)
        {
            currentIndex = choice.nextIndex;
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void HideChoices()
    {
        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
            NextDialogue();
    }
}
