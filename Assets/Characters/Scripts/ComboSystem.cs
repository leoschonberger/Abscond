using UnityEngine;

namespace Characters.Scripts
{
    public class ComboSystem : MonoBehaviour
    {
        public TextMesh comboCounter;

        public BasicAttack attack;

        private int currentComboLength = 0;

        public void incrementComboCounter()
        {
            currentComboLength++;
            if (currentComboLength > 2)
                attack.attackLength *= 0.8f;

            comboCounter.text = "Combo: " + currentComboLength + "x";
        }

        public void resetComboCounter()
        {
            currentComboLength = 0;
            attack.attackLength = attack.originalAttackLength;
            
            comboCounter.text = "Combo: " + currentComboLength + "x";
        }
        
    }
}