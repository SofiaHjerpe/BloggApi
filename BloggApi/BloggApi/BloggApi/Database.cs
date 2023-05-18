using BloggApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BloggApi
{
    public class Database
    {
        private IMongoDatabase GetDb()
        {
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("PostDB");
            return db;
        }

        public async Task<List<Post>> GetPosts()
        {
            var posts = await GetDb().GetCollection<Post>("Posts")
                .Find(p => true).ToListAsync();
            return posts;

        }

        public async Task<Post> GetPost(string id)
        {
            ObjectId _id = new ObjectId(id);
            var post = await GetDb().GetCollection<Post>("Posts")
                .Find(p => p.Id == _id)
                .SingleOrDefaultAsync();
            return post;
        }
        public async Task SavePost(string title, string summary, string maincontent, int mood)
        {
            var post = new Post()
            {
                Title = title,
                Summary = summary,
                Maincontent = maincontent,  
                Mood = mood
            };



            await GetDb().GetCollection<Post>("Posts")
                .InsertOneAsync(post);
        }

        public async Task DeletePost(string id)
        {
            ObjectId _id = new ObjectId(id);
            await GetDb().GetCollection<Post>("Posts")
                .DeleteOneAsync(p => p.Id == _id);
        }
        public async Task UpdatePost(string id, string title, string summary, string maincontent)
        {
            ObjectId _id = new ObjectId(id);

            var update = Builders<Post>.Update
               .Set(p => p.Title, title);
            var update1 = Builders<Post>.Update
            .Set(p  => p.Summary, summary);
            var update2 = Builders<Post>.Update
            .Set(p => p.Maincontent, maincontent);


            await GetDb().GetCollection<Post>("Posts")
              .UpdateOneAsync(p => p.Id == _id, update);
            await GetDb().GetCollection<Post>("Posts")
             .UpdateOneAsync(p => p.Id == _id, update1);
            await GetDb().GetCollection<Post>("Posts")
            .UpdateOneAsync(p => p.Id == _id, update2);
        }
    }
}
