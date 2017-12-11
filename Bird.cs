using System;
using System.Collections.Generic;
using System.IO;

namespace FlappyBird
{
    class Bird
    {
        // Начальные координаты птицы
        public static int[] StartCoords = { 10 + Map.BordersOffset[0], Map.BordersOffset[1] + Map.BordersSize[1] / 3 };
        // Модель птицы
        public static string Model = "X";
        // Высота прыжка птицы в пикселях
        public static int JumpHeight = 4; // bird jump height in pixels

        public int[] CurrentCoords = { StartCoords[0], StartCoords[1] };
        public int CurrentScore = 0;
        public int HighScore;

        public bool IsJumping = false;
        public int CurrentJumpInteration = 0;

        // Главный метод
        public void Update()
        {
            UpdateScore();
            UpdateCoords();
        }

        // Обновление текущего счета
        public void UpdateScore()
        {
            this.CurrentScore += 1;

            if (this.CurrentScore > this.HighScore)
                this.HighScore = this.CurrentScore;
        }

        // Прыжок птицы
        public void ThreadJumping()
        {
            ConsoleKeyInfo KeyInfo;

            while(true)
            {
                KeyInfo = Console.ReadKey();
                // Если нажат пробел, то меняем текущее состояние птицы на прыжок
                if (KeyInfo.Key == ConsoleKey.Spacebar && !IsJumping)
                    IsJumping = true;
            }
        }

        // Проверка на касание стены
        public bool IsTouchedWall(List<int[]> walls)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (CurrentCoords[0] == walls[i][0] && (CurrentCoords[1] >= walls[i][1] && CurrentCoords[1] <= walls[i][1] + walls[i][2]))
                    return true;
            }
            return false;
        }

        // Проверка на касание границ карты
        public bool IsTouchedBorder()
        {
            return CurrentCoords[1] <= Map.BordersOffset[1]
                || CurrentCoords[1] >= Map.BordersOffset[1] + Map.BordersSize[1] - 2;
        }

        public bool IsCrashed(List<int[]> walls)
        {
            return IsTouchedBorder() || IsTouchedWall(walls);
        }

        // Обновление координат птицы
        public void UpdateCoords()
        {
            if (IsJumping)
            {
                CurrentCoords[1] -= 1;
                CurrentJumpInteration += 1;

                if (CurrentJumpInteration == JumpHeight)
                {
                    IsJumping = false;
                    CurrentJumpInteration = 0;
                }
            }
            else
            {
                CurrentCoords[1] += 1;
            }
        }
        
        // Чтение лучшего счета с файла
        public void ReadHighScore()
        {
            string text = File.ReadAllText("highscore.txt");
            HighScore = text.Length > 0 ? int.Parse(text) : 0;
        }

        // Запись лучшего счета в файл
        public void SaveHighScore()
        {
            File.WriteAllText("highscore.txt", HighScore.ToString());
        }
    }
}
