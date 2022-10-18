using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.PlayerClasses.Abilities;
using sprint0.Sprites;
using System;

namespace sprint0.PlayerClasses; 

public class Player : ICollidable {
    
    public int ScaleFactor;

    private Vector2 initPosition;
    public Vector2 Position { get; set; }
    public int Health { get; set; }
    public Game1 Game { get; set; }
    public IPlayerState PlayerState { get; set; }
    public PlayerAbilityManager AbilityManager { get; protected set; }
    public int Damage { get; set; }

    public Player(Game1 game) {
        Game = game;
        PlayerState = new PlayerFacingUpState(this);
        AbilityManager = new PlayerAbilityManager(this);
        Health = 6;
        ScaleFactor = 4;
        Position = new Vector2(150);
        initPosition = Position;
        Damage = 0;
    }

    public Type GetObjectType()
    {
        return this.GetType().BaseType;
    }
    public Rectangle GetHitBox()
    {
        return PlayerState.GetHitBox();
    }

    public void Collide(ICollidable obj, ICollidable.Edge edge)
    {
        switch (edge)
        {
            case ICollidable.Edge.Top:
                Position += new Vector2(0, -IPlayerState.playerSpeed);
                break;
            case ICollidable.Edge.Right:
                Position += new Vector2(-IPlayerState.playerSpeed, 0);
                break;
            case ICollidable.Edge.Left:
                Position += new Vector2(IPlayerState.playerSpeed, 0);
                break;
            case ICollidable.Edge.Bottom:
                Position += new Vector2(0, IPlayerState.playerSpeed);
                break;
            default:
                break;
        }
        PlayerState.Collide(obj, edge);
    }

    public virtual void Draw(SpriteBatch spriteBatch) { 
        PlayerState.Draw(spriteBatch);
        AbilityManager.Draw(spriteBatch);
    }

    public virtual void Update(GameTime gameTime, Game1 game) {
        PlayerState.Update();
        AbilityManager.Update(gameTime, game);
    }

    public virtual void Reset(Game1 game)
    {
        Position = initPosition;
        PlayerState = new PlayerFacingUpState(this);
    }

    public virtual void TakeDamage(int damage) {
        Health -= damage;
        Game.Player = new DamagedPlayer(this, Game);
    }

    public virtual void MoveUp() {
        PlayerState.MoveUp();
    }

    public virtual void MoveDown() {
        PlayerState.MoveDown();
    }

    public virtual void MoveLeft() {
        PlayerState.MoveLeft();
    }

    public virtual void MoveRight() {
        PlayerState.MoveRight();
    }

    public virtual void SwordAttack() {
        PlayerState.SwordAttack();
    }

    public virtual void UseAbility(AbilityTypes abilityType) {
        PlayerState.UseAbility(abilityType);
    }

}