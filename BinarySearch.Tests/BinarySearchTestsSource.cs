using System.Collections.Generic;
using BookClass;
using NUnit.Framework;

namespace BinarySearch.Tests
{
    public class BinarySearchTestsSource
    {
        private static readonly Book[] BookArrayDefaultCompare = new Book[]
        {
            new Book("Graysen Cooper", "Annihilation Of The Lost Ones", "Hawkeyes Racing News"),
            new Book("Forrest Watson", "Defender Of Dread", "Realty Mart"),
            new Book("Solomon Flores", "Descendants And Aliens", "Miss Madison Racing Team"),
            new Book("Wayne Walker", "Foreigner Without Fear", "World Class Coaching"),
            new Book("Zachary Ward", "Foundation Without Desire", "Mindlab Media Inc"),
            new Book("Quennel Morris", "Knights And Soldiers", "Desert Bloom Inc"),
            new Book("Denver Kelly", "Rescue At A Storm", "Optometry & Vision Science"),
            new Book("Zack Jones", "Robots With Silver", "RR Bowker Llc"),
            new Book("Uriyah Powell", "Screams At Technology", "Computer Games Magazine"),
            new Book("Bowen James", "Trees Of The Ancients", "Links Magazine Inc"),
        };

        private static readonly Book[] BookArrayAuthorCompare = new Book[]
        {
            new Book("Bowen James", "Trees Of The Ancients", "Links Magazine Inc"),
            new Book("Uriyah Powell", "Screams At Technology", "Computer Games Magazine"),
            new Book("Zack Jones", "Robots With Silver", "RR Bowker Llc"),
            new Book("Denver Kelly", "Rescue At A Storm", "Optometry & Vision Science"),
            new Book("Quennel Morris", "Knights And Soldiers", "Desert Bloom Inc"),
            new Book("Zachary Ward", "Foundation Without Desire", "Mindlab Media Inc"),
            new Book("Wayne Walker", "Foreigner Without Fear", "World Class Coaching"),
            new Book("Solomon Flores", "Descendants And Aliens", "Miss Madison Racing Team"),
            new Book("Forrest Watson", "Defender Of Dread", "Realty Mart"),
            new Book("Graysen Cooper", "Annihilation Of The Lost Ones", "Hawkeyes Racing News"),
        };

        public static IEnumerable<TestCaseData> TestCasesBookDefaultCompare()
        {
            yield return new TestCaseData(BookArrayDefaultCompare, BookArrayDefaultCompare[1], 1);
            yield return new TestCaseData(BookArrayDefaultCompare, BookArrayDefaultCompare[5], 5);
            yield return new TestCaseData(BookArrayDefaultCompare, BookArrayDefaultCompare[3], 3);
            yield return new TestCaseData(BookArrayDefaultCompare, new Book("Zack Jones", string.Empty, "RR Bowker Llc"), -1);
        }

        public static IEnumerable<TestCaseData> TestCasesBookObjDefaultCompare()
        {
            yield return new TestCaseData(BookArrayDefaultCompare, BookArrayDefaultCompare[1], 1);
            yield return new TestCaseData(BookArrayDefaultCompare, BookArrayDefaultCompare[5], 5);
            yield return new TestCaseData(BookArrayDefaultCompare, BookArrayDefaultCompare[3], 3);
            yield return new TestCaseData(BookArrayDefaultCompare, new Book("Zack Jones", string.Empty, "RR Bowker Llc"), -1);
        }

        public static IEnumerable<TestCaseData> TestCasesBookAuthorCompare()
        {
            yield return new TestCaseData(BookArrayAuthorCompare, BookArrayAuthorCompare[1], 1);
            yield return new TestCaseData(BookArrayAuthorCompare, BookArrayAuthorCompare[5], 5);
            yield return new TestCaseData(BookArrayAuthorCompare, BookArrayAuthorCompare[3], 3);
            yield return new TestCaseData(BookArrayAuthorCompare, new Book("Zack Jones", string.Empty, "RR Bowker Llc"), -1);
        }

        public static IEnumerable<TestCaseData> TestCasesBookObjAuthorCompare()
        {
            yield return new TestCaseData(BookArrayAuthorCompare, BookArrayAuthorCompare[1], 1);
            yield return new TestCaseData(BookArrayAuthorCompare, BookArrayAuthorCompare[5], 5);
            yield return new TestCaseData(BookArrayAuthorCompare, BookArrayAuthorCompare[3], 3);
            yield return new TestCaseData(BookArrayAuthorCompare, new Book("Zack Jones", string.Empty, "RR Bowker Llc"), -1);
        }
    }
}
