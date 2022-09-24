using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW6
{
    public class Game15
    {
        private Random random;

        private int[,] Coordinates;

        private int Size, empty_i, empty_j;

        public Game15()
        {
            Coordinates = new int[4, 4];
            empty_i = 0; empty_j = 0;
            Size = 4;
            random = new Random();
        }
        public void Start()
        {
            try
            {
                for (int i = 0; i < Coordinates.GetLength(0); i++)
                    for (int j = 0; j < Coordinates.GetLength(1); j++)
                        Coordinates[i, j] = CootdinatesToTag(i, j) + 1;

                empty_i = empty_j = Size - 1;
                Coordinates[empty_i, empty_j] = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Move(int buttonTag)
        {
            try
            {
                int i, j;
                TagToCoordinates(buttonTag, out i, out j);

                if (Math.Abs(empty_i - i) + Math.Abs(empty_j - j) != 1)
                    return;

                Coordinates[empty_i, empty_j] = Coordinates[i, j];
                Coordinates[i, j] = 0;

                empty_i = i;
                empty_j = j;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void RandomMove()
        {
            int i = empty_i, j = empty_j;

            int move = random.Next(0, 4);
            switch (move)
            {
                case 0: i--; break;
                case 1: i++; break;
                case 2: j--; break;
                case 3: j++; break;
            }
            Move(CootdinatesToTag(i, j));
        }

        public int CootdinatesToTag(int i, int j)
        {
            if (i < 0) i = 0;
            if (i > Size - 1) i = Size - 1;

            if (j < 0) j = 0;
            if (j > Size - 1) j = Size - 1;

            return i * Size + j;
        }
        public void TagToCoordinates(int tag, out int i, out int j)
        {
            if (tag < 0) tag = 0;
            if (tag > Size * Size - 1) tag = Size * Size - 1;

            i = tag / Size;
            j = tag % Size;
        }

        public int GetTag(int number)
        {
            try
            {
                int i, j;
                TagToCoordinates(number, out i, out j);

                if (i < 0 || j < 0 || i >= Size || j >= Size)
                    throw new IndexOutOfRangeException();

                return Coordinates[i, j];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }

        public bool IsGameOver()
        {
            int i = 1;
            foreach (var item in Coordinates)
            {
                if (item == i || i == 16)
                    i++;
                else
                    return false;
            }
            return true;
        }
    }
}