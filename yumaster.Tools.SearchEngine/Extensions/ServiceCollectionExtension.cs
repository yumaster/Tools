﻿using JiebaNet.Segmenter;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.JieBa;
using Lucene.Net.Store;
using yumaster.Tools.SearchEngine.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace yumaster.Tools.SearchEngine.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="option"></param>
        public static IServiceCollection AddSearchEngine<TContext>(this IServiceCollection services, LuceneIndexerOptions option) where TContext : DbContext
        {
            services.AddSingleton(s => option);
            services.AddMemoryCache();
            services.TryAddSingleton<Directory>(s => FSDirectory.Open(option.Path));
            services.TryAddSingleton<Analyzer>(s => new JieBaAnalyzer(TokenizerMode.Search));
            services.TryAddTransient<ILuceneIndexer, LuceneIndexer>();
            services.TryAddTransient<ILuceneIndexSearcher, LuceneIndexSearcher>();
            services.TryAddTransient(typeof(ISearchEngine<>), typeof(SearchEngine<>));
            services.TryAddTransient<ISearchEngine<TContext>, SearchEngine<TContext>>();
            return services;
        }
    }
}