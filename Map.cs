using System;

namespace FlappyBird
{
    class Map
    {
        // map configurations
        public static int[] BordersSize = { 70, 20 };
        public static int[] BordersOffset = { 1, 4 };
        public static int[] UserBarOffset = { 5, 2 };

        public static void Print(Bird bird)
        {
            Console.CursorVisible = false;
            PrintBorders();
            PrintUserBar(bird);
            PrintBird(bird);
        }

        public static void PrintBorders()
        {
            for(int X = 0; X < BordersSize[0]; X++)
            {
                for(int Y = 0; Y < BordersSize[1]; Y++)
                {
                    Console.SetCursorPosition(BordersOffset[0] + X, BordersOffset[1] + Y);

                    if (X == 0 || X == BordersSize[0] - 1 || Y == 0 || Y == BordersSize[1] - 1)
                        Console.WriteLine("~");
                    else
                        Console.WriteLine(" ");
                }
            }
        }

        public static void PrintUserBar(Bird bird)
        {
            Console.SetCursorPosition(UserBarOffset[0], UserBarOffset[1]);

            Console.WriteLine("SCORE: {0} | HIGH SCORE: {1}", bird.CurrentScore, bird.HighScore);
        }

        public static void PrintBird(Bird bird)
        {
            Console.SetCursorPosition(bird.CurrentCoords[0], bird.CurrentCoords[1]);
            
            Console.WriteLine(Bird.Model);
        }
    }
}
