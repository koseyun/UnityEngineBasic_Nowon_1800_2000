using System;

namespace Example02_Array2D
{
    internal class Program
    {
        //static int[,,] cube = new int[3, 4, 5];

        //                   [행 개수, 열 개수]
        static int[,] map = new int[5, 5]
        {
            { 0, 0, 0, 0, 1 },
            { 0, 1, 1, 1, 1 },
            { 0, 0, 0, 1, 1 },
            { 1, 1, 0, 0, 0 },
            { 0, 1, 1, 0, 0 }
        };

        static void Main(string[] args)
        {
            /*int count = 0;
            for (int i = 0; i < cube.GetLength(0); i++)
            {
                for (int j = 0; j < cube.GetLength(1); j++)
                {
                    for (int k = 0; k < cube.GetLength(2); k++)
                    {
                        cube[k, j, i] = count;
                        Console.WriteLine(cube[i, j, k]);
                        count++;
                    }
                    count += 100;
                }
                count += 10000;
            }*/

            Player player = new Player(0, 3);
            map[0, 3] = 2;

            while (true)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "L":
                        player.MoveLeft(map);
                        break;
                    case "R":
                        player.MoveRight(map);
                        break;
                    case "D":
                        player.MoveDown(map);
                        break;
                    case "U":
                        player.MoveUp(map);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }

                DisplayerMap();
            }
        }

        static void DisplayerMap()
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                for (int i = 0; i < map.GetLength(1); i++)
                {
                    if (map[j, i] == 0)
                        Console.Write("□");
                    else if (map[j, i] == 1)
                        Console.Write("■");
                    else if (map[j, i] == 2)
                        Console.Write("▣");
                }
                Console.WriteLine();
            }
        }

        class Player
        {            
            private int _x;
            private int _y;

            public Player(int y, int x)
            {
                _x = x;
                _y = y;
            }

            public void MoveLeft(int[,] map)
            {
                // 맵 범위를 넘어가는지 체크
                if (_x - 1 < 0)
                    Console.WriteLine($"플레이어를 왼쪽으로 이동시킬 수 없습니다. (경계초과) 현재위치 : {_x}, {_y}");
                else if (map[_y, _x - 1] != 0)
                    Console.WriteLine($"플레이어를 왼쪽으로 이동시킬 수 없습니다. (길이없음) 현재위치 : {_x}, {_y}");
                else
                {
                    map[_y, _x--] = 0;
                    map[_y, _x] = 2;
                    Console.WriteLine($"플레이어 왼쪽으로 한칸 이동함. 현재위치 : {_x}, {_y}");
                }
            }
            public void MoveRight(int[,] map)
            {
                // 맵 범위를 넘어가는지 체크
                if (_x + 1 > map.GetLength(1) - 1) // ※ map 길이 직접 설정X, GetLength 이용
                    Console.WriteLine($"플레이어를 오른쪽으로 이동시킬 수 없습니다. (경계초과) 현재위치 : {_x}, {_y}");
                else if (map[_y, _x + 1] != 0)
                    Console.WriteLine($"플레이어를 오른쪽으로 이동시킬 수 없습니다. (길이없음) 현재위치 : {_x}, {_y}");
                else
                {
                    map[_y, _x++] = 0;
                    map[_y, _x] = 2;
                    Console.WriteLine($"플레이어 오른쪽으로 한칸 이동함. 현재위치 : {_x}, {_y}");
                }
            }
            public void MoveDown(int[,] map)
            {
                // 맵 범위를 넘어가는지 체크
                if (_y + 1 > map.GetLength(0) - 1)
                    Console.WriteLine($"플레이어를 아래쪽으로 이동시킬 수 없습니다. (경계초과) 현재위치 : {_x}, {_y}");
                else if (map[_y + 1, _x] != 0)
                    Console.WriteLine($"플레이어를 아래쪽으로 이동시킬 수 없습니다. (길이없음) 현재위치 : {_x}, {_y}");
                else
                {
                    map[_y++, _x] = 0;
                    map[_y, _x] = 2;
                    Console.WriteLine($"플레이어 아래쪽으로 한칸 이동함. 현재위치 : {_x}, {_y}");
                }
            }
            public void MoveUp(int[,] map)
            {
                // 맵 범위를 넘어가는지 체크
                if (_y - 1 < map.GetLength(0) - 1)
                    Console.WriteLine($"플레이어를 위쪽으로 이동시킬 수 없습니다. (경계초과) 현재위치 : {_x}, {_y}");
                else if (map[_y - 1, _x] != 0)
                    Console.WriteLine($"플레이어를 위쪽으로 이동시킬 수 없습니다. (길이없음) 현재위치 : {_x}, {_y}");
                else
                {
                    map[_y--, _x] = 0;
                    map[_y, _x] = 2;
                    Console.WriteLine($"플레이어 위쪽으로 한칸 이동함. 현재위치 : {_x}, {_y}");
                }
            }
        }
    }
}
