using System;
using System.Globalization;

namespace BookClass
{
    /// <summary>
    /// Represents a book with author, title, publisher, and other information.
    /// </summary>
    public class Book : IEquatable<Book>, IComparable<Book>, IComparable, IFormattable
    {
        private bool published;
        private DateTime datePublished;
        private int totalPages;

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="author">Author of the book.</param>
        /// <param name="title">Title of the book.</param>
        /// <param name="publisher">Publisher of the book.</param>
        public Book(string author, string title, string publisher)
            : this(author, title, publisher, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class with an ISBN.
        /// </summary>
        /// <param name="author">The author of the book.</param>
        /// <param name="title">The title of the book.</param>
        /// <param name="publisher">The publisher of the book.</param>
        /// <param name="isbn">The ISBN of the book.</param>
        public Book(string author, string title, string publisher, string isbn)
        {
            this.Author = author;
            this.Title = title;
            this.Publisher = publisher;
            this.ISBN = isbn;
        }

        /// <summary>
        /// Gets the author of the book.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the publisher of the book.
        /// </summary>
        public string Publisher { get; }

        /// <summary>
        /// Gets the ISBN of the book.
        /// </summary>
        public string ISBN { get; }

        /// <summary>
        /// Gets the price of the book.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Gets the currency used for the price of the book.
        /// </summary>
        public string Currency { get; private set; }

        /// <summary>
        /// Gets or sets the total number of pages in the book.
        /// </summary>
        public int Pages
        {
            get => this.totalPages;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.totalPages = value;
            }
        }

        /// <summary>
        /// Update the published status and publication date of the book.
        /// </summary>
        /// <param name="date">The publication date of the book.</param>
        public void Publish(DateTime date)
        {
            this.published = true;
            this.datePublished = date;
        }

        /// <summary>
        /// Gets the publication date of the book as a formatted string.
        /// </summary>
        /// <returns>A string representation of the publication date.</returns>
        public string GetPublicationDate()
        {
            return this.published ? this.datePublished.ToString("d", CultureInfo.InvariantCulture) : "Not published yet.";
        }

        /// <summary>
        /// Sets the price and currency of the book.
        /// </summary>
        /// <param name="price">The price of the book.</param>
        /// <param name="currency">The currency used for the price of the book.</param>
        public void SetPrice(decimal price, string currency)
        {
            this.Price = price;
            this.Currency = currency;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Title} by {this.Author}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Book);
        }

        /// <inheritdoc />
        public bool Equals(Book other)
        {
            return other != null && this.ISBN == other.ISBN;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ISBN.GetHashCode();
        }

        /// <inheritdoc />
        public int CompareTo(Book other)
        {
            return other == null ? 1 : this.Title.CompareTo(other.Title);
        }

        /// <inheritdoc />
        int IComparable.CompareTo(object obj)
        {
            return this.CompareTo(obj as Book);
        }

        /// <summary>
        /// Converts the value of the current Book object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of the value of the current object, formatted as specified by the format and formatProvider parameters.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            if (formatProvider == null)
            {
                _ = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return $"{this.Title} by {this.Author}";
                case "D":
                    return $"{this.Title} by {this.Author}. {this.datePublished.Year}. {this.Publisher}. ISBN: {this.ISBN}. {this.Pages} pages. {this.Currency}{this.Price:F2}.";
                case "P":
                    return $"{this.Title} by {this.Author}. {this.datePublished.Year}. {this.Publisher}. ISBN: {this.ISBN}. {this.Pages} pages.";
                case "Y":
                    return $"{this.Title} by {this.Author}. {this.datePublished.Year}. {this.Publisher}. {this.Pages} pages.";
                case "T":
                    return $"{this.Title} by {this.Author}. {this.datePublished.Year}. {this.Pages} pages.";
                case "R":
                    return $"{this.Title} by {this.Author} {this.Currency}{this.Price:F2}.";
                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }

    }
}
