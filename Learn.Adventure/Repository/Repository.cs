using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Learn.Adventure.Attributes;
using Learn.Adventure.Models.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Learn.Adventure.Repository
{
    public class Repository<T> : IRepository<T> 
        where T : IDocument
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IDatabaseSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }
        
        private string GetCollectionName(Type documentType)
        {
            return ((CollectionAttribute) documentType.GetCustomAttributes(
                    typeof(CollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
        
        public IQueryable<T> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public T FindOne(Expression<Func<T, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).FirstOrDefaultAsync();
        }

        public T FindById(string id)
        {
            var filter = Builders<T>.Filter.Eq(document => document.Id, ObjectId.Parse(id));
            return _collection.Find(filter).SingleOrDefault();
        }

        public async Task<T> FindByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(document => document.Id, ObjectId.Parse(id));
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }
        
        public void InsertOne(T document)
        {
            _collection.InsertOne(document);
        }

        public async Task InsertOneAsync(T document)
        { 
            await _collection.InsertOneAsync(document);
        }

        public void ReplaceOne(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public async Task ReplaceOneAsync(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<T, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public async Task DeleteOneAsync(Expression<Func<T, bool>> filterExpression)
        {
            await _collection.FindOneAndDeleteAsync(filterExpression);
        }

        public void DeleteById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
            _collection.FindOneAndDelete(filter);
        }

        public async Task DeleteByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, ObjectId.Parse(id));
            await _collection.FindOneAndDeleteAsync(filter);
        }
    }
}