﻿using yumaster.Tools.AspNetCore.ResumeFileResults.Extensions;
using yumaster.Tools.AspNetCore.ResumeFileResults.ResumeFileResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace yumaster.Tools.AspNetCore.ResumeFileResults.Executor
{
    /// <summary>
    /// 可断点续传的FileStreamResult执行器
    /// </summary>
    internal class ResumeFileStreamResultExecutor : FileStreamResultExecutor, IActionResultExecutor<ResumeFileStreamResult>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ResumeFileStreamResultExecutor(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        /// 执行Result
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual Task ExecuteAsync(ActionContext context, ResumeFileStreamResult result)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            context.SetContentDispositionHeaderInline(result);

            return base.ExecuteAsync(context, result);
        }
    }
}