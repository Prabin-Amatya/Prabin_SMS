using Microsoft.EntityFrameworkCore;
using Prabin_SMS.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabin_SMS.Infrastructure.Repository
{
    public class RawSqlRepository:IRawSqlRepository
    {
        private readonly SMSDbContext _smsDbContext;

        public RawSqlRepository(SMSDbContext smsDbContext)
        {
            _smsDbContext = smsDbContext;
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            var result = _smsDbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
            return result;
        }
    }
}
