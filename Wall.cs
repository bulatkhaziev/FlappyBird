using System;
using System.Threading;
using System.Collections.Generic;

namespace FlappyBird
{
    class Wall
    {
        public List<int[]> Walls = new List<int[]>(); // Коллекция стен
        public static int WallPrintDelay = 3000; // Задержка перед добавлением новой стены
        
        public void Print(Thread thread)
        {
            // Проверка состояния потока. Поток не должен быть запущен, иначе будет эксепшн
            if (thread.ThreadState != ThreadState.WaitSleepJoin)
                return;

            // Обновление координат
            Update();
            // Отрисовка стен
            foreach (var wall in Walls)
            {
                for(int i = 0; i < wall[2]; i++)
                {
                    Console.SetCursorPosition(wall[0], wall[1] + i);
                    Console.WriteLine("#");
                }
            }
        }

        // Отрисовка конкретной стены
        public void Print(int index)
        {
            for (int i = 0; i < Walls[index][2]; i++)
            {
                Console.SetCursorPosition(Walls[index][0], Walls[index][1] + i);
                Console.WriteLine("#");
            }
        }

        // Обновление координат стен
        public void Update()
        {
            for(int i = 0; i < Walls.Count; i++)
            {
                this.Walls[i][0] -= 1;

                if (this.Walls[i][0] <= Map.BordersOffset[0])
                    Walls.Remove(this.Walls[i]);
            }
        }
       
        // Добавление стен
        public void Add()
        {
            var random = new Random();

            while(true)
            {
                for(int i = 1; i < 2; i++)
                {
                    // Высота стены
                    int WallHeight = random.Next(Map.BordersSize[1] / 3 * 2);
                    
                    // Координаты
                    int X = Map.BordersOffset[0] + Map.BordersSize[0] - 2;
                    int Y = Map.BordersOffset[1] + Map.BordersSize[1] - WallHeight - 1;
                    
                    // Добавление в коллекцию
                    Walls.Add(new[] { X, Y, WallHeight });

                    // Отрисовка
                    Print(Walls.Count - 1);
                }
                Thread.Sleep(WallPrintDelay);
            }
        }
    }
}
