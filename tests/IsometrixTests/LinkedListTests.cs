using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Isometrix;
using Xunit;

namespace IsometrixTests;

public class LinkedListTests
{
    [Fact]
    public void ShouldCreateEmptyWithNullFirstNodeAReferenceType()
    {
        new LinkedList<TestType>().First.Should().Be(null);
    }

    [Fact]
    public void ShouldCreateEmptyWithNullFirstNodeAValueType()
    {
        new LinkedList<int>().First.Should().Be(null);
    }

    [Fact]
    public void ShouldAddFirstNode()
    {
        const int expected = 1;
        var sut = new LinkedList<int>();
        
        sut.AddFirst(expected);

        sut.First!.Data.Should().Be(expected);
    }

    [Fact]
    public void ShouldAddFirstNode_ThenNextIsOldFirstNode()
    {
        const int old = 1;
        const int newFirstNode = 2;
        var sut = new LinkedList<int>();
        
        sut.AddFirst(old);
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
        
        var sut = new LinkedList<int>();
        
        sut.AddFirst(first);
        sut.AddFirst(second);
        sut.AddFirst(third);

        using var _ = new AssertionScope();
        sut.First!.Data.Should().Be(third);
        sut.First!.Next!.Data.Should().Be(second);
        sut.First!.Next!.Next!.Data.Should().Be(first);
    }

    private class TestType
    {
    }
}