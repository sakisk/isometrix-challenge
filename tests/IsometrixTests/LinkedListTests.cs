using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace IsometrixTests;

public class LinkedListTests
{
    [Fact]
    public void ShouldCreateEmptyWithNullFirstAndLastNodeAReferenceType()
    {
        var sut = new Isometrix.LinkedList<TestType>();
        
        sut.First.Should().Be(null);
        sut.Last.Should().Be(null);
    }

    [Fact]
    public void ShouldCreateEmptyWithNullFirstAndLastNodeAValueType()
    {
        var sut = new Isometrix.LinkedList<int>();
        
        sut.First.Should().Be(null);
        sut.Last.Should().Be(null);
    }

    [Fact]
    public void ShouldAddFirstNodeInAnEmptyList()
    {
        const int expected = 1;
        var sut = new LinkedListBuilder<int>().Build();
        
        sut.AddFirst(expected);

        sut.First!.Data.Should().Be(expected);
    }

    [Fact]
    public void ShouldAddFirstNodeWithOldFirstNodeBecomingNext()
    {
        const int old = 1;
        const int newFirstNode = 2;
        var sut = new LinkedListBuilder<int>().WithElementsFromStart(old).Build();
        
        sut.AddFirst(newFirstNode);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(newFirstNode);
        sut.First!.Next!.Data.Should().Be(old);
    }

    [Fact]
    public void ShouldAllowAddingNewFirstNodeManyTimes()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;

        var sut = new LinkedListBuilder<int>().WithElementsFromStart(first).Build();
        
        sut.AddFirst(second);
        sut.AddFirst(third);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(third);
        sut.First!.Next!.Data.Should().Be(second);
        sut.First!.Next!.Next!.Data.Should().Be(first);
    }

    [Fact]
    public void ShouldAddLastNodeInAnEmptyListBecomesFirstNode()
    {
        const int expected = 1;

        var sut = new LinkedListBuilder<int>().Build();
        
        sut.AddLast(expected);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(expected);
        sut.Last!.Data.Should().Be(expected);
        sut.First!.Next.Should().BeNull();
        sut.Last!.Next.Should().BeNull();
        
    }

    [Fact]
    public void ShouldAddLastNodeWhenFirstExist()
    {
        const int first = 1;
        const int last = 2;

        var sut = new LinkedListBuilder<int>()
            .WithElementsFromStart(first)
            .Build();
        
        sut.AddLast(last);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Next.Should().NotBeNull();
        sut.Last!.Data.Should().Be(last);
        sut.Last!.Next.Should().BeNull();
    }

    [Fact]
    public void ShouldAddLastManyTimes()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;

        var sut = new LinkedListBuilder<int>().WithElementsFromStart(first).Build();
        
        sut.AddLast(second);
        sut.AddLast(third);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Next!.Data.Should().Be(second);
        sut.Last!.Data.Should().Be(third);
    }

    [Fact]
    public void ShouldAddElementInANodeAfterTheNodeContainingExistingElement()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int inserted = 4;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        sut.AddAfter(second, inserted);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Next!.Data.Should().Be(second);
        sut.First!.Next!.Next!.Data.Should().Be(inserted);
        sut.First!.Next!.Next!.Next!.Data.Should().Be(third);
    }

    [Fact]
    public void ShouldAddElementInANodeAfterLastNodeWhenContainsExistingElement()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int inserted = 4;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        sut.AddAfter(third, inserted);
        
        using var _ = new AssertionScope();
        sut.Last!.Data.Should().Be(inserted);
        sut.Last!.Previous!.Data.Should().Be(third);
    }

    [Fact]
    public void ShouldThrowWhenAddingAfterAnElementNotExistingInTheList()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int notExisting = 4;
        const int inserted = 5;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        var addAfterNonExisting = () => sut.AddAfter(notExisting, inserted);

        addAfterNonExisting.Should().Throw<InvalidOperationException>().WithMessage($"Element {notExisting} not found in any node");
    }

    [Fact]
    public void ShouldThrowWhenAddingBeforeAnElementNotExistingInTheList()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int notExisting = 4;
        const int inserted = 5;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        var addAfterNonExisting = () => sut.AddBefore(notExisting, inserted);

        addAfterNonExisting.Should().Throw<InvalidOperationException>().WithMessage($"Element {notExisting} not found in any node");
    }
    
    [Fact]
    public void ShouldAddElementInANodeBeforeNodeWhenContainsExistingElement()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int inserted = 4;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        sut.AddBefore(second, inserted);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Next!.Data.Should().Be(inserted);
        sut.First!.Next!.Next!.Data.Should().Be(second);
        sut.First!.Next!.Next!.Next!.Data.Should().Be(third);
    }

    [Fact]
    public void ShouldAddElementInANodeBeforeFirstNodeWhenContainsExistingElement()
    {
        const int first = 1;
        const int inserted = 2;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first).Build();

        sut.AddBefore(first, inserted);
        
        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(inserted);
        sut.First!.Next!.Data.Should().Be(first);
    }
    
    private class TestType
    {
    }
}