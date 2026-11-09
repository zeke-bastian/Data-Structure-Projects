using System;

/// <summary>
/// Represents an array-backed browser history stack.
/// </summary>
public class q1_Stack
{
    private const int InitialCapacity = 11;

    private q1_HistoryEntry[] items;   // backing array
    private int count;                 // entries stored; also the index of the next free slot

    /// <summary>
    /// Initializes an empty browser history stack.
    /// </summary>
    public q1_Stack()
    {
        items = new q1_HistoryEntry[InitialCapacity];
        count = 0;
    }

    /// <summary>
    /// Gets the current length of the backing array.
    /// </summary>
    public int Capacity
    {
        get { return items.Length; }
    }

    /// <summary>
    /// Adds an entry to the top of the stack, resizing the backing array when necessary.
    /// </summary>
    /// <param name="entry">The history entry to add.</param>
    public void Push(q1_HistoryEntry entry)
    {
        if (count == items.Length)
        {
            Resize();
        }

        items[count] = entry;
        count++;
    }

    /// <summary>
    /// Removes and returns the entry at the top of the stack.
    /// </summary>
    /// <returns>The entry removed from the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public q1_HistoryEntry Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Cannot Pop: the stack is empty.");
        }

        count--;
        q1_HistoryEntry top = items[count];

        Console.WriteLine("Pop()  -> " + top);
        return top;
    }

    /// <summary>
    /// Returns the entry at the top of the stack without removing it.
    /// </summary>
    /// <returns>The entry at the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public q1_HistoryEntry Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Cannot Peek: the stack is empty.");
        }

        return items[count - 1];
    }

    /// <summary>
    /// Determines whether the stack contains no entries.
    /// </summary>
    /// <returns><see langword="true"/> when the stack is empty; otherwise, <see langword="false"/>.</returns>
    public bool IsEmpty()
    {
        return count == 0;
    }

    /// <summary>
    /// Gets the number of entries currently stored in the stack.
    /// </summary>
    /// <returns>The number of entries in the stack.</returns>
    public int Size()
    {
        return count;
    }

    // Resize: new array at double the capacity, copy every entry across, swap it in.
    private void Resize()
    {
        int oldCapacity = items.Length;
        int newCapacity = oldCapacity * 2;
        q1_HistoryEntry[] newItems = new q1_HistoryEntry[newCapacity];

        for (int i = 0; i < count; i++)
        {
            newItems[i] = items[i];
        }

        items = newItems;

        Console.WriteLine("RESIZE: array full (" + count + "/" + oldCapacity + "), capacity doubled " + oldCapacity + " -> " + newCapacity);
    }
}