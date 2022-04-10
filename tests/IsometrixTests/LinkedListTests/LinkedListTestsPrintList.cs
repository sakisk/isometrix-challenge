using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Isometrix;
using Xunit;
using Xunit.Abstractions;

namespace IsometrixTests.LinkedListTests;

public class LinkedListTestsPrintList
{
    private readonly ITestOutputHelper _output;

    public LinkedListTestsPrintList(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ShouldPrintListOfInts()
    {
        var ints = new[] {1, 2, 3, 4, 5};

        var sut = new LinkedListBuilder<int>().WithElementsFromLast(ints).Build();

        var actual = new List<string>();
        sut.PrintList(data => actual.Add(data.ToString()));
        actual.Should().BeEquivalentTo(ints.Select(x => x.ToString()));
        sut.PrintList(data => _output.WriteLine(data.ToString()));
    }

    [Fact]
    public void ShouldPrintListOfSomeType()
    {
        var something = Enumerable.Range(1, 5).Select(x => new SomeType(x.ToString())).ToArray();
        
        var sut = new LinkedListBuilder<SomeType>().WithElementsFromLast(something).Build();
        
        var actual = new List<string>();
        sut.PrintList(data => actual.Add(data.ToString()));
        actual.Should().BeEquivalentTo(something.Select(x => x.ToString()));
        sut.PrintList(data => _output.WriteLine(data.ToString()));
    }

    private class SomeType
    {
        private readonly string _data;

        public SomeType(string data)
        {
            _data = data;
        }

        public override string ToString() => _data;
    }
}