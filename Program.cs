using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortCardsList
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Перечислите имена городов, чтобы составить карту путешествия(имена должны быть уникальными и розделены запятой)!!!");
            string cityList = Console.ReadLine();
            SortCardClass sortCard = new SortCardClass();
            var cardList = sortCard.GetCards(cityList);
            while (cardList == null)
            {
                Console.WriteLine();
                Console.WriteLine("Поправтье входяшие параметри!!!");
                cityList = Console.ReadLine();
                cardList = sortCard.GetCards(cityList);
            }
            sortCard.SortList(cardList);
            Console.ReadKey();
        }
    }

    public class SortCardClass
    {
        public void SortList(List<Card> list)
        {
            List<Card> listCard = list;
            Queue<Card> sortedCard = new Queue<Card>();

            Console.WriteLine();
            Console.WriteLine("Случайный маршрут:");

            foreach (var c in listCard)
            {
                Console.WriteLine(String.Format("{0} -> {1}", c.From, c.To));
            }

            var toarr = listCard.Select(x => x.To);
            string start = listCard.Select(x => x.From).Except(toarr).Single();

            var firstCard = listCard.Single(x => String.ReferenceEquals(x.From, start));
            sortedCard.Enqueue(firstCard);

            string next = firstCard.To;
            listCard.Remove(firstCard);

            while (listCard.Count > 0)
            {
                var nextCard = listCard.Single(x => String.ReferenceEquals(x.From, next));
                sortedCard.Enqueue(nextCard);
                next = nextCard.To;
                listCard.Remove(nextCard);
            }

            Console.WriteLine();
            Console.WriteLine("Результат:");
            while (sortedCard.Count > 0)
            {
                var c = sortedCard.Dequeue();
                Console.WriteLine(String.Format("{0} -> {1}", c.From, c.To));
            }
        }


        public List<Card> GetCards(string cityList)
        {
            if (string.IsNullOrWhiteSpace(cityList))
            {
                return null;
            }

            Random rand = new Random();
            List<Card> listCard = new List<Card>();
            List<string> cities = cityList.Split(new char[] { ',' }).Select(x => x.Replace(" ", "")).ToList();
            if (cities.Count < 2)
            {
                return null;
            }
            var uniqueCities = cities.GroupBy(x => x).Where(x => x.Count() > 1).ToList();
            if (uniqueCities.Count > 0)
            {
                return null;
            }

            int counter = rand.Next(0, cities.Count - 1);
            string fromCity = cities[counter];
            cities.RemoveAt(counter);

            while (cities.Count > 0)
            {
                counter = rand.Next(0, cities.Count - 1);
                listCard.Add(new Card { From = fromCity, To = cities[counter] });
                fromCity = cities[counter];
                cities.RemoveAt(counter);
            }
            listCard.Shuffle();
            return listCard;
        }
    }

    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rand = new Random();
            int count = list.Count;
            while (count > 1)
            {
                count--;
                int position = rand.Next(count + 1);
                T value = list[position];
                list[position] = list[count];
                list[count] = value;
            }
        }
    }

    public class Card
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}