using UnityEngine;
using System.Collections.Generic;

namespace Game.Puzzles
{
    public class PhotoFramePuzzle : MonoBehaviour, IPuzzle
    {
        [SerializeField] private string puzzleID = "photoPuzzle";

        [Tooltip("IDs de los fragmentos requeridos para completar el puzzle")]
        [SerializeField] private List<string> requiredItemIDs = new();

        [Tooltip("Cada fragmento en el cuadro (ya colocado) debe estar en esta lista, su nombre debe coincidir con su ID")]
        [SerializeField] private List<GameObject> framePieces; // uno por ID



        private HashSet<string> insertedItems = new();
        private bool isSolved = false;
        private int touchCount = 0;

        public bool IsSolved => isSolved;

        private void Start()
        {
            PuzzleManager.Instance.RegisterPuzzle(puzzleID, this);

            // Asegurarse de que todos los fragmentos empiecen ocultos
            foreach (var piece in framePieces)
            {
                if (piece.TryGetComponent<MeshRenderer>(out var renderer))
                    renderer.enabled = false;
            }
        }

        public void Activate()
        {
            if (isSolved) return;
            touchCount++;
            if (touchCount == 1)
            {
                // initialize a strings door id list
                List<string> doorIDs = new List<string> { "main_bedroom_door", "bathroom_door" };
                DoorManager.Instance.UnlockDoors(doorIDs);
                DoorManager.Instance.OpenDoors(doorIDs);
            }
            Debug.Log($"[Puzzle] {puzzleID} activado. Inserta los fragmentos...");
            UIStateManager.Instance.SetState(UIState.Inventory);
            InventoryCarouselUI.Instance.Open();

            InventoryManager.Instance.OnItemUsedExternally = TryInsertPiece;
        }

        public void Deactivate()
        {
            InventoryManager.Instance.OnItemUsedExternally = null;
        }

        private void TryInsertPiece(string itemId)
        {
            if (isSolved)
            {
                Debug.Log("Puzzle ya resuelto.");
                return;
            }

            if (!requiredItemIDs.Contains(itemId))
            {
                Debug.Log($"El ítem '{itemId}' no pertenece a este puzzle.");
                return;
            }

            if (insertedItems.Contains(itemId))
            {
                Debug.Log($"Ya colocaste el fragmento '{itemId}'.");
                return;
            }

            // Activar el mesh renderer del fragmento correspondiente
            var matchingPiece = framePieces.Find(p => p.name == itemId);
            if (matchingPiece != null && matchingPiece.TryGetComponent<MeshRenderer>(out var renderer))
            {
                renderer.enabled = true;
            }
            else
            {
                Debug.LogWarning($"No se encontró la pieza visual para '{itemId}' en el cuadro.");
            }

            insertedItems.Add(itemId);
            InventoryManager.Instance.RemoveItem(itemId); // <-- Nueva función en InventoryManager

            Debug.Log($"Fragmento '{itemId}' colocado ({insertedItems.Count}/{requiredItemIDs.Count}).");

            if (insertedItems.Count >= requiredItemIDs.Count)
            {
                isSolved = true;
                LoopManager.Instance.SetConditionMet(true);
                Debug.Log($"Puzzle '{puzzleID}' completado.");
                InventoryManager.Instance.OnItemUsedExternally = null;
            }
            InventoryCarouselUI.Instance.Close();
        }
    }
}