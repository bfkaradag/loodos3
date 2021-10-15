using System;
using System.Collections.Generic;
using System.Text;

namespace loodos3
{
    public class Player
    {
        public int id { get; set; }
        public List<Card> cards { get; set; }
        public int point { get; set; }
        public bool isBusted { get; set; }
        public bool isStand { get; set; }
        public int score { get; set; }
        public Player(int id, List<Card> cards, int point)
        {
            this.id = id;
            this.cards = cards;
            this.point = point;
        }
    }
}
