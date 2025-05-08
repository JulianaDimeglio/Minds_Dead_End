using UnityEngine;

namespace Game.Mediators.Interfaces
{
    public interface ILightEffectService
    {
        void StartFlickerNear(Vector3 origin, float radius);
        void ResetAllFlicker();
    }
}