using AutoMapper;
using MongoDB.Driver;
using eTamir.Services.Comment.Settings;

namespace eTamir.Services.Comment.Repository
{
    public class CommentRepository :  ICommentRepository<Models.Comment>
    {
        public IMongoCollection<Models.Comment> Collection { get; }
        public IMapper Mapper {  get; }
        public CommentRepository(IMapper mapper, IDatabaseSettings dbSettins)
        {
            var client = new MongoClient(dbSettins.ConnectionString);
            var database = client.GetDatabase(dbSettins.DatabaseName);

            Collection = database.GetCollection<Models.Comment>(dbSettins.CommentsCollectionName);
            Mapper = mapper;
        }
    }
}