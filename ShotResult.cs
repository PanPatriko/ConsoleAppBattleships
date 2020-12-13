using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleAppBattleships
{
    public class ShotResult
    {
        public Point coordinates;
        public Battleships.BoardState state;
        public bool IsShipDestroyed;
        public Ship ship;
        public ShotResult(Point coordinates, Battleships.BoardState state, bool IsShipDestroyed, Ship ship)
        {
            this.coordinates = coordinates;
            this.state = state;
            this.IsShipDestroyed = IsShipDestroyed;
            this.ship = ship;
        }
    }
}
