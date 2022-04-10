using FluentAssertions;
using Xunit;

namespace IsometrixTests.LinkedListTests;

public class LinkedListTestsDeletingNodes
{
    [Fact]
    public void ShouldRemoveFirstNodeInAnEmptyList()
    {
        var sut = new LinkedListBuilder<int>().Build();
        
        sut.RemoveFirst();

        sut.First.Should().BeNull();
    }
}