using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    public class OnCollisionGameEventRaiser : OnPhysicsGameEventRaiser
    {
        private void OnCollisionEnter(Collision other)
        {
            OnEnter(other.gameObject.tag, other.gameObject.layer);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnEnter(other.gameObject.tag, other.gameObject.layer);
        }

        private void OnCollisionExit(Collision other)
        {
            OnExit(other.gameObject.tag, other.gameObject.layer);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnExit(other.gameObject.tag, other.gameObject.layer);
        }

        private void OnCollisionStay(Collision other)
        {
            OnStay(other.gameObject.tag, other.gameObject.layer);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnStay(other.gameObject.tag, other.gameObject.layer);
        }
    }
}