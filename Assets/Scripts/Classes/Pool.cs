using System.Collections.Generic;
using System.Collections;
public class Pool<T> : IEnumerable<T> where T : ICard
{
    List<T> cards = new List<T>();

    public SecureRandom RNGProvider { get; private set; } = new SecureRandom();

    public int Count { get => cards.Count; }

    public Pool()
    {
        cards = new List<T>();
    }

    public Pool(List<T> cards, bool withShuffle = false)
    {
        this.cards.AddRange(cards);

        if (withShuffle)
            Shuffle();
    }

    public void Shuffle()
    {
        int n = cards.Count;

        while (n > 1)
        {
            int k = RNGProvider.Next(n);

            n--;

            T value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public T DrawCard(int number = -1, bool random = false)
    {
        int k = (number >= 0 && number < Count) ? number : PeekNumberCard(random);

        T res = cards[k];
        cards.RemoveAt(k);

        return res;

    }

    public int PeekNumberCard(bool random = false) => random ? RNGProvider.Next(cards.Count) : 0;

    public void Add(T card) => cards.Add(card);

    public void Clear() => cards.Clear();

    public void RemoveAt(int index) => cards.RemoveAt(index);

    public T this[int index]
    {
        get => cards[index];
        set => cards[index] = value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T card in cards)
            yield return card;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
