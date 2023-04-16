namespace RickAndMorty.Core.Domain.Entities
{
    public class Location
    {
        public long Id { get; set; }
        /// <summary>
        /// Surrogate key
        /// </summary>
        public long? Sk { get; set; }
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime Created { get; set; }
        public string Type { get; set; } = default!;
        public string Dimension { get; set; } = default!;
        #region Navigation Properties
        public IList<Character> Residents { get; set; } = new List<Character>();
        #endregion
    }
}