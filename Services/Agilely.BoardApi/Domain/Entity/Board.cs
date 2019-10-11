namespace Agilely.BoardApi.Domain.Entity
{
    public class Board
    {
        /// <summary>
        /// Gets or sets the id for this board.
        /// </summary>
        public string Id { get; set; } = System.Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the tenant id of the board.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the name of the board.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the board.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the board's logo url.
        /// </summary>
        public string Logo { get; set; }
    }
}