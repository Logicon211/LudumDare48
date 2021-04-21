using UnityEngine;

namespace Enemy.Interface
{
    public interface IEnemy
    {
        // Move the enemy
        void Move(float tarX, float tarY);

        // Stop Moving the enemy
        void StopMove();

        // Rotate the enemy
        void Rotate(Vector3 direction);

        // Attack shit towards target
        void Attack(float attackSpeed);

        // Stop Moving the enemy
        void StopAttack();
    }
}
