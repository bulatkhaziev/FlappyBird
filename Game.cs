// Game.cs. Написал: Хазиев Булат
using System;
using System.Threading;
using System.Diagnostics;

namespace FlappyBird
{
    class Game
    {
        // Задержка перед обновлением цикла
        public static int UpdateDelay = 100;
        
        public static void Init()
        {
            Bird bird = new Bird();
            bird.ReadHighScore();

            // Используем поток для отлова прыжка птицы.
            Thread birdThread = new Thread(bird.ThreadJumping);
            birdThread.Start();

            // Используем поток для создания стен.
            Wall wall = new Wall();
            Thread wallsThread = new Thread(wall.Add);
            wallsThread.Start();
            
            while (true)
            {
                // Если птица ударилась о границы карты или о препятствие.
                if (bird.IsCrashed(wall.Walls))
                {
                    // Завершаем потоки и выходим из цикла
                    wallsThread.Abort();
                    birdThread.Abort();
                    break;
                }
                // Обновляем координаты птицы, преград и отрисовываем карту заново.
                bird.Update();
                
                Map.Print(bird);
                wall.Print(wallsThread);

                Thread.Sleep(UpdateDelay);
            }
            // Завершаем игру
            Destruct(bird);
        }

        public static void Destruct(Bird bird)
        {
            bird.SaveHighScore();

            Console.Clear();
            Console.WriteLine("Score: {0}. Do you want to play again? Y/N", bird.CurrentScore);

            string input = Console.ReadLine();

            if (input.Length == 0)
                return;

            char answer = input[0];

            if(char.Equals(char.ToLower(answer), 'y'))
            {
                Console.Clear();
                Init();
            }
        }
    }
}
