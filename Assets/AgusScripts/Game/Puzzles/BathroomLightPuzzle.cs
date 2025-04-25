using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Environment.Lights;

namespace Game.Puzzles
{
    public class BathroomLightPuzzle : MonoBehaviour, IPuzzle
    {
        [SerializeField] private List<HauntedLight> puzzleLights;
        //[SerializeField] private DrawingClue clue;
        //[SerializeField] private PhoneController phone;
        //[SerializeField] private DoorController exitDoor;

        private bool _isActive = false;
        private bool _isSolved = false;
        private Dictionary<string, int> _lightFlashCounts;

        public bool IsSolved => _isSolved;

        public void Activate()
        {
            _isActive = true;
            _isSolved = false;

            GenerateFlashCounts();
            StartCoroutine(FlashSequenceLoop());
            //phone.OnCodeSubmitted += OnCodeSubmitted;
        }

        public void Deactivate()
        {
            _isActive = false;
            StopAllCoroutines();
            //phone.OnCodeSubmitted -= OnCodeSubmitted;
        }

        private void GenerateFlashCounts()
        {
            _lightFlashCounts = new Dictionary<string, int>();
            foreach (var light in puzzleLights)
            {
                int flashCount = Random.Range(3, 8);
                _lightFlashCounts[light.LightId] = flashCount;
                light.SetLocked(true);
            }
        }

        private IEnumerator FlashSequenceLoop()
        {
            while (_isActive && !_isSolved)
            {
                foreach (var light in puzzleLights)
                {
                    string id = light.LightId;
                    int flashCount = _lightFlashCounts[id];
                    light.FlashPattern(flashCount, 1f);
                }

                yield return new WaitForSeconds(10f);
            }
        }
        /*
        private void OnCodeSubmitted(List<int> submittedCode)
        {
            if (!_isActive || _isSolved) return;

            var correctOrder = clue.GetOrderedRoomIds();
            var expectedCode = new List<int>();

            foreach (var roomId in correctOrder)
            {
                if (_lightFlashCounts.TryGetValue(roomId, out int count))
                    expectedCode.Add(count);
                else
                {
                    Debug.LogWarning($"[BathroomLightPuzzle] No se encontró el foco para la habitación '{roomId}'");
                    return;
                }
            }

            if (submittedCode.Count != expectedCode.Count) return;

            for (int i = 0; i < submittedCode.Count; i++)
            {
                if (submittedCode[i] != expectedCode[i])
                {
                    Debug.Log("[BathroomLightPuzzle] Código incorrecto.");
                    return;
                }
            }

            PuzzleSolved();
        }
        */
        private void PuzzleSolved()
        {
            _isSolved = true;
            _isActive = false;

            foreach (var light in puzzleLights)
            {
                light.SetLocked(false);
                light.TurnOff();
            }

            //exitDoor.Unlock();
            Debug.Log("[BathroomLightPuzzle] Puzzle resuelto. Puerta desbloqueada.");
        }
    }
}