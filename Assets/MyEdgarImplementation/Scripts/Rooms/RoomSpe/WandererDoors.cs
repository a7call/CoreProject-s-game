using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Edgar.Unity.Examples
{
    public class WandererDoors : InteractableBase
    {
        public DoorState State;

        /// <summary>
        /// Show text when the interaction begins (player is close to the door).
        /// </summary>
        public override void BeginInteract()
        {
            switch (State)
            {
                case DoorState.Unlocked:
                    ShowText("This door is now unlocked. Press E to open.");
                    break;

                case DoorState.Locked:
                    ShowText("This door is locked. It can be only unlocked from the other way.");
                    break;
            }
        }

        /// <summary>
        /// Check for key press when the player is near.
        /// </summary>
        public override void Interact()
        {
            if (State == DoorState.Unlocked && Input.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Hide text when the interaction ends (player gets further from doors).
        /// </summary>
        public override void EndInteract()
        {
            HideText();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && State == DoorState.Unlocked)
            {
                gameObject.SetActive(false);
            }
        }

        public enum DoorState
        {
            Unlocked, // The door is unlocked and can be opened
            Locked,  // The door is locked and must be unlocked before opening
            EnemyLocked // The door is locked when there are enemies in the room and disappears automatically when they are defeated
        }
    }

}
