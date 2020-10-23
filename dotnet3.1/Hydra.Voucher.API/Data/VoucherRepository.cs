using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Voucher.API.Configurations;
using Hydra.Voucher.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Hydra.Voucher.API.Data
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly IMongoClient _mongoClient;
        public readonly IMongoCollection<Vouchers> _collection;

        public VoucherRepository(IMongoClient mongoClient, IConfiguration config)
        {
            _mongoClient = mongoClient;

            var connectionString = config.GetSection("MongoDBConnection");
            var settings = connectionString.Get<MongoSettings>();

            var database = _mongoClient.GetDatabase(settings.DataBase);
            _collection = database.GetCollection<Vouchers>(settings.Collection);
        }

        public async Task Insert(Vouchers voucher) => 
            await _collection.InsertOneAsync(voucher);

        public async Task Delete(Guid id) => await _collection.DeleteOneAsync(p => p.Id == id);

        public async Task<Vouchers> Find(Guid id) =>
            await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task Update(Vouchers voucher) =>
            await _collection.ReplaceOneAsync(p => p.Id == voucher.Id, voucher);

        public async Task<List<Vouchers>> FindAll() =>
            await _collection.Find(f => f.Active == true).ToListAsync();

        public async Task<Vouchers> FindByCode(string code) =>
            await _collection.Find(p => p.Code == code).FirstOrDefaultAsync();
    }
}