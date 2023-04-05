﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Dynamic.Core;
using Payment_Gateway.Shared.DataTransferObjects.Requests;
using Payment_Gateway.Shared.DataTransferObjects.Responses;
using System.Linq;

namespace Payment_Gateway.DAL.Extentions
{
    public static class RepositoryExtensions
    {
        public static async Task RunPendingMigrationsAsync<T>(this T db) where T : IdentityDbContext
        {
            List<string> pendingMigrations = (await db.Database.GetPendingMigrationsAsync()).ToList();

            if (pendingMigrations.Any())
            {
                var migrator = db.Database.GetService<IMigrator>();

                foreach (var targetMigration in pendingMigrations)
                    await migrator.MigrateAsync(targetMigration);
            }

            await Task.CompletedTask;
        }
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return query;

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<T>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return query;

            return query.OrderBy(orderQuery);
        }

        //public static async Task<PagedList<T>> GetPagedItems<T>(this IQueryable<T> query, RequestParameters parameters, Expression<Func<T, bool>> searchExpression = null)
        //{
        //    var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        //    if (searchExpression != null)
        //        query = query.Where(searchExpression);

        //    if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        //        query = query.Sort(parameters.OrderBy);

        //    var items = await query.Skip(skip).Take(parameters.PageSize).ToListAsync();
        //    return new PagedList<T>(items, await query.CountAsync(), parameters.PageNumber, parameters.PageSize);
        //}

        //public static PagedList<T> GetPagedItems<T>(this IEnumerable<T> query, RoleRequestDto request, RequestParameters parameters)
        //{
        //    var skip = (parameters.PageNumber - 1) * parameters.PageSize;

        //    var items = query.Skip(skip).Take(parameters.PageSize).ToList();
        //    return new PagedList<T>(items, query.Count(), parameters.PageNumber, parameters.PageSize);
        //}
    }
}
