using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace Game.Environment.Lights
{
    public class LightingManager : MonoBehaviour
    {
        public static LightingManager Instance { get; private set; }

        private List<HauntedLight> _allLights = new();
        private List<HauntedLight> _currentlyFlickering = new();
        private bool _powerOn = true;
        private Coroutine _flickerFollowRoutine;

        public bool IsPowerOn => _powerOn;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _allLights = FindObjectsOfType<HauntedLight>(true).ToList();
        }

        public void RegisterLight(HauntedLight light)
        {
            if (!_allLights.Contains(light))
                _allLights.Add(light);
        }

        public void CutPower()
        {
            _powerOn = false;
            foreach (var light in _allLights)
                light.OnPowerCut();
        }

        public void RestorePower()
        {
            _powerOn = true;
            foreach (var light in _allLights)
                light.OnPowerRestored();
        }

        public void TurnOffGroup(LightGroup group)
        {
            foreach (var light in _allLights.Where(l => l.Group == group))
                light.TurnOff();
        }

        public void TurnOnGroup(LightGroup group)
        {
            foreach (var light in _allLights.Where(l => l.Group == group))
                light.TurnOn();
        }

        public void StartFlickerNear(Vector3 origin, float radius)
        {
            foreach (var light in _allLights)
            {
                if (Vector3.Distance(light.transform.position, origin) <= radius)
                    light.StartFlicker();
            }
        }

        public void StopAllFlicker()
        {
            foreach (var light in _allLights)
                light.StopFlicker();
        }

        public void TurnOffLightById(string id)
        {
            var light = _allLights.FirstOrDefault(l => l.LightId == id);
            light?.TurnOff();
        }

        public void TurnOnLightById(string id)
        {
            var light = _allLights.FirstOrDefault(l => l.LightId == id);
            light?.TurnOn();
        }

        public HauntedLight GetLightById(string id)
        {
            return _allLights.FirstOrDefault(l => l.LightId == id);
        }

        public List<HauntedLight> GetLightsByGroup(LightGroup group)
        {
            return _allLights.Where(l => l.Group == group).ToList();
        }

        public void BeginDynamicFlicker(Transform target, float radius)
        {
            if (_flickerFollowRoutine != null)
                StopDynamicFlicker();

            _flickerFollowRoutine = StartCoroutine(FlickerFollowLoop(target, radius));
        }

        public void StopDynamicFlicker()
        {
            Debug.Log($"Stopping dynamic flicker... {_flickerFollowRoutine}");
            if (_flickerFollowRoutine != null)
                StopCoroutine(_flickerFollowRoutine);

            foreach (var light in _currentlyFlickering)
                light.StopFlicker();

            _currentlyFlickering.Clear();
        }

        private IEnumerator FlickerFollowLoop(Transform target, float radius)
        {
            while (true)
            {
                _currentlyFlickering.Clear();

                var lights = GetLightsNear(target.position, radius);

                foreach (var light in lights)
                {
                    light.StartFlicker(); // Su propia rutina interna, irregular
                    _currentlyFlickering.Add(light);
                }

                yield return new WaitForSeconds(1f); // Check cada 1s. Podés ajustar
            }
        }

        public List<HauntedLight> GetLightsNear(Vector3 position, float radius)
        {
            return _allLights
                .Where(light =>
                    light != null &&
                    !light.IsBroken &&
                    Vector3.Distance(light.transform.position, position) <= radius)
                .ToList();
        }

        private void OnDrawGizmosSelected()
        {
            if (_currentlyFlickering == null) return;

            Gizmos.color = Color.yellow;
            foreach (var light in _currentlyFlickering)
            {
                if (light != null)
                    Gizmos.DrawWireSphere(light.transform.position, 0.5f);
            }
        }
    }
}