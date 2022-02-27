using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Learn.Adventure.Attributes;
using Learn.Adventure.Models.Abstractions;
using Learn.Adventure.Repository.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Learn.Adventure.Repository.Implementation
{
    public class Repository<T> : IRepository<T> 
        where T : IDocument, new()
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
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                var filter = Builders<T>.Filter.Eq(document => document.Id, objectId);
                return _collection.Find(filter).SingleOrDefault();
            }
            return new T();
        }

        public async Task<T> FindByIdAsync(string id)
        {
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                var filter = Builders<T>.Filter.Eq(document => document.Id, objectId);
                return await _collection.Find(filter).SingleOrDefaultAsync();
            }

            return new T();
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
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDelete(filter);
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                await _collection.FindOneAndDeleteAsync(filter);
            }
        }
    }
}