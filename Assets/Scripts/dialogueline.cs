using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public int nextIndex = -1; // -1 encerra o diálogo após a escolha
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName;

    [TextArea(2, 5)]
    public string text;

    public bool speakerIsLeft = true;
    public Sprite leftPortrait;
    public Sprite rightPortrait;

    public DialogueChoice[] choices; // Se tiver choices, é uma linha de decisão
}
