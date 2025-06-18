using UnityEngine;

public class DialogueTrigger : MonoBehaviour


{
    [Header("Secuencia de Diálogo")]
    public DialogueSequence introSecuencia;        // Arrastra aquí “IntroSecuencia”

    [Header("Referencias")]
    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        //dialogueManager = FindFirstObjectByType<DialogueManager>();
        if (dialogueManager == null)
            Debug.LogError("No se encontró DialogueManager en la escena.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Asume que el jugador tiene tag "Player"
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(introSecuencia);
            // Desactivar este trigger para que no vuelva a dispararse
            enabled = false;
        }
    }
}
