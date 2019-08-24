using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public class OnTriggerGameEventRaiser : OnPhysicsGameEventRaiser
    {
        private void OnTriggerEnter(Collider other)
        {
            OnEnter(other.tag, other.gameObject.layer);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter(other.tag, other.gameObject.layer);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit(other.tag, other.gameObject.layer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnExit(other.tag, other.gameObject.layer);
        }

        private void OnTriggerStay(Collider other)
        {
            OnStay(other.tag, other.gameObject.layer);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnStay(other.tag, other.gameObject.layer);
        }
    }
}
