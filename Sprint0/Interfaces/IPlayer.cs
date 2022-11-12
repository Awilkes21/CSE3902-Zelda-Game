using Microsoft.Xna.Framework;
using sprint0.PlayerClasses.Abilities;

namespace sprint0.Interfaces; 

public interface IPlayer : ICollidable
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 InitVelocity { get; set; }

    public int GetHealth();
    public void TakeDamage(int damage);
    public void MoveUp();
    public void MoveDown();
    public void MoveLeft();
    public void MoveRight();
    public void SwordAttack();
    public void UseAbility(AbilityTypes abilityType);
}