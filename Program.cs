using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ConsoleAppBattleships
{
    class Program
    {
        static Random rand = new Random();

        const int boardSize = 11;
        const int border = 45;

        const string shipSquare = "O";
        const string emptySquare = "0";
        const string reservedSquare = "R";
        const string missSquare = "X";
        const string hitSquare = "T";

        static void Main(string[] args)
        {
            Ship[] PlayerShips;
            Ship[] ComputerShips;

            Ship battleship = new Ship(Ship.Type.Battleship);
            Ship cruiser1 = new Ship(Ship.Type.Cruiser);
            Ship cruiser2 = new Ship(Ship.Type.Cruiser);
            Ship destroyer1 = new Ship(Ship.Type.Destroyer);
            Ship destroyer2 = new Ship(Ship.Type.Destroyer);
            Ship destroyer3 = new Ship(Ship.Type.Destroyer);
            Ship patrol1 = new Ship(Ship.Type.Submarine);
            Ship patrol2 = new Ship(Ship.Type.Submarine);
            Ship patrol3 = new Ship(Ship.Type.Submarine);
            Ship patrol4 = new Ship(Ship.Type.Submarine);

            Ship Cbattleship = new Ship(Ship.Type.Battleship);
            Ship Ccruiser1 = new Ship(Ship.Type.Cruiser);
            Ship Ccruiser2 = new Ship(Ship.Type.Cruiser);
            Ship Cdestroyer1 = new Ship(Ship.Type.Destroyer);
            Ship Cdestroyer2 = new Ship(Ship.Type.Destroyer);
            Ship Cdestroyer3 = new Ship(Ship.Type.Destroyer);
            Ship Cpatrol1 = new Ship(Ship.Type.Submarine);
            Ship Cpatrol2 = new Ship(Ship.Type.Submarine);
            Ship Cpatrol3 = new Ship(Ship.Type.Submarine);
            Ship Cpatrol4 = new Ship(Ship.Type.Submarine);

            PlayerShips = new Ship[] {battleship,cruiser1,cruiser2,
                                destroyer1,destroyer2,destroyer3,
                                patrol1,patrol2,patrol3,patrol4};
            ComputerShips = new Ship[] {Cbattleship,Ccruiser1,Ccruiser2,
                                Cdestroyer1,Cdestroyer2,Cdestroyer3,
                                Cpatrol1,Cpatrol2,Cpatrol3,Cpatrol4};

            string option,option2,option3="";
            string[,] PlayerBoard = new string[boardSize, boardSize];
            string[,] ComputerBoard = new string[boardSize, boardSize];

            Battleships.BoardState [,] PlayerBoardState = new Battleships.BoardState[boardSize, boardSize];
            Battleships.BoardState [,] ComputerBoardState = new Battleships.BoardState[boardSize, boardSize];

            do
            {
                Console.ResetColor();
                Console.Clear();
                WriteBorder(border);
                WriteMess(border, " Statki");
                WriteMess(border, " 1. - Gra");
                WriteMess(border, " 2. - O grze");
                WriteMess(border, " 3. - Wyjście");
                WriteBorder(border);
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        WriteBorder(border);
                        WriteMess(border, " Rozmieść swoje statki");
                        WriteMess(border, " 1. - Ręcznie");
                        WriteMess(border, " 2. - Losowo");
                        WriteMess(border, " 3. - Wróć");
                        WriteBorder(border);
                        option2 = Console.ReadLine();
                        switch(option2)
                        {
                            case "1":
                                CreateEmptyBoard(PlayerBoard);
                                Battleships.CreateEmptyBoard(PlayerBoardState);
                                Battleships.ResetShips(PlayerShips);
                                Battleships.ResetShips(ComputerShips);
                                PlaceShipsOnBoard(PlayerShips, PlayerBoard,PlayerBoardState);
                                if(AllShipsReady(PlayerShips))
                                {
                                    CreateEmptyBoard(ComputerBoard);
                                    Battleships.CreateEmptyBoard(ComputerBoardState);
                                    Battleships.RandomizeShips(ComputerShips, ComputerBoardState);
                                    StartGame();
                                }
                                break;
                            case "2":
                                Battleships.ResetShips(PlayerShips);
                                Battleships.ResetShips(ComputerShips);
                                CreateEmptyBoard(PlayerBoard);
                                Battleships.CreateEmptyBoard(PlayerBoardState);
                                CreateEmptyBoard(ComputerBoard);
                                Battleships.CreateEmptyBoard(ComputerBoardState);
                                Battleships.RandomizeShips(PlayerShips,PlayerBoardState);
                                Battleships.RandomizeShips(ComputerShips, ComputerBoardState);
                                StartGame();
                                break;
                            case "3":

                                break;
                            default:
                                WriteBorder(border);
                                WriteMess(border, " Nie ma takiej opcji");
                                WriteBorder(border);
                                break;
                        }
                        break;
                    case "2":
                        About();
                        break;
                    case "3":

                        break;
                    default:
                        WriteBorder(border);
                        WriteMess(border, " Nie ma takiej opcji");
                        WriteBorder(border);
                        break;
                }


            } while (option != "3");

            void About()
            {
                Console.Clear();
                WriteBorder(border * 2);
                WriteMess(border * 2, "Każdy z graczy posiada plansze o wielkości 10x10");
                WriteMess(border * 2, "Kolumny są oznaczone poprzez współrzędne literami od A do J i liczbami 1 do 10.");
                WriteMess(border * 2, "Statki ustawiane są w pionie lub poziomie, w taki sposób,");
                WriteMess(border * 2, "aby nie stykały się one ze sobą ani bokami, ani rogami.");
                WriteBorder(border * 2);
                WriteMess(border * 2, "Występują 4 rodzaje okrętów:");
                WriteMess(border * 2, Ship.Type.Battleship.ToPL() + " zajmujący 4 pola");
                WriteMess(border * 2, Ship.Type.Cruiser.ToPL() + " zajmujący 3 pola");
                WriteMess(border * 2, Ship.Type.Destroyer.ToPL() + " zajmujący 2 pola");
                WriteMess(border * 2, Ship.Type.Submarine.ToPL() + " zajmujący 1 pole");
                WriteBorder(border * 2);
                WriteMess(border * 2, "Strzały oddawane są naprzemiennie, poprzez podanie współrzędnych pola (np. B5).");
                WriteMess(border * 2, "W przypadku strzału trafionego, gracz kontynuuje strzelanie aż do momentu chybienia.");
                WriteMess(border * 2, "Zatopienie statku ma miejsce wówczas, gdy gracz odgadnie położenie całego statku.");
                WriteMess(border * 2, "O chybieniu gracz informowany jest komunikatem „pudło”,");
                WriteMess(border * 2, "o trafieniu „trafiony” lub „trafiony i zatopiony”.");
                WriteMess(border * 2, "Zatopiony statek powoduje, pola wokół niego oznaczane są jako ostrzelane");
                WriteMess(border * 2, "Wygrywa ten, kto pierwszy zatopi wszystkie statki przeciwnika.");
                WriteBorder(border * 2);
                WriteMess(border * 2, "Oznaczenia pól:");
                WriteMess(border * 2, " " + emptySquare + " - Puste pole");
                WriteMess(border * 2, " " + missSquare + " - Pole w które oddano nietrafiony strzał");
                WriteMess(border * 2, " " + hitSquare + " - Pole w które oddano trafiony strzał");
                WriteMess(border * 2, " " + shipSquare + " - Pole reprezentujące statek");
                WriteMess(border * 2, " " + reservedSquare + " - Pole zarezerwowane przez statek, w tym miejscu nie może znajdować się inny statek");
                WriteMess(border * 2, "Przykładowa plansza:");
               // WriteBorder(border * 2);
                CreateEmptyBoard(ComputerBoard);
                Battleships.CreateEmptyBoard(ComputerBoardState);
                Battleships.RandomizeShips(ComputerShips, ComputerBoardState);
                DrawBoard(ComputerBoard, ComputerBoardState);
                WriteBorder(border * 2);
                WriteMess(border * 2, "Kliknij dowolny przycisk aby wrócić do menu");
                WriteBorder(border * 2);
                Console.ReadKey();
            }

            bool AllShipsReady(Ship[] ships)
            {
                foreach (Ship s in ships)
                {
                    if (!s.IsReady)
                        return false;
                }
                return true;
            }

            void CreateEmptyBoard(string[,] board)
            {
                board[0, 0] = " ";
                for (int i = 1; i < boardSize; i++)
                {
                    board[i, 0] = i.ToString();
                    board[0, i] = ((char)(64 + i)).ToString();
                }
                for (int i = 1; i < boardSize; i++)
                {
                    for (int j = 1; j < boardSize; j++)
                    {
                        board[i, j] = emptySquare;
                    }
                }
            }           

            void DrawBoard(string[,] board,Battleships.BoardState[,] boardStates)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (j == 0 && i != boardSize - 1)
                        {
                            Console.ResetColor();
                            Console.Write(board[i, j] + "  ");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;

                            switch (boardStates[i, j])
                            {
                                case Battleships.BoardState.Ship:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(shipSquare + " ");
                                    break;

                                case Battleships.BoardState.Empty:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write(emptySquare + " ");
                                    break;

                                case Battleships.BoardState.Reserved:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write(reservedSquare + " ");
                                    break;

                                case Battleships.BoardState.Miss:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write(missSquare + " ");
                                    break;

                                case Battleships.BoardState.Hit:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(hitSquare + " ");
                                    break;

                                default:
                                    Console.ResetColor();
                                    Console.Write(board[i, j] + " ");
                                    break;
                            }
                        }
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            void DrawHiddenBoard(string[,] board, Battleships.BoardState[,] boardStates)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (j == 0 && i != boardSize - 1)
                        {
                            Console.ResetColor();
                            Console.Write(board[i, j] + "  ");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;

                            switch (boardStates[i, j])
                            {
                                case Battleships.BoardState.Ship:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write(emptySquare + " ");
                                    break;

                                case Battleships.BoardState.Empty:
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write(emptySquare + " ");
                                    break;

                                case Battleships.BoardState.Reserved:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write(reservedSquare + " ");
                                    break;

                                case Battleships.BoardState.Miss:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write(missSquare + " ");
                                    break;

                                case Battleships.BoardState.Hit:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(hitSquare + " ");
                                    break;

                                default:
                                    Console.ResetColor();
                                    Console.Write(board[i, j] + " ");
                                    break;
                            }
                        }
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }

            void StartGame()
            {
                Console.Clear();
                WriteBorder(border);
                WriteMess(border, " Kto ma zacząć?");
                WriteMess(border, " 1. - Gracz");
                WriteMess(border, " 2. - Komputer");
                WriteMess(border, " 3. - Losowo");
                WriteMess(border, " 4. - Wróc");
                WriteBorder(border);
                option3 = Console.ReadLine();
                switch (option3)
                {
                    case "1":
                        Game(true);
                        break;
                    case "2":
                        Game(false);
                        break;
                    case "3":
                        int n = rand.Next(0, 2);
                        if(n==0)
                        {
                            Game(true);
                        }
                        else
                            Game(false);
                        break;
                    case "4":
                    default:
                        WriteBorder(border);
                        WriteMess(border, " Nie ma takiej opcji");
                        WriteBorder(border);
                        break;
                }

            }

            Point SetCoordinates()
            {
                int x = 0, y = 0;
                bool correct = false;
                WriteBorder(border);
                WriteMess(border, " Podaj [A-J]");
                WriteBorder(border);
                while (!correct)
                {
                    char[] c1 = Console.ReadLine().ToCharArray();
                    if (c1.Length == 1 && c1[0] >= 'A' && c1[0] <= 'J')
                    {
                        y = c1[0] - 64;
                        correct = true;
                    }
                    else
                    {
                        WriteBorder(border);
                        WriteMess(border, " Nieprawidłowe dane");
                        WriteBorder(border);
                        correct = false;
                    }
                }
                WriteBorder(border);
                WriteMess(border, " Podaj [1-10]");
                WriteBorder(border);
                correct = false;
                while (!correct)
                {
                    if (int.TryParse(Console.ReadLine(), out x))
                    {
                        if (x >= 1 && x <= 10)
                        {
                            correct = true;
                        }
                        else
                        {
                            WriteBorder(border);
                            WriteMess(border, " Nieprawidłowy zakres");
                            WriteBorder(border);
                            correct = false;
                        }
                    }
                    else
                    {
                        WriteBorder(border);
                        WriteMess(border, " Nieprawidłowe dane");
                        WriteBorder(border);
                        correct = false;
                    }
                }
                return new Point(x, y);
            }
            void Game(bool playerTurn)
            {
                Battleships.ClearReservedSquares(PlayerBoardState);
                Battleships.ClearReservedSquares(ComputerBoardState);
                CreateEmptyBoard(ComputerBoard);
                bool endGame = false;
                do
                {
                    Console.Clear();
                    WriteBorder(border);
                    WriteMess(border, " Twoja plansza");
                    WriteBorder(border);
                    DrawBoard(PlayerBoard,PlayerBoardState);
                    WriteBorder(border);
                    WriteMess(border, " Plansza przeciwnkia");
                    WriteBorder(border);
                    DrawHiddenBoard(ComputerBoard,ComputerBoardState);
                    if (playerTurn)
                    {
                        WriteBorder(border);
                        WriteMess(border, " Podaj koordynaty");
                        WriteBorder(border);
                        Point hit = SetCoordinates();
                        ShotResult result = Battleships.Shot(ComputerBoardState,ComputerShips,hit);
                        if(result.state == Battleships.BoardState.Hit || result.state == Battleships.BoardState.Miss)
                        {
                            WriteBorder(border);
                            WriteMess(border, " Już tam strzelałeś !!!");
                            WriteMess(border, " Zmarnowany strzał " + ((char)(64 + hit.Y)).ToString() + hit.X.ToString());
                            WriteBorder(border);
                            playerTurn = false;
                        }
                        else if (result.state == Battleships.BoardState.Empty)
                        {
                            WriteBorder(border);
                            WriteMess(border, " Pudło " + ((char)(64 + hit.Y)).ToString() + hit.X.ToString());
                            WriteBorder(border);
                            playerTurn = false;
                        }
                        else
                        {
                            WriteBorder(border);
                            WriteMess(border, " Trafiony " + ((char)(64 + hit.Y)).ToString() + hit.X.ToString());
                            if (result.IsShipDestroyed)
                            {
                                WriteMess(border, " i zatopiony " + result.ship.type.ToPL());
                            }
                            WriteBorder(border);
                            playerTurn = true;                           
                        }
                        endGame = true;
                        foreach (Ship s in ComputerShips)
                        {
                            if (s.IsAlive)
                                endGame = false;
                        }
                        if (endGame)
                            WriteMess(border, " Wygrałeś");
                    }
                    else
                    {
                        do
                        {
                            Point hit = new Point(rand.Next(1, boardSize), rand.Next(1, boardSize));
                            ShotResult result = Battleships.Shot(PlayerBoardState, PlayerShips, hit);
                            if (result.state == Battleships.BoardState.Empty)
                            {
                                WriteBorder(border);
                                WriteMess(border, " Przeciwnk spudłował " + ((char)(64 + hit.Y)).ToString() + hit.X.ToString());
                                WriteBorder(border);
                                playerTurn = true;
                            }
                            else if (result.state == Battleships.BoardState.Ship)
                            {
                                WriteBorder(border);
                                WriteMess(border, " Przeciwnk trafił " + ((char)(64 + hit.Y)).ToString() + hit.X.ToString());
                                if(result.IsShipDestroyed)
                                {
                                    WriteMess(border, " i zatopił nasz " + result.ship.type.ToPL());
                                }
                                WriteBorder(border);
                                playerTurn = false;                               
                            }
                        } while (!playerTurn);

                        endGame = true;
                        foreach (Ship s in PlayerShips)
                        {
                            if (s.IsAlive)
                                endGame = false;
                        }
                        if (endGame)
                            WriteMess(border, " Przegrałeś");
                    }

                    WriteMess(border, " Kliknij dowolny przycisk aby przejść dalej");
                    if (!endGame)
                    {
                        WriteMess(border, " Kliknij 4 aby wyjść");
                    } 
                    WriteBorder(border);
                    if (Console.ReadLine() == "4")
                        break;

                } while (!endGame);
            }

            void PlaceShipsOnBoard(Ship[] ships, string[,] board,Battleships.BoardState [,] boardStates)
            {
                foreach (Ship s in ships)
                {
                    DrawBoard(board, boardStates);
                    bool correct = false;
                    while (!correct)
                    {
                        WriteBorder(border);
                        WriteMess(border, " Wybierz orientacje dla " + s.type.ToPL());
                        WriteMess(border, " 1. - Pionowa");
                        WriteMess(border, " 2. - Pozioma");
                        WriteMess(border, " 3. - Wróć");
                        WriteBorder(border);

                        while (!correct)
                        {
                            option3 = Console.ReadLine();
                            if (option3 == "1")
                            {
                                s.position = Ship.Position.Vertical;
                                correct = true;
                            }
                            else if (option3 == "2")
                            {
                                s.position = Ship.Position.Horizontal;
                                correct = true;
                            }
                            else if (option3 == "3")
                            {
                                break;
                            }
                            else
                            {
                                WriteBorder(border);
                                WriteMess(border, " Nie ma takiej opcji");
                                WriteBorder(border);
                                correct = false;
                            }
                        }
                        if (option3 == "3")
                        {
                            break;
                        }
                        Console.Clear();
                        DrawBoard(board, boardStates);
                        WriteBorder(border);
                        WriteMess(border, " Wybierz koordynaty dla " + s.type.ToPL());
                        Point p = SetCoordinates();
                        if (Battleships.CheckPlaceOnBoard(s, boardStates,(int)p.X,(int)p.Y))
                        {
                            s.StartPos = p;
                            Battleships.PlaceShipOnBoard(s, boardStates);
                            correct = true;
                        }
                        else
                        {
                            correct = false;
                            WriteBorder(border);
                            WriteMess(border, " Statek poza planszą lub");
                            WriteMess(border, " za blisko innego statku, wybierz ponownie");
                            WriteBorder(border);
                        }
                    }
                    if (option3 == "3")
                        break;
                }
            }

           

            void WriteBorder(int n)
            {
                string m = "---||---";
                for (int i = 0; i < n; i++)
                {
                    m = m + "-";
                }
                m = m + "---||---";
                Console.WriteLine(m);
            }

            void WriteMess(int n, string mess)
            {
                string m = "---||---" + mess;

                for (int i = 0; i < n - mess.Length; i++)
                {
                    m = m + " ";
                }
                m = m + "---||---";
                Console.WriteLine(m);
            }

        }
    }
}
