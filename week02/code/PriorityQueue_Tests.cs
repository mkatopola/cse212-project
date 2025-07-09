using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Requirement 1:
    // Scenario: Add three items using Enqueue with different priorities.
    // Expected Result: Items are added to the queue without any issue.
    // Defect(s) Found: None.
    public void Test_Enqueue_AddsItemsToBack()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Item1", 1);
        queue.Enqueue("Item2", 2);
        queue.Enqueue("Item3", 3);
        string output = queue.ToString();
        Assert.AreEqual("[Item1 (Pri:1), Item2 (Pri:2), Item3 (Pri:3)]", output);
    }

    [TestMethod]
    // Requirement 2:
    // Scenario: Add items with different priorities and Dequeue once.
    // Expected Result: The item with the highest priority is removed and returned.
    // Defect(s) Found: None.
    public void Test_Dequeue_RemovesHighestPriorityItem()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Low", 1);
        queue.Enqueue("High", 10);
        queue.Enqueue("Medium", 5);

        string result = queue.Dequeue();

        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Requirement 3:
    // Scenario: Add two items with the same highest priority.
    // Expected Result: The one that was added first is removed first (FIFO).
    // Defect(s) Found: Previously removed the last-in instead of first-in for ties.
    public void Test_Dequeue_RespectsFIFOWhenTied()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("First", 7);
        queue.Enqueue("Second", 7);

        string result = queue.Dequeue();

        Assert.AreEqual("First", result);
    }

    [TestMethod]
    // Requirement 4:
    // Scenario: Try to Dequeue when the queue is empty.
    // Expected Result: Should throw InvalidOperationException.
    // Defect(s) Found: None.
    [ExpectedException(typeof(InvalidOperationException))]
    public void Test_Dequeue_OnEmptyQueue_ThrowsException()
    {
        var queue = new PriorityQueue();
        queue.Dequeue(); // This should throw an exception
    }


    [TestMethod]
    /*
    Scenario: Add one item and then remove it.
    Expected Result: The same item is returned.
    Defect(s) Found: None.
    */
    public void Test_EnqueueAndDequeue_OneItem()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("OnlyItem", 5);
        string result = queue.Dequeue();
        Assert.AreEqual("OnlyItem", result);
    }

    [TestMethod]
    /*
    Scenario: Add three items with different priorities and Dequeue twice.
    Expected Result: The two highest-priority items are removed in order.
    Defect(s) Found: None.
    */
    public void Test_Dequeue_TwoHighestPriorityItems()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Low", 1);
        queue.Enqueue("High", 10);
        queue.Enqueue("Medium", 5);

        string first = queue.Dequeue();  // Should return "High"
        string second = queue.Dequeue(); // Should return "Medium"

        Assert.AreEqual("High", first);
        Assert.AreEqual("Medium", second);
    }

    [TestMethod]
    /*
    Scenario: Add items with decreasing priority.
    Expected Result: Even though added in reverse order, Dequeue returns the one with highest priority.
    Defect(s) Found: None.
    */
    public void Test_Dequeue_FromReverseOrder()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Third", 1);
        queue.Enqueue("Second", 5);
        queue.Enqueue("First", 10); // highest priority, added last

        string result = queue.Dequeue(); // Should return "First"
        Assert.AreEqual("First", result);
    }
}