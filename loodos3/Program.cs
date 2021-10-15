using System;
using System.Collections.Generic;
using System.Linq;
public enum Faces { A, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, J, Q, K };

namespace loodos3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> cards = new List<Card>();
            char[] suits = { 'D', 'H', 'C', 'S' };// Kart tanımları
            Dictionary<Faces, int> faces = new Dictionary<Faces, int>
            {
                {Faces.A, 1},
                {Faces.Two, 2},
                {Faces.Three, 3},
                {Faces.Four, 4},
                {Faces.Five, 5},
                {Faces.Six, 6},
                {Faces.Seven, 7},
                {Faces.Eight, 8},
                {Faces.Nine, 9},
                {Faces.Ten, 10},
                {Faces.J, 10},
                {Faces.Q, 10},
                {Faces.K, 10}
            };
            cards = Actions.createCards(suits, faces);
            cards = Actions.shuffleCards(cards);

            List<Player> players = new List<Player>();
            players.Add(new Player(1, new List<Card>(), 0));
            players.Add(new Player(2, new List<Card>(), 0));
            players.Add(new Player(3, new List<Card>(), 0));

            Game.Start(cards, players);
        }
        
       
    }   
        
}
    

   
    