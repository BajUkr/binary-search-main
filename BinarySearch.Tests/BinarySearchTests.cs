using System;
using System.Collections.Generic;
using BookClass;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace BinarySearch.Tests
{
    [TestFixture]
    public class BinarySearchTests
    {
        [TestCaseGeneric(new[] { int.MinValue, -5, 0, 1, 2, 3, 4, 5, 6, 7, 8 }, int.MinValue, ExpectedResult = 0,
            Type = typeof(int))]
        [TestCaseGeneric(new[] { 0, 1, 2, 3, 4, 5, 6, 7, int.MaxValue }, int.MaxValue, ExpectedResult = 8,
            Type = typeof(int))]
        [TestCaseGeneric(new[] { 0, 1, 1, 2, 3, 4, 5, 6, 7, int.MaxValue }, int.MinValue, ExpectedResult = -1,
            Type = typeof(int))]
        [TestCaseGeneric(new[] { -10.6, -5.1, 0.5, 1.4114, 1.441, 3.121, 4.9595, double.MaxValue }, 1.4114d,
            ExpectedResult = 3, Type = typeof(double))]
        [TestCaseGeneric(new[] { "Are", "Arena", "Boom", "Viva", "Zero" }, "Viva", ExpectedResult = 3,
            Type = typeof(string))]
        [TestCaseGeneric(new[] { "Are", "Arena", "Boom", "Viva", "Zero" }, "Vi va", ExpectedResult = -1,
            Type = typeof(string))]
        [TestCaseGeneric(
            new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z',
            }, "Z", ExpectedResult = 25, Type = typeof(char))]
        [TestCaseGeneric(
            new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z',
            }, "a", ExpectedResult = -1, Type = typeof(char))]
        public int BinarySearch_WithoutComparer<T>(T[] array, T value) => array.BinarySearch(value);

        [TestCaseSourceGeneric(typeof(BinarySearchTestsSource),
            nameof(BinarySearchTestsSource.TestCasesBookDefaultCompare), Type = typeof(Book))]
        [TestCaseSourceGeneric(typeof(BinarySearchTestsSource),
            nameof(BinarySearchTestsSource.TestCasesBookObjDefaultCompare), Type = typeof(object))]
        public void BinarySearch_WithoutComparer<T>(T[] array, T value, int expectedResult)
        {
            Assert.AreEqual(array.BinarySearch(value), expectedResult);
        }

        [TestCase(new[] { "Are", "Arena", "Boom", "Viva", "Zero" }, 'a')]
        [TestCase(new[] { 1, 2, (object)3, }, "Zero")]
        public void BinarySearch_WithoutComparer_ThrowArgumentException(object[] array, object value) =>
            Assert.Throws<ArgumentException>(
                () => array.BinarySearch(value),
                "Value is of a type that is not compatible with the elements of array");

        [TestCaseGeneric(null, int.MinValue, Type = typeof(int))]
        [TestCaseGeneric(null, 1.4114d, Type = typeof(double))]
        [TestCaseGeneric(null, "Viva", Type = typeof(string))]
        [TestCaseGeneric(null, "Z", Type = typeof(char))]
        public void BinarySearch_WithoutComparer_ThrowArgumentNullException<T>(T[] array, T value) =>
            Assert.Throws<ArgumentNullException>(
                () => array.BinarySearch(value),
                "Value is of a type that is not compatible with the elements of array");

        [TestCaseGeneric(
            new[] { 8, 7, 6, 5, 4, 3, 2, 1, 0, int.MinValue }, int.MinValue, ExpectedResult = 9,
            Type = typeof(int))]
        [TestCaseGeneric(
            new[] { int.MaxValue, 8, 7, 6, 5, 4, 3, 2, 1, 0, int.MinValue }, int.MaxValue,
            ExpectedResult = 0, Type = typeof(int))]
        [TestCaseGeneric(
            new[] { 8, 7, 6, 5, 4, 3, 2, 1, 0, int.MinValue }, int.MaxValue, ExpectedResult = -1,
            Type = typeof(int))]
        [TestCaseGeneric(
            new[] { 10.6, 5.1, 0.5, -1.4114, -1.441, -3.121, -4.9595, double.MinValue }, -1.441d,
            ExpectedResult = 4, Type = typeof(double))]
        [TestCaseGeneric(
            new[] { "Zero", "Viva", "Boom", "Arena", "Are", }, "Viva", ExpectedResult = 1,
            Type = typeof(string))]
        [TestCaseGeneric(
            new[] { "Zero", "Viva", "Boom", "Arena", "Are", }, "Vi va", ExpectedResult = -1,
            Type = typeof(string))]
        [TestCaseGeneric(
            new[]
            {
                'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G',
                'F', 'E', 'D', 'C', 'B', 'A',
            }, "Z", ExpectedResult = 0, Type = typeof(char))]
        [TestCaseGeneric(
            new[]
            {
                'Z', 'Y', 'X', 'W', 'V', 'U', 'T', 'S', 'R', 'Q', 'P', 'O', 'N', 'M', 'L', 'K', 'J', 'I', 'H', 'G',
                'F', 'E', 'D', 'C', 'B', 'A',
            }, "a", ExpectedResult = -1, Type = typeof(char))]
        public int BinarySearch_WithComparer<T>(T[] array, T value)
        {
            var mockComparer = new Mock<Comparer<T>>();
            mockComparer
                .Setup(p => p.Compare(It.IsAny<T>(), It.IsAny<T>()))
                .Returns((T a, T b) => Comparer<T>.Default.Compare(b, a));

            return array.BinarySearch(value, mockComparer.Object);
        }

        [TestCaseSourceGeneric(typeof(BinarySearchTestsSource),
            nameof(BinarySearchTestsSource.TestCasesBookAuthorCompare), Type = typeof(Book))]
        [TestCaseSourceGeneric(typeof(BinarySearchTestsSource),
            nameof(BinarySearchTestsSource.TestCasesBookObjAuthorCompare), Type = typeof(object))]
        public void BinarySearch_WithComparer<T>(T[] array, T value, int expectedResult)
        {
            var mockComparer = new Mock<Comparer<T>>();
            mockComparer
                .Setup(p => p.Compare(It.IsAny<T>(), It.IsAny<T>()))
                .Returns((T a, T b) => Comparer<T>.Default.Compare(b, a));

            Assert.AreEqual(array.BinarySearch(value, mockComparer.Object), expectedResult);
        }

        [TestCase(new[] { "Are", "Arena", "Boom", "Viva", "Zero" }, 'a')]
        [TestCase(new[] { 1, 2, (object)3, }, "Zero")]
        public void BinarySearch_WithComparer_ThrowArgumentException(object[] array, object value) =>
            Assert.Throws<ArgumentException>(() => array.BinarySearch(value, Comparer<object>.Default),
                "Value is of a type that is not compatible with the elements of array");

        [TestCaseGeneric(null, int.MinValue, Type = typeof(int))]
        [TestCaseGeneric(null, 1.4114d, Type = typeof(double))]
        [TestCaseGeneric(null, "Viva", Type = typeof(string))]
        [TestCaseGeneric(null, "Z", Type = typeof(char))]
        public void BinarySearch_WithComparer_ThrowArgumentNullException<T>(T[] array, T value) =>
            Assert.Throws<ArgumentNullException>(() => array.BinarySearch(value, Comparer<T>.Default),
                "Value is of a type that is not compatible with the elements of array");

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public class TestCaseGeneric : TestCaseAttribute, ITestBuilder, ITestData
        {
            public TestCaseGeneric(params object[] arguments) : base(arguments) { }

            public Type Type { get; set; }

            IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, Test suite)
            {
                if (method.IsGenericMethodDefinition && this.Type != null)
                {
                    var gm = method.MakeGenericMethod(this.Type);
                    return base.BuildFrom(gm, suite);
                }

                return base.BuildFrom(method, suite);
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public class TestCaseSourceGeneric : TestCaseSourceAttribute, ITestBuilder
        {
            public TestCaseSourceGeneric(string sourceName) : base(sourceName) { }

            public TestCaseSourceGeneric(Type sourceType, string sourceName) : base(sourceType, sourceName) { }

            public Type Type { get; set; }

            IEnumerable<TestMethod> ITestBuilder.BuildFrom(IMethodInfo method, Test suite)
            {
                if (method.IsGenericMethodDefinition && this.Type != null)
                {
                    var gm = method.MakeGenericMethod(this.Type);
                    return base.BuildFrom(gm, suite);
                }

                return base.BuildFrom(method, suite);
            }
        }
    }
}
