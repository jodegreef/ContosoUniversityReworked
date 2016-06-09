using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Infrastructure.Mapping
{
    public static class MapperExtensions
    {
        public static IPagedList<TDestination> ProjectToPagedList<TDestination>(this IQueryable queryable, MapperConfiguration config,
            int pageNumber, int pageSize)
        {
            return queryable.ProjectTo<TDestination>(config).Decompile().ToPagedList(pageNumber, pageSize);
        }
    }
}