using SpaceInvaders.Engine;
using SpaceInvaders.Game;
using SpaceInvaders.UI;

namespace SpaceInvaders
{

    class Program
    {
        static void Main(string[] args)
        {
            var state = State.MainMenu;
            // Clear Console
            Console.Clear();
            Console.CursorVisible = false;
            
            while (true)
            {
                switch (state)
                {
                    case State.MainMenu:
                        state = MainMenu.DrawMenu();
                        break;
                    case State.Exit:
                        Exit.DrawExit();
                        return;
                    case State.Play:
                        Application game = new Application();
                        Level level = Level.Instances;
                        LevelUi levelUi = new LevelUi();
                        game.Run();
                        state = level.FinalState;
                        break;
                    case State.Victory:
                        GameOver.DrawVictory();
                        state = State.MainMenu;
                        break;
                    case State.Defeat:
                        GameOver.DrawDefeat();
                        state = State.MainMenu;
                        break;
                    default:
                        break;
                }
            }


        }
    }
    enum State
    {
        MainMenu,
        Exit,
        Play,
        Victory,
        Defeat,
    }
}