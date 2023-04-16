namespace RickAndMorty.Core.Domain.Entities
{
    public class Episode
    {
        public long Id { get; set; }
        /// <summary>
        /// Surrogate key
        /// </summary>
        public long? Sk { get; set; }
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime Created { get; set; }
        public string AirDate { get; set; } = default!;
        public string EpisodeNumber { get; set; } = default!;
        #region Navigation Properties
        public virtual IList<Character> Characters { get; set; } = new List<Character>();
        #endregion
    }
}