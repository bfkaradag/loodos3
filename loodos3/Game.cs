using System;
using System.Collections.Generic;
using System.Text;

namespace loodos3
{
    public class Game
    {
        public static void Start(List<Card> cards, List<Player> players)
        {
            List<Player> gameScores = Actions.beginTheRound(cards, players);

            Console.WriteLine("Oyun bitti!\nSkorlar:");
            Console.WriteLine("_____________________");

            for (int i = 0; i < gameScores.Count; i++)
            {
                Console.WriteLine($"Oyuncu {gameScores[i].id}: {gameScores[i].score}");
            }
            List<Player> highestScores = Actions.orderByHighest(gameScores, "score");


            Console.WriteLine($"\nKazanan: Oyuncu {highestScores[0].id}  Skor: {highestScores[0].score}");
        }
        public static List<Player> play(List<Player> players, List<Card> cards)
        {
            List<Player> unqualifiedPlayers = players.FindAll(player => player.isBusted != true);
            List<Player> standPlayers = players.FindAll(player => player.isStand == true);

            if (standPlayers.Count == unqualifiedPlayers.Count) // Tüm oyuncuların stand kararı senaryosunda 21 e en yakın oyuncu, oyunu kazanır.
            {
                List<Player> highestScores = Actions.orderByHighest(unqualifiedPlayers, "point");
                if (highestScores.Count > 1 && highestScores[0].point == highestScores[1].point)
                {
                    Console.WriteLine($"\n***Kalan oyuncular 'STAND' , bu elin kazananı yok. Oyuncuların puanları eşit.");
                    return Actions.resetForNextGame(players);
                }
                Player winner = highestScores[0];
                Console.WriteLine($"\n***Kalan oyuncular 'STAND' , bu elin kazananı: {winner.id}, puan: {winner.point}");
                players.Find(player => player.id == winner.id).score++;
                return Actions.resetForNextGame(players);
            }

            for (int i = 0; i < unqualifiedPlayers.Count; i++)
            {
                Console.WriteLine($"\nOyuncu {unqualifiedPlayers[i].id} seçiminizi yapınız. Şuanki puanınız: {unqualifiedPlayers[i].point}");
                Console.WriteLine("(1)-HIT\n(2)-STAND");
                string decision = Console.ReadLine();
                if (decision == "1")
                {
                    Tuple<List<Card>, List<Card>> current = Actions.drawCard(cards, 1);
                    cards = current.Item2;
                    players[i].cards.AddRange(current.Item1);
                    players[i].point = Actions.calculatePlayerPoint(players[i].cards);
                    if (players[i].point > 21)
                    {
                        Console.WriteLine($"\n***Oyuncu {players[i].id}:BUST\n\n");
                        players[i].isBusted = true;
                        if (players.FindAll(player => player.isBusted != true).Count == 1) // BUST olmayan tek kişi kalırsa kazanır
                        {
                            Player winner = players.Find(player => player.point <= 21);
                            Console.WriteLine($"Diğer oyuncular 'BUST', bu elin kazananı: {winner.id}, puan: {winner.point}");
                            players.Find(player => player.point <= 21).score++;
                            return Actions.resetForNextGame(players);
                        }
                    }
                }
                if (decision == "2")
                {
                    players[i].isStand = true;
                }
            }
            return play(players, cards);
        }
    }
}
