using System.Collections.Generic;
using UnityEngine;

namespace Game.Puzzles
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance { get; private set; }

        private Dictionary<string, IPuzzle> _puzzles = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void RegisterPuzzle(string id, IPuzzle puzzle)
        {
            if (!_puzzles.ContainsKey(id))
                _puzzles.Add(id, puzzle);
        }

        public void ActivatePuzzle(string id)
        {
            if (_puzzles.TryGetValue(id, out var puzzle))
                puzzle.Activate();
        }

        public void DeactivatePuzzle(string id)
        {
            if (_puzzles.TryGetValue(id, out var puzzle))
                puzzle.Deactivate();
        }

        public bool IsPuzzleSolved(string id)
        {
            return _puzzles.TryGetValue(id, out var puzzle) && puzzle.IsSolved;
        }

        public void DeactivateAll()
        {
            foreach (var puzzle in _puzzles.Values)
                puzzle.Deactivate();
        }
    }
}