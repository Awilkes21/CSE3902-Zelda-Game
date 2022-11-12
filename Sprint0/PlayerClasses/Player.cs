using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.PlayerClasses.Abilities;

using sprint0.RoomClasses;
using sprint0.Sound;
using System;
using sprint0.Classes;

namespace sprint0.PlayerClasses; 

public class Player : IPlayer {

    private Vector2 initPosition;

    public PlayerInventory PlayerInventory;
    public ICollidable.ObjectType type { get; set; }
    public Vector2 Position { get; set; }
    public int Health { get; private set; }
    public int ScaleFactor { get; set; }
    public Game1 Game { get; set; }
    public bool IsMultiSprite { get; set; }
    public IPlayerState PlayerState { get; set; }
    public PlayerAbilityManager AbilityManager { get; protected set; }
    public int Damage { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 InitVelocity { get; set; }
    
    public Player(Game1 game) {
        Game = game;
        Position = new Vector2(200, 200);
        initPosition = Position;
        Reset();
    }
    
    public int GetHealth()
    {
        return Health;
    }
    
    public Rectangle GetHitBox()
    {
        return PlayerState.GetHitBox();
    }

    public void Collide(ICollidable obj, ICollidable.Edge edge)
    {
        if (obj.type == ICollidable.ObjectType.Wall || obj.type == ICollidable.ObjectType.Tile)
        {
            switch (edge)
            {
                case ICollidable.Edge.Top:
                    Position += new Vector2(0, -3);
                    break;
                case ICollidable.Edge.Right:
                    Position += new Vector2(-3, 0);
                    break;
                case ICollidable.Edge.Left:
                    Position += new Vector2(3, 0);
                    break;
                case ICollidable.Edge.Bottom:
                    Position += new Vector2(0, 3);
                    break;
            }
        }
        PlayerState.Collide(obj, edge);
    }

    public void Draw(SpriteBatch spriteBatch) { 
        PlayerState.Draw(spriteBatch, Color.White);
    }

    public void Update(GameTime gameTime, Game1 game) {
        PlayerState.Update();
        AbilityManager.Update(gameTime, game);
    }

    public void Reset()
    {
        Position = initPosition;
        PlayerState = new PlayerFacingUpState(this);
        AbilityManager = new PlayerAbilityManager(this);
        PlayerInventory = new PlayerInventory();

        Health = 6;
        ScaleFactor = 4;
        Damage = 0;
        type = ICollidable.ObjectType.Player;
        Velocity = Vector2.Zero;
    }

    public void TakeDamage(int damage) {
        Health -= damage;
        Game.Player = new DamagedPlayer(this, Game);
        CollisionManager.Collidables.Add(Game.Player);
        CollisionManager.Collidables.Remove(this);
        SoundManager.Manager.linkDamageSound().Play();
    }

    public void MoveUp() {
            PlayerState.MoveUp();
    }

    public void MoveDown() {
            PlayerState.MoveDown();
    }

    public void MoveLeft() {
            PlayerState.MoveLeft();
    }

    public void MoveRight() {
            PlayerState.MoveRight();
    }

    public void SwordAttack() {
        PlayerState.SwordAttack();
    }

    public virtual void UseAbility(AbilityTypes abilityType) {
        if (PlayerInventory.AbilityCounts[abilityType] > 0) {
            if (abilityType == AbilityTypes.Bomb) {
                PlayerInventory.AbilityCounts[AbilityTypes.Bomb] -= 1;
            }

            PlayerState.UseAbility(abilityType);
        }
    }

    public int GetHealth()
    {
        return this.Health;
    }

    public PlayerInventory GetInventory()
    {
        return this.PlayerInventory;
    }

}