#nullable enable
using System;

/// <summary>
/// Binary search tree of browsing-history entries, ordered alphabetically by URL.
/// For every node, all URLs in its left subtree come before the node's URL and all
/// URLs in its right subtree come after it. No built-in collection classes are used.
/// </summary>
public class q2_BST
{
    private q2_BSTNode? root;   // top of the tree (null while the tree is empty)
    private int count;          // number of entries currently stored

    /// <summary>Number of entries currently stored in the tree.</summary>
    public int Count
    {
        get { return count; }
    }

    // Ordering rule shared by Insert, Search and Delete

    /// <summary>
    /// Compares two URLs alphabetically. The comparison is ordinal and case-insensitive:
    /// URLs are symbols rather than natural language, so the order must not change with the
    /// computer's culture settings.
    /// </summary>
    /// <returns>Negative if a comes before b, zero if they are equal, positive if a comes after b.</returns>
    private static int Compare(string a, string b)
    {
        return string.Compare(a, b, StringComparison.OrdinalIgnoreCase);
    }

    // Insert

    /// <summary>
    /// Inserts a history entry at the position decided by its URL. Starting at the root, go left
    /// when the new URL comes before the current node's URL and right when it comes after,
    /// until an empty child slot is found, then attach the new node there.
    /// </summary>
    /// <param name="entry">The entry to insert.</param>
    /// <returns>True if inserted; false if an entry with the same URL is already in the tree.</returns>
    /// <exception cref="ArgumentNullException">The entry is null.</exception>
    public bool Insert(q1_HistoryEntry entry)
    {
        if (entry == null)
        {
            throw new ArgumentNullException(nameof(entry));
        }

        q2_BSTNode newNode = new q2_BSTNode(entry);

        // Empty tree: the new node becomes the root.
        if (root == null)
        {
            root = newNode;
            count++;
            return true;
        }

        q2_BSTNode current = root;
        while (true)
        {
            int comparison = Compare(entry.URL, current.Data.URL);

            if (comparison == 0)
            {
                // This URL is already stored: reject the duplicate so every key stays unique.
                return false;
            }
            else if (comparison < 0)
            {
                // New URL comes before this node's URL, so it belongs in the left subtree.
                if (current.Left == null)
                {
                    current.Left = newNode;
                    count++;
                    return true;
                }
                current = current.Left;
            }
            else
            {
                // New URL comes after this node's URL, so it belongs in the right subtree.
                if (current.Right == null)
                {
                    current.Right = newNode;
                    count++;
                    return true;
                }
                current = current.Right;
            }
        }
    }

    // Search

    /// <summary>
    /// Searches for an entry by URL using binary search on the tree and prints the result.
    /// At each node the search discards one whole subtree: left if the URL comes before the
    /// node's URL, right if it comes after. It never scans the tree linearly.
    /// </summary>
    /// <param name="url">The URL to look for.</param>
    /// <returns>The matching entry, or null if the URL is not in the tree.</returns>
    public q1_HistoryEntry? Search(string url)
    {
        q2_BSTNode? current = root;
        int comparisons = 0;

        while (current != null)
        {
            comparisons++;
            int comparison = Compare(url, current.Data.URL);

            if (comparison == 0)
            {
                Console.WriteLine("Search(\"" + url + "\") -> FOUND after " + comparisons + " comparison(s): " + current.Data);
                return current.Data;
            }
            else if (comparison < 0)
            {
                current = current.Left;     // target comes before this node: go left
            }
            else
            {
                current = current.Right;    // target comes after this node: go right
            }
        }

        // Fell off the bottom of the tree (or the tree is empty): the URL is not stored.
        Console.WriteLine("Search(\"" + url + "\") -> NOT FOUND after " + comparisons + " comparison(s)");
        return null;
    }

    // Delete

    /// <summary>
    /// Removes the entry with the given URL, handling all three deletion cases:
    /// (1) leaf, (2) one child, (3) two children. Prints which case was applied.
    /// </summary>
    /// <param name="url">URL of the entry to remove.</param>
    /// <returns>True if an entry was removed; false if the URL was not found (tree unchanged).</returns>
    public bool Delete(string url)
    {
        int countBefore = count;

        // DeleteNode returns the new root of the tree, so deleting the root itself also works.
        root = DeleteNode(root, url);

        if (count == countBefore)
        {
            Console.WriteLine("Delete(\"" + url + "\") -> NOT FOUND, tree unchanged");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Recursive helper for Delete. Walks down to the node holding the URL and returns the root
    /// of the (possibly changed) subtree so the caller can re-link its child reference.
    /// </summary>
    /// <param name="node">Root of the subtree being searched (null when we have run off the tree).</param>
    /// <param name="url">URL to remove.</param>
    /// <returns>The new root of this subtree after the deletion.</returns>
    private q2_BSTNode? DeleteNode(q2_BSTNode? node, string url)
    {
        // Ran off the tree without finding the URL: nothing to delete here.
        if (node == null)
        {
            return null;
        }

        int comparison = Compare(url, node.Data.URL);

        if (comparison < 0)
        {
            // URL comes before this node, so it can only be in the left subtree.
            node.Left = DeleteNode(node.Left, url);
            return node;
        }
        if (comparison > 0)
        {
            // URL comes after this node, so it can only be in the right subtree.
            node.Right = DeleteNode(node.Right, url);
            return node;
        }

        // comparison == 0: this is the node to delete.
        string removedUrl = node.Data.URL;
        count--;

        // Case 1: leaf (no children). Remove it by telling the parent its child is now null.
        if (node.Left == null && node.Right == null)
        {
            Console.WriteLine("Delete(\"" + removedUrl + "\") -> case 1 (leaf): node removed");
            return null;
        }

        // Case 2: exactly one child. The child takes the deleted node's place.
        if (node.Left == null)
        {
            Console.WriteLine("Delete(\"" + removedUrl + "\") -> case 2 (one child): node removed, its right child took its place");
            return node.Right;
        }
        if (node.Right == null)
        {
            Console.WriteLine("Delete(\"" + removedUrl + "\") -> case 2 (one child): node removed, its left child took its place");
            return node.Left;
        }

        // Case 3: two children. Copy the in-order successor (smallest URL in the right subtree)
        // into this node, then remove the successor's original node from the right subtree.
        // The successor is the next URL in alphabetical order, so the ordering rule still holds.
        q2_BSTNode successor = FindMin(node.Right);
        Console.WriteLine("Delete(\"" + removedUrl + "\") -> case 3 (two children): node removed, replaced by in-order successor " + successor.Data.URL);
        node.Data = successor.Data;
        node.Right = RemoveMin(node.Right);
        return node;
    }

    /// <summary>Returns the node with the smallest URL in a subtree: keep going left.</summary>
    private static q2_BSTNode FindMin(q2_BSTNode node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    /// <summary>
    /// Removes the smallest node from a subtree and returns the subtree's new root.
    /// The smallest node has no left child, so its right child (possibly null) takes its place.
    /// </summary>
    private static q2_BSTNode? RemoveMin(q2_BSTNode node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = RemoveMin(node.Left);
        return node;
    }

    // InOrder

    /// <summary>
    /// Prints every entry in alphabetical URL order. In-order traversal visits the left subtree,
    /// then the node, then the right subtree, which produces the keys in sorted order.
    /// </summary>
    public void InOrder()
    {
        if (root == null)
        {
            Console.WriteLine("  (tree is empty)");
            return;
        }

        InOrderTraversal(root);
    }

    /// <summary>Recursive helper for InOrder: left subtree, this node, right subtree.</summary>
    private static void InOrderTraversal(q2_BSTNode? node)
    {
        if (node == null)
        {
            return;
        }

        InOrderTraversal(node.Left);
        Console.WriteLine("  " + node.Data);
        InOrderTraversal(node.Right);
    }

    // Height

    /// <summary>
    /// Returns the height of the tree: the number of edges on the longest path from the root down
    /// to a leaf. An empty tree has height -1 and a tree with only a root has height 0.
    /// </summary>
    /// <returns>The number of edges on the longest root-to-leaf path.</returns>
    public int Height()
    {
        return HeightOf(root);
    }

    /// <summary>Recursive helper for Height: one more than the taller of the two subtrees.</summary>
    private static int HeightOf(q2_BSTNode? node)
    {
        if (node == null)
        {
            return -1;
        }

        return 1 + Math.Max(HeightOf(node.Left), HeightOf(node.Right));
    }
}