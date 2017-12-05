using Blog.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        int SubmitForm(TEntity entity, Expression<Func<TEntity, bool>> predicate);

        int Insert(TEntity entity);
        int Insert(List<TEntity> entitys);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        TEntity FindEntity(object keyValue);
        TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);

        //REntity FindEntity<REntity>(object keyValue);

        //REntity FindEntity<REntity>(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> IQueryable();
        IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList(string strSql);
        List<TEntity> FindList(string strSql, Pagination pagination);
        List<TEntity> FindList(string strSql, SqlParameter[] SqlParameter, Pagination pagination);
        List<TEntity> FindList(string strSql, SqlParameter[] SqlParameter);
        List<TEntity> FindList(Pagination pagination);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
    }
}
