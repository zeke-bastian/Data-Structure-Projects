#nullable enable

/// <summary>
/// One node of the binary search tree. It stores a single browsing-history entry
/// and references to its left and right child nodes.
/// </summary>
public class q2_BSTNode
{
    /// <summary>The history entry stored in this node. Its URL is the key the tree is ordered by.</summary>
    public q1_HistoryEntry Data { get; set; }

    /// <summary>Left child: root of the subtree whose URLs come BEFORE this node's URL (null if none).</summary>
    public q2_BSTNode? Left { get; set; }

    /// <summary>Right child: root of the subtree whose URLs come AFTER this node's URL (null if none).</summary>
    public q2_BSTNode? Right { get; set; }

    /// <summary>Creates a node with no children that stores the given entry.</summary>
    /// <param name="data">The history entry to store.</param>
    public q2_BSTNode(q1_HistoryEntry data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}