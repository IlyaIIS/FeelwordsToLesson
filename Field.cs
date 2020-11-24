using System;
using System.Collections.Generic;

namespace Fillwords
{
    class Field
    {
        Random rnd = new Random();
        
        public List<string> wordsList = new List<string>();      //лист слов на поле
        public List<List<int>> wordPos = new List<List<int>>();  //лист координат каждой буквы каждого слова
        public int xSize, ySize;                                 //размер поля
        public char[,] cellLetter;                              //поле букв
        public dynamic[,,] cellColor;

        char[] lettersList = {'А', 'Б', 'В', 'Г', 'Д', 'Е', 'И' };  //лист букв для заполнения пробелов

        public void CreateNewField(int input1, int input2, WordsSet words)
        {
            xSize = input1;
            ySize = input2;
            cellLetter = new char[xSize, ySize];
            cellColor = new dynamic[xSize, ySize, 2];

            //создание поля свободных ячеек
            bool[,] preField = new bool[xSize + 2, ySize + 2];
            for (int y = 0; y <= ySize + 1; y++)
                for (int x = 0; x <= xSize + 1; x++)
                    if (x == xSize + 1 || y == 0 || x == 0 || y == ySize + 1) preField[x, y] = false;
                    else preField[x, y] = true;

            //заполнение поля шаблонами слов
            int cellNum = xSize * ySize;
            foreach (int pos in GetArrayOfPositionsWithRandomOrder(xSize, ySize))
            {
                int x = pos % xSize + 1;
                int y = pos / ySize + 1;

                if (preField[x, y])
                {
                    int dir = FindDirection(x, y, preField);

                    if (dir != 0)
                    {
                        int lenght = 0;
                        wordPos.Add(new List<int>());

                        do
                        {
                            preField[x, y] = false;
                            cellNum--;

                            lenght++;
                            wordPos[wordPos.Count - 1].Add((y - 1) * xSize + (x - 1));

                            int localX = x + (-(dir - 2) % 2);
                            int localY = y + ((dir - 3) % 2);
                            if (!preField[localX, localY])
                            {
                                dir = FindDirection(x, y, preField);
                                if (dir == 0) break;
                            }
                            x += (-(dir - 2) % 2);
                            y += (dir - 3) % 2;

                            if (lenght == words.setWords.Count - 1 || (lenght >= 4 && rnd.Next(6) == 0)) break;
                        } while (true);
                    }
                }
            }

            //заполнение пустот
            int time = 0;
            do
            {
                for (int y = 1; y <= ySize; y++)
                    for (int x = 1; x <= xSize; x++)
                    {
                        if (preField[x, y])
                        {
                            int pos = (y - 1) * xSize + (x - 1);

                            for (int i = 0; i < wordPos.Count; i++)
                            {
                                int dif = Math.Abs(pos - wordPos[i][0]);
                                if (dif == 1 || dif == 10)
                                {
                                    wordPos[i].Insert(0, pos);
                                    cellNum--;
                                    preField[x, y] = false;
                                    break;
                                }

                                dif = Math.Abs(pos - wordPos[i][wordPos[i].Count - 1]);
                                if (dif == 1 || dif == 10)
                                { 
                                    wordPos[i].Add(pos);
                                    cellNum--;
                                    preField[x, y] = false;
                                    break;
                                }
                            }
                        }
                    }
                time++;
            } while (cellNum > 0 && time < 4);

            //деление слишком больших слов
            for (int i = 0; i < wordPos.Count; i++)
            {
                if (wordPos[i].Count >= words.setWords.Count)
                {
                    int lenght = wordPos[i].Count;
                    int mid = lenght / 2 + (lenght % 2) / 5;

                    wordPos.Add(new List<int>());
                    wordPos.Add(new List<int>());

                    for (int ii = 0; ii < lenght / 2; ii++)
                        wordPos[wordPos.Count - 2].Add(wordPos[i][ii]);

                    for (int ii = mid; ii < lenght; ii++)
                        wordPos[wordPos.Count - 1].Add(wordPos[i][ii]);

                    wordPos.RemoveAt(i);
                    i--;
                }
            }

            //подбор слов в шаблоны слов
            for (int i = 0; i < wordPos.Count; i++)
                {
                    int lenght = wordPos[i].Count;

                    if (lenght <= words.setWords.Count - 1 && words.setWords[lenght].Count != 0)
                    {
                        int randomNum = rnd.Next(words.setWords[lenght].Count);

                        wordsList.Add(words.setWords[lenght][randomNum]);

                        words.setWords[lenght].Remove(wordsList[wordsList.Count - 1]);

                        if (words.setWords.Count == lenght && words.setWords[lenght].Count == 0)
                            words.setWords.RemoveAt(words.setWords.Count - 1);
                    }
                    else
                    {
                        wordsList.Add(new string('0', lenght));
                    }
                }

            


            //заполнение основного поля буквами и присваивание им цвета
            for (int i = 0; i < wordsList.Count; i++)
                for (int ii = 0; ii < wordsList[i].Length; ii++)
                {
                    int x = wordPos[i][ii] % xSize;
                    int y = wordPos[i][ii] / xSize;
                    cellLetter[x, y] = wordsList[i][ii];
                }

            for (int y = 0; y < ySize; y++)
                for (int x = 0; x < xSize; x++)
                {
                    if (cellLetter[x, y] == '\0')
                        cellLetter[x, y] = 'Y';//lettersList[rnd.Next(lettersList.Length)];

                    cellColor[x, y, 0] = ConsoleColor.Black;
                    cellColor[x, y, 1] = ConsoleColor.White;
                }
        }

        int[] GetArrayOfPositionsWithRandomOrder(int w, int h)
        {
            int[] output = new int[w * h];

            for (int i = 0; i < output.Length; i++)
                output[i] = i;

            for (int i = 0; i < output.Length; i++)
            {
                int local = output[i];
                output[i] = output[rnd.Next(output.Length)];
                output[rnd.Next(output.Length)] = local;
            }

            return output;
        }

        int FindDirection(int x, int y, bool[,] field)
        {
            List<int> dirList = new List<int>() { 1, 2, 3, 4 };

            if (!field[x + 1, y    ]) dirList.Remove(1);
            if (!field[x    , y - 1]) dirList.Remove(2);
            if (!field[x - 1, y    ]) dirList.Remove(3);
            if (!field[x    , y + 1]) dirList.Remove(4);

            if (dirList.Count != 0) 
                return dirList[rnd.Next(dirList.Count)];
            else 
                return 0;
        }
    }
}
