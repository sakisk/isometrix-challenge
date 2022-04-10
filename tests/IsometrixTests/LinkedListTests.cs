using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace IsometrixTests;

public class LinkedListTests
{
    [Fact]
    public void ShouldCreateEmptyWithNullFirstAndLastNodeAReferenceType()
    {
        new Isometrix.LinkedList<TestType>().First.Should().Be(null);
        new Isometrix.LinkedList<TestType>().Last.Should().Be(null);
    }

    [Fact]
    public void ShouldCreateEmptyWithNullFirstAndLastNodeAValueType()
    {
        new Isometrix.LinkedList<int>().First.Should().Be(null);
        new Isometrix.LinkedList<int>().Last.Should().Be(null);
    }

    [Fact]
    public void ShouldAddFirstNode()
    {
        const int expected = 1;

        var sut = new LinkedListBuilder<int>().WithElementsFromStart(expected).Build();

        sut.First!.Data.Should().Be(expected);
    }

    [Fact]
    public void ShouldAddFirstNode_ThenNextIsOldFirstNode()
    {
        const int old = 1;
        const int newFirstNode = 2;

        var sut = new LinkedListBuilder<int>().WithElementsFromStart(old, newFirstNode).Build();

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

        var sut = new LinkedListBuilder<int>().WithElementsFromStart(first, second, third).Build();

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(third);
        sut.First!.Next!.Data.Should().Be(second);
        sut.First!.Next!.Next!.Data.Should().Be(first);
    }

    [Fact]
    public void ShouldAddLastNodeWhenFirstDoesNotExist_ThenBecomesFirst()
    {
        const int expected = 1;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(expected).Build();

        sut.First!.Data.Should().Be(expected);
        sut.Last!.Data.Should().Be(expected);
        sut.Last!.Next.Should().BeNull();
    }

    [Fact]
    public void ShouldAddLastNodeWhenFirstExist()
    {
        const int first = 1;
        const int last = 2;

        var sut = new LinkedListBuilder<int>()
            .WithElementsFromStart(first)
            .WithElementsFromLast(last)
            .Build();

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.Last!.Data.Should().Be(last);
    }

    [Fact]
    public void ShouldAddLastManyTimes()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(first);
        sut.First!.Next!.Data.Should().Be(second);
        sut.Last!.Data.Should().Be(third);
    }

    [Fact]
    public void ShouldAddElementAfterNodeWithExistingElement()
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
    public void ShouldAddElementAfterLastNode()
    {
        const int first = 1;
        const int second = 2;
        const int third = 3;
        const int inserted = 4;

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(first, second, third).Build();

        sut.AddAfter(third, inserted);
        sut.Last!.Data.Should().Be(inserted);
    }

    [Fact]
    public void ShouldThrow_WhenAddingAfterAnElementNotExistingInAnyNode()
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

    private class TestType
    {
    }
}