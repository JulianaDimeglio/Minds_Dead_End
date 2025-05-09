using UnityEngine;

namespace Game.Mediators.Interfaces
{
    /// <summary>
    /// Interface for triggering environmental effects from gameplay systems.
    /// </summary>
    public interface IEnvironmentMediator
    {
        /// <summary>
        /// Starts a flickering effect on lights near a given position.
        /// </summary>
        /// <param name="origin">The central position to search for flicker zones.</param>
        /// <param name="radius">The radius in which to affect lights.</param>
        void StartFlickerNear(Vector3 origin, float radius);

        /// <summary>
        /// Resets all flickering lights to their normal state.
        /// </summary>
        void ResetAllFlicker();

        /// <summary>
        /// Shatters a specific mirror or window by identifier.
        /// </summary>
        /// <param name="id">The identifier of the environmental object to shatter.</param>
        void ShatterGlass(string id);
    }
}