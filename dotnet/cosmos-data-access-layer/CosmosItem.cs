namespace dk.commentor.starterproject.dal.cosmos
{
    // Must be public
    public class CosmosItem
    {
        public string id { get; set; }
        public dynamic Data { get; set; }

        public CosmosItem()
        {
        }        
    }
}