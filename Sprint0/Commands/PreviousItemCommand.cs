using sprint0.Interfaces;
using sprint0.PlayerClasses;
namespace sprint0.Commands; 

public class PreviousItemCommand : ICommand {

    public void Execute(Game1 game) {
        game.PreviousItem();
    }
}