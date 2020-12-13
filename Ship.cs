using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleAppBattleships
{
    public class Ship
    {
        public Position position;
        public Type type;
        public bool IsReady = false;
        public bool IsAlive { get; set; }
        private int hitCount;
        public Ship(Type type)
        {
            this.type = type;
            position = Position.Horizontal;
            IsAlive = true;
        }
        int HitCount
        {
            get
            {
                return hitCount;
            }
            set
            {
                hitCount = value;
                if (hitCount == (int)type) IsAlive = false;
            }
        }
        public  Point [] Coordinates;
        private Point startpos;
        public Point StartPos
        {
            get
            {
                return startpos;
            }
            set
            {
                startpos = value;
                Coordinates = new Point[(int)type];
                switch (position)
                {
                    case Ship.Position.Vertical:
                        for (int i = 0; i < (int)type; i++)
                        {
                            Coordinates[i] = new Point(startpos.X + i, startpos.Y); /// i zmaienione
                        }
                        break;
                    case Ship.Position.Horizontal:
                        for (int i = 0; i < (int)type; i++)
                        {
                            Coordinates[i] = new Point(startpos.X, startpos.Y + i);
                        }
                        break;
                }
            }
        }
        public void Hit()
        {
            HitCount++;
        }
        public void Reset()
        {
            HitCount = 0;
            IsAlive = true;
            IsReady = false;
        }
        public enum Position
        {
            Vertical = 1, 
            Horizontal,
        }
        public enum Type
        {
            Submarine = 1, //1
            Destroyer, //2
            Cruiser, //3
            Battleship, //4
        }

    }
    public static class Extension
    {
        public static string ToPL(this Ship.Type type)
        {
            switch (type)
            {
                case Ship.Type.Battleship:
                    return "Pancernik";
                case Ship.Type.Destroyer:
                    return "Niszczyciel";
                case Ship.Type.Submarine:
                    return "Okręt podwodny";
                case Ship.Type.Cruiser:
                    return "Krążownik";
            }
            throw new NotImplementedException();
        }
    }
}
