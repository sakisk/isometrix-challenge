using System.Linq;
using FluentAssertions;
using Isometrix;
using Xunit;

namespace IsometrixTests.LinkedListTests;

public class LinkedListTestsGetDataFromList
{
    [Fact]
    public void ShouldReturnEmptyListWhenGettingDataForAnEmptyLinkedList()
    {
        new LinkedList<int>().GetData().ToList().Should().BeEmpty();
    }

    [Fact]
    public void ShouldReturnCorrectDataWhenGettingDataForALinkedListWithData()
    {
        var expected = new[] { 1, 2, 3, 4, 5 };
        var sut = new LinkedListBuilder<int>().WithElementsFromLast(expected).Build();
        
        var actual = sut.GetData().ToList();

        actual.Should().BeEquivalentTo(expected);
    }
}