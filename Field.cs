using System;
using System.Collections.Generic;

namespace Fillwords
{
    class Field
    {
        Random rnd = new Random();
        
        public List<string> wordsList = new List<string>();      //лист слов на поле
        public List<List<int>> wordPos = new List<List<int>>();  //лист координат каждой буквы каждого слова

        char[] lettersList = {'А', 'Б', 'В', 'Г', 'Д', 'Е', 'И' };  //лист букв для заполнения пробелов

        public void CreateNewField(int xSize, int ySize, WordsSet words)
        {
            char[,] field = new char[xSize, ySize];

            //создание поля свободных ячеек
            bool[,] preField = new bool[xSize + 2, ySize + 2];  
            for(int y = 0; y <= ySize + 1; y++)
                for(int x = 0; x <= xSize + 1; x++)
                    if (x == xSize + 1 || y == 0 || x == 0 || y == ySize + 1) preField[x, y] = false;
                    else                                                      preField[x, y] = true;

            //массив закраски поля словами
            int cellNum = xSize * ySize;
            do
            {
                //нахаждение свободной ячейки
                int x, y;   
                do
                {
                    x = rnd.Next(xSize) + 1;
                    y = rnd.Next(ySize) + 1;
                } while (!preField[x, y]);

                //создание слова начиная с этой ячейки
                int dir = FindDirection(x, y, preField);
                if (dir != 0)
                {
                    int lenght = 0;
                    wordPos.Add(new List<int>());
                    do
                    {
                        preField[x, y] = false;
                        cellNum--;
                        wordPos[wordsList.Count].Add((y - 1) * xSize + (x - 1));
                        lenght++;

                        int localX = x + (-(dir - 2) % 2);
                        int localY = y + ((dir - 3) % 2);
                        if (!preField[localX, localY])
                        {
                            dir = FindDirection(x, y, preField);
                            if (dir == 0) break;
                            localX = x + (-(dir - 2) % 2);
                            localY = y + (dir - 3) % 2;
                        }
                        x = localX;
                        y = localY;

                        if (lenght == words.setWords.Count - 1 || (lenght >= 4 && rnd.Next(6) == 0)) break;
                    } while (true);

                    if (words.setWords[lenght].Count > 0)
                    {
                        int randomNum = rnd.Next(words.setWords[lenght].Count);
                        wordsList.Add(words.setWords[lenght][randomNum]);
                        words.setWords[lenght].Remove(wordsList[wordsList.Count - 1]);
                        if (words.setWords.Count == lenght && words.setWords[lenght].Count == 0)
                            words.setWords.RemoveAt(words.setWords.Count - 1);
                    }
                    else
                    {
                        wordsList.Add(new string('\0', lenght));
                    }
                }
                else
                {
                    preField[x, y] = false;
                    cellNum--;
                    continue;
                }

            } while (cellNum > 0);

            //заполнение основного поля буквами
            for(int i = 0; i < wordsList.Count; i++)
                for (int ii = 0; ii < wordsList[i].Length; ii++)
                {
                    int x = wordPos[i][ii] % xSize;
                    int y = wordPos[i][ii] / xSize;
                    field[x, y] = wordsList[i][ii];
                }

            for (int y = 0; y < ySize; y++)
                for (int x = 0; x < xSize; x++)
                    if (field[x, y] == '\0') field[x, y] = lettersList[rnd.Next(lettersList.Length)];           
        }

        int FindDirection(int x, int y, bool[,] field)
        {
            List<int> dirList = new List<int>() { 1, 2, 3, 4 };
            int output;

            if (!field[x + 1, y    ]) dirList.Remove(1);
            if (!field[x    , y - 1]) dirList.Remove(2);
            if (!field[x - 1, y    ]) dirList.Remove(3);
            if (!field[x    , y + 1]) dirList.Remove(4);

            if (dirList.Count != 0) output = dirList[rnd.Next(dirList.Count)];
            else output = 0;

            return output;
        }
    }
}
