namespace RickAndMorty.Api.Models
{
    public class CharacterInputModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Species
        /// </summary>
        public string Species { get; set; } = default!;
        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; } = default!;
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = default!;
        /// <summary>
        /// Image URL
        /// </summary>
        public string ImageUrl { get; set; } = default!;
        /// <summary>
        /// Location Id
        /// </summary>
        public long? LocationId { get; set; }
        /// <summary>
        /// Origin Id
        /// </summary>
        public long? OriginId { get; set; }
    }
}
