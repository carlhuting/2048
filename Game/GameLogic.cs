using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// 2048游戏的逻辑代码
    /// </summary>
    public class GameLogic
    {
        public GameLogic()
        { }
        private int cube;
        private int capacity;
        private double tworation = 0.9;
        private List<int> Cells;
        private List<int> emptyposition;
        private Random random = new Random();
        private bool IsInGame = false;
        private MainViewModel mainmodel;
        public event EventHandler OnGameOver;
        public event EventHandler OnMerge;
        public void InitGame()
        {

            this.capacity = this.cube * this.cube;
            emptyposition = new List<int>(capacity);
            Cells = new List<int>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                this.emptyposition.Add(i);
                this.Cells.Add(0);
            }
        }

        private void NewGame()
        {
            this.IsInGame = true;

            this.emptyposition.Clear();
            for (int i = 0; i < capacity; i++)
            {
                this.emptyposition.Add(i);
                this.Cells[i] = 0;
            }
            for (int j = 0; j < 2; j++)
            {
                int index = this.random.Next(0, this.emptyposition.Count);
                int positon = this.emptyposition[index];
                this.Cells[positon] = 2;
                this.emptyposition.RemoveAt(index);
            }
        }
        public void Run(Direction direction)
        {

            bool flag = false;
            switch (direction)
            {
                case Direction.Up:

                    flag = flag || Up();
                    break;
                case Direction.Down:
                    flag = flag || Down();
                    break;
                case Direction.Left:
                    flag = flag || Left();
                    break;
                case Direction.Right:
                    flag = flag || Right();
                    break;
            }
                  
            if (this.emptyposition.Count > 0 && flag)
            {
                int index = random.Next(0, this.emptyposition.Count);
                int position = this.emptyposition[index];
                this.emptyposition.RemoveAt(index);
                if (random.NextDouble() > this.tworation)
                {
                    this.Cells[position] = 4;
                }
                else
                {
                    this.Cells[position] = 2;
                }

            }
            if (this.emptyposition.Count == 0)
            {
                
                    if (IsGameOver())
                    {
                       
                        if (mainmodel.Score > mainmodel.BestScore)
                        {
                            mainmodel.BestScore = mainmodel.Score;                          
                        }

                        
                    }
                
            }
        }

        public bool Down()
        {
            bool flag = false; //标示是否出现移位或者合并

            for (int col = capacity - cube; col < capacity; col++)//最后一行开始查询
            {
                for (int i = col; i >= 0; i = i - cube)
                {
                    if (this.Cells[i] == 0) continue;

                    int current = i;
                    int next = current + cube;

                    while (next <= col)
                    {
                        if (this.Cells[next] == 0)
                        {
                            this.Cells[next] = this.Cells[current];
                            this.Cells[current] = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current] == this.Cells[next])
                            {
                                this.Cells[next] = this.Cells[current] * 2;
                                this.mainmodel.Score += this.Cells[next];
                                this.Cells[current] = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current + cube;
                    }
                }
            }
            return flag;
        }
        public bool Up()
        {
            bool flag = false;
            for (int col = 0; col < cube; col++)
            {

                for (int i = col; i < capacity; i = i + cube)
                {
                    if (this.Cells[i] == 0) continue;

                    int current = i;
                    int next = current - cube;

                    while (next >= 0)
                    {
                        if (this.Cells[next] == 0)
                        {
                            this.Cells[next] = this.Cells[current];
                            this.Cells[current] = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current] == this.Cells[next])
                            {
                                this.Cells[next] = this.Cells[current] * 2;
                                this.mainmodel.Score += this.Cells[next];

                                this.Cells[current] = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current - cube;
                    }
                }
            }
            return flag;
        }
        public bool Left()
        {
            bool flag = false;

            for (int col = capacity - cube; col >= 0; col -= cube)
            {
                for (int i = col; i < col + cube; i++)
                {
                    if (this.Cells[i] == 0) continue;

                    int current = i;
                    int next = current - 1;

                    while (next >= col)
                    {
                        if (this.Cells[next] == 0)
                        {
                            this.Cells[next] = this.Cells[current];
                            this.Cells[current] = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current] == this.Cells[next])
                            {
                                this.Cells[next] = this.Cells[current] * 2;
                                this.mainmodel.Score += this.Cells[next];

                                this.Cells[current] = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current - 1;
                    }
                }
            }
            return flag;
        }
        public bool Right()
        {
            bool flag = false;
            for (int col = capacity - 1; col >= 0; col -= cube)
            {
                for (int i = col; i > col - cube; i = i - 1)
                {
                    if (this.Cells[i] == 0) continue;

                    int current = i;
                    int next = current + 1;

                    while (next <= col)
                    {
                        if (this.Cells[next] == 0)
                        {
                            this.Cells[next] = this.Cells[current];
                            this.Cells[current] = 0;
                            this.emptyposition.Remove(next);
                            this.emptyposition.Add(current);
                            flag = flag || true;
                        }
                        else
                        {
                            if (this.Cells[current] == this.Cells[next])
                            {
                                this.Cells[next] = this.Cells[current] * 2;
                                this.mainmodel.Score += this.Cells[next];

                                this.Cells[current] = 0;
                                this.emptyposition.Remove(next);
                                this.emptyposition.Add(current);
                                flag = flag || true;
                            }
                            break;
                        }
                        current = next;
                        next = current + 1;
                    }
                }
            }
            return flag;
        }
        private bool IsGameOver()
        {
            bool flag = true;
            int i, j;
            for (i = 0; i < cube; i += 2)
            {
                int rowmin = i * cube;
                int rowmax = (i + 1) * cube - 1;
                for (j = 0; j < cube; j += 2)
                {

                    int index = i * cube + j;
                    int up = index - cube;
                    if (up >= i && this.Cells[index] == this.Cells[up])
                    {
                        flag = false;
                        goto exit;
                    }
                    int bottom = index + cube;
                    if (bottom < capacity && this.Cells[index] == this.Cells[bottom])
                    {
                        flag = false;
                        goto exit;
                    }
                    int left = index - 1;
                    if (left >= rowmin && this.Cells[index] == this.Cells[left])
                    {
                        flag = false;
                        goto exit;
                    }
                    int right = index + 1;
                    if (right <= rowmax && this.Cells[index] == this.Cells[right])
                    {
                        flag = false;
                        goto exit;
                    }
                }
            }
            for (i = 1; i < cube; i += 2)
            {
                int rowmin = i * cube;
                int rowmax = (i + 1) * cube - 1;
                for (j = 1; j < cube; j += 2)
                {

                    int index = i * cube + j;
                    int up = index - cube;
                    if (up >= i && this.Cells[index] == this.Cells[up])
                    {
                        flag = false;
                        goto exit;
                    }
                    int bottom = index + cube;
                    if (bottom < capacity && this.Cells[index] == this.Cells[bottom])
                    {
                        flag = false;
                        goto exit;
                    }
                    int left = index - 1;
                    if (left >= rowmin && this.Cells[index] == this.Cells[left])
                    {
                        flag = false;
                        goto exit;
                    }
                    int right = index + 1;
                    if (right <= rowmax && this.Cells[index] == this.Cells[right])
                    {
                        flag = false;
                        goto exit;
                    }
                }
            }
        exit:
            return flag;
        }
    }
}
