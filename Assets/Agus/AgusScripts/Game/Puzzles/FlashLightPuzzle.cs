using Game.Puzzles;
using UnityEngine;

public class FlashLightPuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private string puzzleID = "flashlightPuzzle";
    [SerializeField] private string requiredItemID = "flashlight_item";

    private bool isSolved = false;
    public bool IsSolved => isSolved;

    private void Start()
    {
        PuzzleManager.Instance.RegisterPuzzle(puzzleID, this);
        PuzzleManager.Instance.ActivatePuzzle(puzzleID);
    }

    public void Activate()
    {
        if (isSolved) return;

        Debug.Log("[FlashlightPuzzle] Activado. Esperando a que el jugador recoja la linterna...");
        StartCoroutine(CheckForFlashlightCoroutine());
    }

    public void Deactivate()
    {
        StopAllCoroutines();
    }

    private System.Collections.IEnumerator CheckForFlashlightCoroutine()
    {
        while (!isSolved)
        {
            if (InventoryManager.Instance.HasItem(requiredItemID))
            {
                isSolved = true;
                Debug.Log("[FlashlightPuzzle] Linterna detectada en el inventario. Puzzle completado.");
                LoopManager.Instance.SetConditionMet(true);
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}