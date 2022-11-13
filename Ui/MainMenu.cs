namespace SpaceInvaders.UI
{
    class MainMenu
    {
        private const string StartFile = @"Assets/startMenuFile.txt";
        public static State DrawMenu()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), StartFile);
            string menuArt = File.ReadAllText(filePath);
            Console.Clear();
            Console.WriteLine(menuArt);

            while(true)
            {
                var cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        return State.Play;
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.D0:
                        return State.Exit;
                    default:
                        break;
                }
            }
        }
    }
}