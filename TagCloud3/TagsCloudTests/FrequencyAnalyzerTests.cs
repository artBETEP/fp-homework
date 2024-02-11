﻿using FluentAssertions;
using System.Text;
using TagsCloudContainer.FrequencyAnalyzers;

namespace TagsCloudTests
{
    [TestFixture]
    public class FrequencyAnalyzerTests
    {
        [Test]
        public void Analyze_ShouldReturnCorrectFrequency_ForSimpleText()
        {
            var text = "hello\nworld\nhello";
            var expectedFrequency = new List<(string, int)>
                {
                    ( "hello", 2 ),
                    ( "world", 1 )
                };

            FrequencyAnalyzer.Analyze(text).Value.Should().BeEquivalentTo(expectedFrequency);
        }

        [Test]
        public void Analyze_ShouldNotChangeFrequency_ForEmptyText()
        {
            var text = string.Empty;
            var expectedFrequency = new List<(string, int)>();

            var sut = FrequencyAnalyzer.Analyze(text).Value;

            sut.Should().BeEquivalentTo(expectedFrequency);
        }

        [Test]
        public void GetAnalyzedText_ShouldNotContainExcludedWord()
        {
            var text = "hello\nworld\nhello";
            var exclude = "hello";
            var excludeFile = CreateTempFileWithText(exclude);

            FrequencyAnalyzer.Analyze(text, excludeFile).GetValueOrDefault().Should().NotContain(x => x.Item1.Contains(exclude));
        }

        private string CreateTempFileWithText(string text)
        {
            var tempFilePath = Path.GetTempFileName();
            using (var streamWriter = new StreamWriter(tempFilePath, false, Encoding.UTF8))
            {
                streamWriter.Write(text);
            }
            return tempFilePath;
        }
    }
}