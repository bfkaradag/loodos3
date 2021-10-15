using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace loodos3
{
    public class Actions
    {
        public static List<Card> createCards(char[] suits, Dictionary<Faces, int> faces)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                foreach (var j in faces)
                {
                    Card card = new Card();
                    card.suite = suits[i];
                    card.faces = j;
                    cards.Add(card);
                }
            }
            return cards;
        }
        public static List<Card> shuffleCards(List<Card> cards)
        {
            Random rng = new Random();
            return cards.OrderBy(a => rng.Next()).ToList();
        }
        public static Tuple<List<Card>, List<Player>> dealCards(List<Card> cards, List<Player> players) //At begin of the game all player get two cards
        {

            for (int i = 0; i < players.Count; i++)
            {
                Tuple<List<Card>, List<Card>> currentCards = drawCard(cards, 2);
                players[i].cards = currentCards.Item1;
                players[i].point = calculatePlayerPoint(players[i].cards);
                cards = currentCards.Item2;
            }
            return new Tuple<List<Card>, List<Player>>(cards, players);
        }

        public static int calculatePlayerPoint(List<Card> cards)
        {
            int result = 0;

            for (int i = 0; i < cards.Count; i++)
            {
                result += cards[i].faces.Value;
            }
            return result;
        }
        public static Tuple<List<Card>, List<Card>> drawCard(List<Card> cards, int count)
        {
            List<Card> userCards = cards.Skip<Card>(Math.Max(0, cards.Count() - count)).ToList();
            List<Card> newCards = cards.SkipLast(count).ToList();

            return new Tuple<List<Card>, List<Card>>(userCards, newCards);
        }
        public static List<Player> orderByHighest(List<Player> players, string by)
        {
            if (by == "point")
                return players.OrderByDescending(player => player.point).ToList();
            else if (by == "score")
                return players.OrderByDescending(player => player.score).ToList();
            return null;
        }

        public static List<Player> resetForNextGame(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].point = 0;
                players[i].isStand = false;
                players[i].isBusted = false;
                players[i].cards = null;
            }
            return players;
        }
       
        public static List<Player> beginTheRound(List<Card> cards, List<Player> players)
        {
            Tuple<List<Card>, List<Player>> dealed = dealCards(cards, players);
            cards = dealed.Item1;
            players = dealed.Item2;

            List<Player> scoredPlayers = Game.play(players, cards);
            Console.WriteLine("\n** Bu elin sonuçları **\n");
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"Oyuncu {players[i].id}, Skor: {players[i].score}");
            }
            Console.WriteLine("\n Yeni Oyun [Y] - Puanları Hesapla [Q]");
            string input = Console.ReadLine();
            if (input == "Y" || input == "y")
                beginTheRound(cards, players);

            else if (input == "Q" || input == "q")
                return scoredPlayers;

            else
                Console.WriteLine("Geçersiz tuş!");

            return null;

        }
    }
}
