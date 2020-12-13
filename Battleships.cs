using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleAppBattleships
{
    public static class Battleships
    {
        static readonly int boardSize = 11;
        static Random rand = new Random();
        public static bool AllShipsReady(Ship[] ships)
        {
            foreach (Ship s in ships)
            {
                if (!s.IsReady)
                    return false;
            }
            return true;
        }
        public static void CreateEmptyBoard(BoardState[,] board)
        {
            board[0, 0] = BoardState.CharOrNum;
            for (int i = 1; i < boardSize; i++)
            {
                board[i, 0] = BoardState.CharOrNum;
                board[0, i] = BoardState.CharOrNum;
            }
            for (int i = 1; i < boardSize; i++)
            {
                for (int j = 1; j < boardSize; j++)
                {
                    board[i, j] = BoardState.Empty;
                }
            }
        }
        public static void ClearReservedSquares(BoardState[,] board)
        {
            for (int i = 1; i < boardSize; i++)
            {
                for (int j = 1; j < boardSize; j++)
                {
                    if (board[i, j] == BoardState.Reserved)
                        board[i, j] = BoardState.Empty;
                }
            }
        }
        public static bool CheckPlaceOnBoard(Ship ship, BoardState[,] board, int x, int y)
        {
            switch (ship.position)
            {
                case Ship.Position.Vertical: //zamieniome

                    for (int i = 0; i < (int)ship.type; i++)
                    {
                        if (x + i < boardSize && x + i >= 1)
                        {
                            if (board[x + i, y] == BoardState.Reserved ||
                                board[x + i, y] == BoardState.Ship)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                case Ship.Position.Horizontal:

                    for (int i = 0; i < (int)ship.type; i++)
                    {
                        if (y + i < boardSize && y + i >= 1)
                        {
                            if (board[x, y + i] == BoardState.Reserved ||
                                board[x, y + i] == BoardState.Ship)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
            }
            return true;
        }
        public static void RandomizeShips(Ship[] ships, BoardState[,] board)
        {
            foreach (Ship s in ships)
            {
                s.IsReady = false;
                int x = 0, y = 0;
                do
                {
                    s.position = (Ship.Position)rand.Next(1, 3);
                    switch (s.position)
                    {
                        case Ship.Position.Vertical:
                            y = rand.Next(1, boardSize);
                            x = rand.Next(1, boardSize - (int)s.type);
                            break;
                        case Ship.Position.Horizontal:
                            x = rand.Next(1, boardSize);
                            y = rand.Next(1, boardSize - (int)s.type);
                            break;
                    }
                } while (!CheckPlaceOnBoard(s, board, x, y));
                s.StartPos = new Point(x, y);
                PlaceShipOnBoard(s, board);
            }
        }

        public static void ResetShips(Ship[] ships)
        {
            foreach (Ship s in ships)
            {
                s.Reset();
            }
        }

        public static void PlaceShipOnBoard(Ship ship, BoardState[,] board)
        {
            int x = (int)ship.StartPos.X;
            int y = (int)ship.StartPos.Y;
            switch (ship.position)
            {
                case Ship.Position.Vertical:
                    for (int i = -1; i < (int)ship.type + 1; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (x + i < boardSize && y + j < boardSize
                                && x + i >= 1 && y + j >= 1)
                            {
                                board[x + i, y + j] = BoardState.Reserved;
                            }
                        }
                    }
                    foreach (Point p2 in ship.Coordinates)
                    {
                        board[(int)p2.X, (int)p2.Y] = BoardState.Ship;
                    }
                    ship.IsReady = true;
                    break;
                case Ship.Position.Horizontal:
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < (int)ship.type + 1; j++)
                        {
                            if (x + i < boardSize && y + j < boardSize
                                && x + i >= 1 && y + j >= 1)
                            {
                                board[x + i, y + j] = BoardState.Reserved;
                            }
                        }
                    }
                    foreach (Point p2 in ship.Coordinates)
                    {
                        board[(int)p2.X, (int)p2.Y] = BoardState.Ship;
                    }
                    ship.IsReady = true;
                    break;
            }
        }
        public static ShotResult Shot(BoardState[,] board, Ship[] ships, Point hit)
        {
            if (board[(int)hit.X, (int)hit.Y] == BoardState.Hit || board[(int)hit.X, (int)hit.Y] == BoardState.Miss)
            {
                return new ShotResult(hit, board[(int)hit.X, (int)hit.Y], false, null);
            }
            else if (board[(int)hit.X, (int)hit.Y] == BoardState.Empty)
            {
                board[(int)hit.X, (int)hit.Y] = BoardState.Miss;
                return new ShotResult(hit, BoardState.Empty, false, null);
            }
            else
            {
                foreach (Ship s in ships)
                {
                    foreach (Point p in s.Coordinates)
                    {
                        if (p == hit)
                        {
                            s.Hit();
                            if (!s.IsAlive)
                            {
                                switch (s.position)
                                {
                                    case Ship.Position.Vertical:
                                        for (int i = -1; i < (int)s.type + 1; i++)
                                        {
                                            for (int j = -1; j < 2; j++)
                                            {
                                                if (s.StartPos.X + i < boardSize && s.StartPos.Y + j < boardSize
                                                    && s.StartPos.X + i >= 1 && s.StartPos.Y + j >= 1)
                                                {
                                                    board[(int)s.StartPos.X + i, (int)s.StartPos.Y + j] = BoardState.Miss;
                                                }
                                            }
                                        }
                                        foreach (Point p2 in s.Coordinates)
                                        {
                                            board[(int)p2.X, (int)p2.Y] = BoardState.Hit;
                                        }
                                        break;
                                    case Ship.Position.Horizontal:
                                        for (int i = -1; i < 2; i++)
                                        {
                                            for (int j = -1; j < (int)s.type + 1; j++)
                                            {
                                                if (s.StartPos.X + i < boardSize && s.StartPos.Y + j < boardSize
                                                    && s.StartPos.X + i >= 1 && s.StartPos.Y + j >= 1)
                                                {
                                                    board[(int)s.StartPos.X + i, (int)s.StartPos.Y + j] = BoardState.Miss;
                                                }
                                            }
                                        }
                                        foreach (Point p2 in s.Coordinates)
                                        {
                                            board[(int)p2.X, (int)p2.Y] = BoardState.Hit;
                                        }
                                        break;
                                }
                                return new ShotResult(hit, BoardState.Ship, true,s);
                            }
                            board[(int)hit.X, (int)hit.Y] = BoardState.Hit;
                            return new ShotResult(hit, BoardState.Ship, false,s);
                        }
                    }
                }
            }
            return null;
        }
        public enum BoardState
        {
            Ship,
            Empty,
            Reserved,
            Miss,
            Hit,
            CharOrNum,
        }
    }
}
