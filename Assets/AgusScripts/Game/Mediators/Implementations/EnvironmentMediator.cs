using Game.Mediators.Interfaces;
using UnityEditor.EditorTools;
using UnityEngine;
using Game.Environment.Lights;

namespace Game.Mediators.Implementations
{
    /// <summary>
    /// Mediator responsible for routing gameplay-driven environment interactions
    /// to the appropriate environmental subsystems.
    /// </summary>
    public class EnvironmentMediator : MonoBehaviour, IEnvironmentMediator
    {
        [Header("Subsystem References")]
        [SerializeField] private LightingManager lightingManager;
        [SerializeField] private GlassManager glassManager;
        [SerializeField] private DoorManager doorManager;
        [SerializeField] private AmbientAudioManager audioManager;

        // -----------------------------
        // ILightEffectService
        // -----------------------------
        public void TriggerMotherEntrance(Transform motherPosition, float motherRadius)
        {
            lightingManager?.BeginDynamicFlicker(motherPosition, motherRadius); // o una luz en particular
            //ambientAudioManager?.PlayJumpscareSting();
            //doorManager?.SlamRandomDoor(); // ¡boom!
            //glassManager?.ShatterNear(entryPoint);
        }

        public void TriggerMotherOut()
        {
            lightingManager?.StopDynamicFlicker();
        }

        public void StartFlickerNear(Vector3 origin, float radius)
        {
            //lightingManager?.FlickerLightsNear(origin, radius);
        }

        public void ResetAllFlicker()
        {
            // lightingManager?.ResetAll();
        }

        // -----------------------------
        // IGlassInteractionService
        // -----------------------------

        public void ShatterGlass(string id)
        {
            glassManager?.Shatter(id);
        }

        // -----------------------------
        // IDoorControlService
        // -----------------------------

        public void OpenDoor(string doorId)
        {
            doorManager?.Open(doorId);
        }

        public void CloseDoor(string doorId)
        {
            doorManager?.Close(doorId);
        }

        public void ToggleDoor(string doorId)
        {
            doorManager?.Toggle(doorId);
        }

        // -----------------------------
        // IAmbientAudioService
        // -----------------------------

        public void ToggleRadio(string radioId, bool on)
        {
            audioManager?.SetRadioState(radioId, on);
        }

        public void PlayAmbientSound(string clipName)
        {
            audioManager?.Play(clipName);
        }

        public void StopAmbientSound(string clipName)
        {
            audioManager?.Stop(clipName);
        }
    }
}